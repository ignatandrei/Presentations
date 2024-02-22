using System.Linq.Expressions;

namespace RSCG_Templating;

[Generator]
public class GeneratorIntercept : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        
        var classesInterfaceV1 = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (s, _) => IsSyntaxTargetForGeneration(s),
                transform: (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null)!
            ;
        

        var compilationAndData
            = context.CompilationProvider.Combine(classesInterfaceV1.Collect()).Combine(context.AdditionalTextsProvider.Collect());

        context.RegisterSourceOutput(compilationAndData,
            (spc,  data) => 
            ExecuteGen(spc, data));
    }

    private void ExecuteGen(SourceProductionContext spc, ((Compilation Left, ImmutableArray<Tuple<ClassDeclarationSyntax, INamedTypeSymbol>?> Right) Left, ImmutableArray<AdditionalText> Right) data)
    {
        Execute(data.Left.Left, data.Left.Right,data.Right,spc);
    }

    private void Execute(Compilation item1, ImmutableArray<Tuple<ClassDeclarationSyntax, INamedTypeSymbol>?> classData , ImmutableArray<AdditionalText> addtional, SourceProductionContext spc)
    {
        if(classData.Length ==0 ) return;
        var classes = classData.Where(it => it != null && it.Item1!= null).Select(it=>it!).ToArray();
        foreach(var tpl in classes)
        {
            
            var cds = tpl.Item1;
            var data = new ClassData();

            var baseNamespace = cds.Parent as BaseNamespaceDeclarationSyntax;
            var name = baseNamespace?.Name.ToFullString();
            data.nameSpace = name;
            data.className = cds.Identifier.ValueText;
            
            var symbolClass = tpl.Item2;

            var ts = symbolClass as ITypeSymbol;
            if (ts == null) continue;      
            
            data.Interfaces=symbolClass.Interfaces                
                .Select(it=>it.ToString())
                .ToArray();

            var methods = symbolClass
                .GetMembers()
                .Where(it=>it.Kind == SymbolKind.Method)
                .Select(it=>it as IMethodSymbol)
                .Where(it=>it != null)
                .Select(it=>it!)
                .Where(it=>it.MethodKind == MethodKind.Ordinary)
                .ToArray()
                ;
            
            foreach(var method in methods) 
            {
                var n = method.Name;
                var yu = method.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                var m = new MethodData(method);
                Console.WriteLine(m.MethodDeclaration);
                Console.WriteLine(m.MethodCall);
                Console.WriteLine("---");


            }
            data.methods= methods.Select(it=>new MethodData(it)).ToArray();
            data.properties= symbolClass
                .GetMembers()
                .Where(it=>it.Kind==SymbolKind.Property)
                .Select(it=>it as IPropertySymbol)
                .Where(it=>it != null)
                .Select(it=>it!)
                .Select(it=> new PropertyData(it))
                .ToArray();


            var allAtt = ts.GetAttributes()
                .Where(it => it.AttributeClass != null && it.AttributeClass.Name.Contains("IGenerateDataFromClass"))
                .ToArray();
                ;
            if (allAtt.Length == 0) continue;
            foreach (var myAtt in allAtt)
            {
                var nameAdd = myAtt.ConstructorArguments.First().Value?.ToString();

                var addText = addtional.Where(it => it.Path.EndsWith($"{nameAdd}.txt")).FirstOrDefault();
                

                if (addText == null) continue;

                var templateText = addText.GetText();
                if (templateText == null) continue;
                Template template;
                try
                {
                    template = Template.Parse(templateText.ToString());
                }
                catch(Exception ex)
                {
                    var dd = new DiagnosticDescriptor("RSCG_TEMPLATING_ERROR1", "ParseError", "ParseError", "RSCG_TEMPLATING", DiagnosticSeverity.Error, true);
                    Diagnostic d = Diagnostic.Create(dd, Location.None, ex.Message);
                    spc.ReportDiagnostic(d);
                    continue;
                }
                //will do with SCRIBAN . Every class has a corresponding scriban additional file.
                var result = template.Render(new { data , fileName = addText }, a => a.Name);//
                                                                        //var result = "namespace asd{ class MyData{ public int id=9;}}";
                var fileName = $"{data.nameSpace}.{data.className}.{nameAdd}";
                spc.AddSource(fileName, result);
            }

        }
    }

    private Tuple<ClassDeclarationSyntax,INamedTypeSymbol>? GetSemanticTargetForGeneration(GeneratorSyntaxContext ctx)
    {
        var cds = ctx.Node as ClassDeclarationSyntax;
        if(cds == null ) return null;
        
        if (cds.Parent != null && (cds.Parent is not BaseNamespaceDeclarationSyntax)) return null;
        var data = ctx.SemanticModel.GetDeclaredSymbol(cds) ;
        if(data == null) return null;
        return new Tuple<ClassDeclarationSyntax, INamedTypeSymbol>(cds,data);

    }

    private bool IsSyntaxTargetForGeneration(SyntaxNode s)
    {
        if(s is not ClassDeclarationSyntax cds) return false;
        if(cds.AttributeLists.Count == 0) return false;
        return cds.AttributeLists.Any(it => it.ToFullString().Trim().Contains("IGenerateDataFromClass"));
       
    }
}
