using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Immutable;

namespace InterceptDataRSCG;

[Generator]
public class GeneratorForAndreiMethod : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var methodsToIntercept = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (s, _) => IsSyntaxTargetForGeneration(s),
            transform: static (context, token) =>
            {
                var operation = context.SemanticModel.GetOperation(context.Node, token);
                if (operation is null)
                    return null;
                return new Tuple<SyntaxNode, IOperation>(context.Node, operation);
            })
        .Where(static m => m is not null)
        .Select((it, _) => it!)
        .Collect()
        ;
        context.RegisterSourceOutput(methodsToIntercept,GeneratedSourceOutput);
    }

    private void GeneratedSourceOutput(SourceProductionContext context, ImmutableArray<Tuple<SyntaxNode, IOperation>> array)
    {
        if(array.Length == 0)
            return;

        var data= array.ToArray();
        foreach(var item in data)
        {
            var syntaxNode = item.Item1;
            var operation = item.Item2;

            var nameVar = "";
            if (operation is ILocalReferenceOperation localOp)
                nameVar = localOp.Local.Name;

            if (!TryGetMapMethodName(syntaxNode, out var nameMethod))
                continue;
            
            if(string.IsNullOrWhiteSpace(nameMethod))
                continue;

            var location = syntaxNode.GetLocation();
            var lineSpan = location.GetLineSpan();
            var startLinePosition = lineSpan.StartLinePosition;

            var sourceText = location.SourceTree?.GetText() ;
            var line = sourceText!.Lines[startLinePosition.Line];

            var lineNumber = startLinePosition.Line + 1;
            var pathFile = lineSpan.Path;
            string code = line.ToString();

            int startMethod = code.IndexOf(nameVar+"."+nameMethod);
            startMethod +=nameVar.Length+1;//dot

            
            var output = $$"""
namespace System.Runtime.CompilerServices{
[AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
file class InterceptsLocationAttribute(string filePath, int line, int character) : Attribute
{
}
}
//end namespace for attribute

""";

            output += "\r\n";
            output += $$"""
//now the interceptor
namespace RSCG_InterceptorTemplate{
static partial class SimpleIntercept
{
[System.Runtime.CompilerServices.InterceptsLocation(@"{{pathFile}}", {{lineNumber}}, {{startMethod+1}})]
public static string NewAndrei_{{nameMethod}}_{{lineNumber}} (this InterceptAndreiConsole.Person p, string data )  
    {    
        var result =  p.{{nameMethod}}(data);
        var additionalInfo = "I have intercepted line {{lineNumber}}: ";
        return additionalInfo + result;
    }//end method NewAndrei_{{nameMethod}}
}//end class
}//end namespace

""";
            var nameFile = $"Intercept{nameMethod}_{lineNumber}.cs";
            context.AddSource(nameFile, output);
        }
    }

    private bool IsSyntaxTargetForGeneration(SyntaxNode s)
    {

        if (!TryGetMapMethodName(s, out var method))
            return false;
        
        if(string.IsNullOrWhiteSpace(method))
            return false;
        
        if (method.Contains("Andrei"))
            return true;

        return false;

    }
    public static bool TryGetMapMethodName(SyntaxNode node, out string? methodName)
    {
        if (node is not InvocationExpressionSyntax inv)
        {
            methodName = default;
            return false;
        }
        methodName = default;
        if (inv is InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier: { ValueText: var methodValue } } })
        {
            methodName = methodValue;
        }
        if (inv is InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax { Name: { Identifier: { ValueText: var method } } } })
        {
            methodName = method;
        }
        if (string.IsNullOrWhiteSpace(methodName))
            return false;

        var parent = inv.Parent;
        while (parent is not null && parent is not ClassDeclarationSyntax)
        {
            parent = parent.Parent;
        }
        if (parent == null)
            return true;//in program.cs without declaration of a class
        
        return true;
    }
}
