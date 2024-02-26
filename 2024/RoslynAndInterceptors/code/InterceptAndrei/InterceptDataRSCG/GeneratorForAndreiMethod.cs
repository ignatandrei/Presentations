using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace InterceptDataRSCG;

[Generator]
public class GeneratorForAndreiMethod : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classesToIntercept = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (s, _) => IsSyntaxTargetForGeneration(s),
            transform: static (context, token) =>
            {
                var operation = context.SemanticModel.GetOperation(context.Node, token);
                return operation;
            })
        .Where(static m => m is not null)!
        ;

    }
    private bool IsSyntaxTargetForGeneration(SyntaxNode s)
    {

        if (!TryGetMapMethodName(s, out var method))
            return false;
        return true;

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
