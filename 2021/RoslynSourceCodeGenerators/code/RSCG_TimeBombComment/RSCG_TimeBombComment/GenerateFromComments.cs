using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace RSCG_TimeBombComment
{
    [Generator]
    public class GenerateFromComments : ISourceGenerator
    {
    
        public void Execute(GeneratorExecutionContext context)
        {
            var rec = context.SyntaxReceiver as ReceiveCommentsAndObsolete;
            if (rec == null)
                return;
            MakeCommentsDiagnostics(context, rec.candidatesComments.ToArray());
            MakeObsoleteVariables(context, rec.candidatesObsolete.ToArray());
        }

        private void MakeObsoleteVariables(GeneratorExecutionContext context, AttributeSyntax[] attributeSyntaxes)
        {
            Dictionary<string, string> sources = new Dictionary<string, string>();
            foreach (var syntaxNode in attributeSyntaxes)
            {
                var severity = DiagnosticSeverity.Warning;
                var id = syntaxNode.Name as IdentifierNameSyntax;
                if (id == null)
                    continue;
                var args = syntaxNode.ArgumentList?.Arguments.ToArray();
                if (args.Length != 2) //message and TB_ 
                    continue;
                var TB_Date = args
                    .Select(it => it.Expression as IdentifierNameSyntax)
                    .Where(it => it != null)
                    .Select(it => it.Identifier.Text)
                    .FirstOrDefault(it => it.StartsWith(ReceiveCommentsAndObsolete.obsoleteStart));
                if (TB_Date == null)
                    continue;
                
                var dateLimitString = TB_Date.Substring(ReceiveCommentsAndObsolete.obsoleteStart.Length);
                if(!DateTime.TryParseExact(dateLimitString,"yyyyMMdd",null,DateTimeStyles.AssumeUniversal,out var dateLimit))
                {
                    var dd = new DiagnosticDescriptor(DiagnosticId, Title, $"cannot parse {dateLimitString}"  , Category, severity, isEnabledByDefault: true, description: $"cannot parse {dateLimitString}");
                    var dg = Diagnostic.Create(dd, syntaxNode.GetLocation());
                    context.ReportDiagnostic(dg);
                    continue;
                }
                var Error = (dateLimit <= DateTime.UtcNow.Date).ToString().ToLower();
                var classForAttribute = FindClassParent(syntaxNode);
                if(classForAttribute is null)
                {
                    var dd = new DiagnosticDescriptor(DiagnosticId, Title, $"cannot find class parent ", Category, severity, isEnabledByDefault: true, description: $"cannot parse {dateLimitString}");
                    var dg = Diagnostic.Create(dd, syntaxNode.GetLocation());
                    context.ReportDiagnostic(dg);
                    continue;
                }

                var namespaceClass = classForAttribute?.Parent as NamespaceDeclarationSyntax;
                if (namespaceClass is null)
                {
                    var dd = new DiagnosticDescriptor(DiagnosticId, Title, $"cannot find namespace ", Category, severity, isEnabledByDefault: true, description: $"cannot parse {dateLimitString}");
                    var dg = Diagnostic.Create(dd, syntaxNode.GetLocation());
                    context.ReportDiagnostic(dg);
                    continue;
                }
                var varTb = $@"
namespace {namespaceClass.Name.GetText()} {{
    partial class {classForAttribute.Identifier.Text} {{ 
        const bool {TB_Date} = {Error};
    }}
}}

                ";

                if (!sources.ContainsKey(TB_Date))
                {
                    sources.Add(TB_Date, "");
                }
                sources[TB_Date] += varTb;
            }
            foreach (var item in sources)
            {
                context.AddSource(item.Key, item.Value);
            }

        }
        ClassDeclarationSyntax FindClassParent(AttributeSyntax att)
        {
            var p = att.Parent;
            while (p != null && ((p as ClassDeclarationSyntax) == null))
            {
                p = p.Parent;
            }

            return p as ClassDeclarationSyntax;
        }
        private void MakeCommentsDiagnostics(GeneratorExecutionContext context ,SyntaxTrivia[] comments)
        {   
            foreach (var item in comments)
            {

                var text = item.ToFullString().Replace(ReceiveCommentsAndObsolete.commentStart, "").Trim();
                var severity = DiagnosticSeverity.Warning;
                string message = text;
                string desc = text;

                if (text.Length >= 10)//yyyy.MM.dd
                {
                    if (DateTime.TryParseExact(text.Substring(0, 10), "yyyy-MM-dd", null, DateTimeStyles.AssumeUniversal, out var date))
                    {
                        message = text.Substring(10);
                        if (date <= DateTime.UtcNow.Date)
                        {
                            severity = DiagnosticSeverity.Error;
                        }
                        else
                        {
                            severity = DiagnosticSeverity.Hidden;
                        }
                    }
                }
                var dd = new DiagnosticDescriptor(DiagnosticId, Title, message, Category, severity, isEnabledByDefault: true, description: desc);
                var dg = Diagnostic.Create(dd, item.GetLocation());
                context.ReportDiagnostic(dg);
            }
        }
        private static readonly string DiagnosticId = "TB";
        private static readonly string Title = "TB";
        private static readonly string Category = "TB";
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ReceiveCommentsAndObsolete());
        }
    }
}
