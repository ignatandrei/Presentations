using System;
using System.IO;

namespace AssemblyLoadingUnloadingDiamond
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support
    /// please use https://github.com/natemcmaster/DotNetCorePlugins
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello plugins!");
            string PluginAPath =Path.Combine( Environment.CurrentDirectory , "pluginA");
            string PluginBPath = Path.Combine(Environment.CurrentDirectory , "pluginB");
            var tcA = new TestAssemblyLoadContext(PluginAPath);
            tcA.GetMyPlugin().LoadData();
            tcA.Unload();

            var tcB = new TestAssemblyLoadContext(PluginBPath);
            tcB.GetMyPlugin().LoadData();
            tcB.Unload();


        }
    }
}
