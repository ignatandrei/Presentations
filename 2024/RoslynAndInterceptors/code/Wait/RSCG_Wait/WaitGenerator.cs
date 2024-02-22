namespace RSCG_Wait;
[Generator]
public class WaitGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var data= context.AnalyzerConfigOptionsProvider.Select((it,_)=>
        {
            //HomeWork: find all values from the keys
            //it.GlobalOptions.Keys
            //and add them to the source output with their values
            if(it.GlobalOptions.TryGetValue("build_property.RSCG_Wait_Seconds", out var value))
            {
                if(int.TryParse(value, out var result))
                {
                    return result;
                }
            }
            return 10;
        });

       
        context.RegisterSourceOutput(data, GenerateData);
    }

    private void GenerateData(SourceProductionContext context, int secondsToWait)
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/raw-string-literal
        context.AddSource("WaitGeneratorStart.g", $$"""
namespace RSCG_Wait;
public partial class MyGeneratedCode
{
    public static string DateStart => "{{DateTime.Now.ToString()}}";
    public static int SecondsToWait={{secondsToWait}};
}
""");
        Thread.Sleep(secondsToWait*1000);
        context.AddSource("WaitGeneratorEnd.g", $$"""
namespace RSCG_Wait;
public partial class MyGeneratedCode
{
    public static string DateEnd => "{{DateTime.Now.ToString()}}";
    
}
""");
        //var x = 1;
    }
}
