using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;

namespace RSCG_Simple
{
    [Generator]
    public partial class SimpleGenerator : ISourceGenerator
    {
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
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //Debugger.Launch();
        }
    }


}
