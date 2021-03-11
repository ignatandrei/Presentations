using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;

namespace RSCG_Simple
{
    [Generator]
    public partial class SimpleGenerator : ISourceGenerator
    {
        static Diagnostic DoDiagnostic(DiagnosticSeverity ds, string message)
        {
            //info  could be seen only with 
            // dotnet build -v diag
            var dd = new DiagnosticDescriptor("SimpleGenerator1", $"StartExecution", message, "SimpleGenerator2", ds, true);
            var d = Diagnostic.Create(dd, Location.None, "andrei.txt");
            return d;
        }
        public void Execute(GeneratorExecutionContext context)
        {
            var text = @"
                using System;
                public class MyTest{
                    public static void WriteDate(){
                        System.Diagnostics.Debugger.Break();
                        Console.WriteLine(DateTime.Now.ToString());
                    }
                }";
            context.AddSource("myTest.cs", text);
            context.ReportDiagnostic(
                DoDiagnostic(DiagnosticSeverity.Warning,$"this is a warning")
                );


        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //Debugger.Launch();
        }
    }


}
