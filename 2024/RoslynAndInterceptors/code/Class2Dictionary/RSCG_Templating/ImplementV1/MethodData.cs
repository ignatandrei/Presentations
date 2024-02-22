using RSCG_TemplatingCommon.InterfacesV1;

namespace RSCG_Templating.ImplementV1;

internal class MethodData : IMethodData
{
    public MethodData(IMethodSymbol methodSymbol)
    {
        if (methodSymbol == null) throw new ArgumentNullException(nameof(methodSymbol));
        this.methodAccesibility = (Accessibility)((int)methodSymbol.DeclaredAccessibility);
        this.methodSymbol = methodSymbol;
        MethodName = methodSymbol.Name;
        if (methodSymbol.Locations.Length == 1)
        {
            var location = methodSymbol.Locations[0];
            FileName = location.GetLineSpan().Path;
            Line = location.GetLineSpan().StartLinePosition.Line;
        }
        parameters = methodSymbol.Parameters
            .Where(it => !it.IsDiscard)
            .Where(it => !it.IsParams)
            .Where(it => it.Type.IsValueType || it.Type.SpecialType == SpecialType.System_String)
            .Select(it => it.Name)
            .ToArray();
    }
    public Accessibility methodAccesibility { get; private set; }
    public string[] parameters { get; private set; }
    public string MethodName { get; }
    private readonly IMethodSymbol methodSymbol;
    public string FileName { get; } = "";
    public int Line { get; }
    public string NameVariable
    {
        get
        {
            return $"{MethodName}_{Line}";
        }
    }
    public string MethodCall {
        get
        {
            // Assuming you have an IMethodSymbol named 'methodSymbol'
            var originalMethodDeclaration = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.ParseTypeName(methodSymbol.OriginalDefinition.ReturnType.ToDisplayString()),
                methodSymbol.Name
            );

            // Add parameters with optional/default values
            var parameters = methodSymbol.OriginalDefinition.Parameters.Select(parameter =>
            {
                var parameterSyntax = SyntaxFactory.Parameter(
                    default,
                    default,
                    SyntaxFactory.ParseTypeName(parameter.Type.ToDisplayString()),
                    SyntaxFactory.Identifier(parameter.Name),
                    default
                );


                return parameterSyntax;
            });

            originalMethodDeclaration = originalMethodDeclaration.WithParameterList(
                SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(parameters))
            );

            // Generate the method call expression with the same parameters
            var methodCallExpression = SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("original"), // Change variableName to your desired variable name
                    SyntaxFactory.IdentifierName(methodSymbol.Name)
                ),
                SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList(methodSymbol.OriginalDefinition.Parameters.Select(parameter =>
                        SyntaxFactory.Argument(SyntaxFactory.IdentifierName(parameter.Name))
                    ))
                )
            );


            // Convert the method call expression to a string
            var methodCallString = methodCallExpression.NormalizeWhitespace().ToFullString();
            if (methodSymbol.ReturnsVoid)
            {
                if (IsAsyncMethod())
                {
                    methodCallString = "await " + methodCallString;
                }

            }
            else
            {
                if (IsAsyncMethod())
                {
                    if (methodSymbol.ReturnType.ToDisplayString().Contains(".Task<"))
                    {
                        methodCallString = "return await " + methodCallString;
                    }
                    else
                    {
                        methodCallString = "await " + methodCallString;
                    }
                }
                else
                {
                    methodCallString = " return " + methodCallString;
                }
            }

            methodCallString += ";";
            return methodCallString;

        }
    }
    public string MethodDeclaration
    {
        get
        {
            // Assuming you have an IMethodSymbol named 'methodSymbol'
            var originalMethodDeclaration = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.ParseTypeName(methodSymbol.OriginalDefinition.ReturnType.ToDisplayString()),
                methodSymbol.Name
            );

            // Add optional parameters if any
            var parameters = methodSymbol.OriginalDefinition.Parameters.Select(parameter =>
            {
                var parameterSyntax = SyntaxFactory.Parameter(
                    default,
                    default,
                    SyntaxFactory.ParseTypeName(parameter.Type.ToDisplayString()),
                    SyntaxFactory.Identifier(parameter.Name),
                    default
                );

                // Handle optional parameters
                if (parameter.HasExplicitDefaultValue)
                {
                    object defaultValue = parameter.ExplicitDefaultValue!;
                    var literalExpression = GetLiteralExpression(defaultValue);
                    parameterSyntax = parameterSyntax.WithDefault(SyntaxFactory.EqualsValueClause(literalExpression));

                }

                return parameterSyntax;
            });

            originalMethodDeclaration = originalMethodDeclaration.WithParameterList(
                SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(parameters))
            );
            // Check if the method returns Task or Task<T> and add the async modifier
            if (IsAsyncMethod())
            {
                originalMethodDeclaration = originalMethodDeclaration.WithModifiers(originalMethodDeclaration.Modifiers.Add(SyntaxFactory.Token(SyntaxKind.AsyncKeyword)));
            }

            // Convert the original method declaration to a string
            var originalMethodDeclarationString = originalMethodDeclaration.NormalizeWhitespace().ToFullString();

            // Print or use the originalMethodDeclarationString as needed
            return originalMethodDeclarationString;

        }
    }
    bool IsAsyncMethod()
    {
        if (methodSymbol.ReturnsVoid)
        {
            return false;
        }


        return methodSymbol.ReturnType.ToDisplayString().Contains("Task") || methodSymbol.ReturnType.OriginalDefinition.ToDisplayString().Contains("Task");
    }
    ExpressionSyntax GetLiteralExpression(object defaultValue)
    {
        if (defaultValue == null)
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);
        }
        else if (defaultValue is bool boolValue)
        {
            return boolValue ? SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression) : SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression);
        }
        else if (defaultValue is char charValue)
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(charValue));
        }
        else if (defaultValue is string stringValue)
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(stringValue));
        }
        else if (defaultValue is int intValue)
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(intValue));
        }
        // Add more cases for other types as needed
        else
        {
            throw new NotSupportedException($"Default value of type {defaultValue.GetType()} is not supported.");
        }
    }

}
