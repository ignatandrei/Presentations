using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace RSCG_TimeBombComment
{
    internal class ReceiveCommentsAndObsolete : ISyntaxReceiver
    {
        internal List<SyntaxTrivia> candidatesComments = new List<SyntaxTrivia>();
        internal List<AttributeSyntax> candidatesObsolete = new List<AttributeSyntax>();
        internal static string commentStart = "//TB:";
        internal static string obsoleteStart = "TB_";
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {

            if (syntaxNode is CompilationUnitSyntax)
            {
                var cu = syntaxNode as CompilationUnitSyntax;
                string s = syntaxNode.ToFullString();

                if (s.Contains(commentStart))
                {
                    var trivia = cu.DescendantTrivia().Where(it => it.IsKind(SyntaxKind.SingleLineCommentTrivia)).ToArray();
                    foreach (var item in trivia)
                    {
                        if (item.ToFullString().Contains(commentStart))
                        {
                            candidatesComments.Add(item);
                        }
                    }
                }
            }
            if (syntaxNode is AttributeSyntax)
            {
                var att = syntaxNode as AttributeSyntax;
                var name = att.Name;
                if (name is IdentifierNameSyntax)
                {
                    var id = name as IdentifierNameSyntax;
                    if (id.Identifier.Text == "Obsolete")
                    {
                        candidatesObsolete.Add(att);
                        
                    }
                }
            }
        }

       
    }
}