namespace RSCG_TimeBombComment;
internal class GenerateCommentsAsError
{

    public static void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var syntax = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (sn, _) => FindCorrectComment(sn),
            transform: (ctx, _) => GetDataForGeneration(ctx))
            .Where(it => it != null)
            .SelectMany((it, _) => it!)
            ;

        var comp = context
            .CompilationProvider
            .Combine(syntax.Collect());

        context.RegisterSourceOutput(comp,
           (spc, source) => Execute(source.Left, source.Right, spc));
    }
    private static Diagnostic GenerateForTB(SyntaxTrivia item)
    {
        string text = item.ToFullString();
        string message = "";
        

        DiagnosticSeverity severity = DiagnosticSeverity.Warning;
        if (text.StartsWith(commentStart2))
        {
            text = text.Substring(commentStart2.Length).Trim();

        }
        message = text;
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
                    severity = DiagnosticSeverity.Warning;
                }
            }
        }

        string desc = text;
        var dd = new DiagnosticDescriptor(DiagnosticId, Title, message, Category, severity, isEnabledByDefault: true, description: desc);
        var dg = Diagnostic.Create(dd, item.GetLocation());
        return dg;

    }
    private static void Execute(Compilation compilation, ImmutableArray<SyntaxTrivia> comments, SourceProductionContext spc)
    {
        if (comments.IsDefaultOrEmpty)
            return;
        var data = comments.Distinct();
        //int nr = 0;
        bool isOnRelease = (compilation.Options?.OptimizationLevel == OptimizationLevel.Release);
        foreach (var item in data)
        {
            var text = item.ToFullString();
            
            if (text.StartsWith(commentStart2))
            {
                int nr = 0;
                spc.ReportDiagnostic(GenerateForTB(item));
                var parent = item.Token.Parent;
                while (parent != null && !(parent is ClassDeclarationSyntax))
                {
                    parent = parent.Parent;
                }
                if (parent == null) continue;
                var classNode = parent as ClassDeclarationSyntax;
                var namespaceName = (classNode.Parent as BaseNamespaceDeclarationSyntax)?.Name?.ToFullString();
                if(!string.IsNullOrWhiteSpace(namespaceName))
                {
                    namespaceName = "namespace " + namespaceName+ ";";
                }
                var name = classNode!.Identifier.Text;
                var str=
$$"""
{{namespaceName}}
    
partial class {{name}}                            
{
    public static string FoundTimeBombComment_{{nr++}}()
    {
        return "{{item.ToFullString()}}";
    }
}
""";
            spc.AddSource($"{name}_TimeBombComment_{nr++}.cs", str);

            }
            
        }
    }

    private static readonly string DiagnosticId = "TB";
    private static readonly string Title = "TB";
    private static readonly string Category = "TB";
    internal static string commentStart2 = "//TODO";

    private static SyntaxTrivia[]? GetDataForGeneration(GeneratorSyntaxContext context)
    {
        GlobalStatementSyntax a;
        List<SyntaxTrivia> ret = new();
        var atts = context.Node as SyntaxNode;
        if (atts == null) return null;
        var tr = atts.DescendantTrivia();
        foreach (var item in tr)
        {
            var text = item.ToFullString();
            if (text.Contains(commentStart2))
            {
                ret.Add(item);
            }
       }
        return ret.ToArray();

    }
    private static bool FindCorrectComment(SyntaxNode node)
    {
        var x=node.ToFullString();
        var l = x.IndexOf(commentStart2);
        if (node is BlockSyntax || node is GlobalStatementSyntax)
        {
            var str = node.ToFullString();
            
            if (str.Contains(commentStart2))
            {
                return true;
            }
        }
        return false;

    }


}
