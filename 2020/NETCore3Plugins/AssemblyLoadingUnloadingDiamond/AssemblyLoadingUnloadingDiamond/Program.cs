using System;
using System.IO;

namespace AssemblyLoadingUnloadingDiamond
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support
    /// https://github.com/dotnet/samples/tree/master/core/tutorials/Unloading
    /// please use https://github.com/natemcmaster/DotNetCorePlugins
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello plugins!");
            //Ensure that you have copied the pluginA and pluginB
            string PluginAPath =Path.Combine( Environment.CurrentDirectory , "pluginA");
            string PluginBPath = Path.Combine(Environment.CurrentDirectory , "pluginB");
            var tcA = new TestAssemblyLoadContext(PluginAPath);
            tcA.GetMyPlugin().LoadData();
            GC.Collect();
            Console.WriteLine("before unload");
            Console.ReadKey();
            tcA.Unload();
            Console.WriteLine("after unload");
            GC.Collect();
            Console.ReadKey();

            var tcB = new TestAssemblyLoadContext(PluginBPath);
            tcB.GetMyPlugin().LoadData();
            tcB.Unload();


        }
    }
}
