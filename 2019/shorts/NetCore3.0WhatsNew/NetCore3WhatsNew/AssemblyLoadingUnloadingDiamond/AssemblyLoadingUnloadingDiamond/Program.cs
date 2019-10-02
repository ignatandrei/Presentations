using System;
using System.IO;

namespace AssemblyLoadingUnloadingDiamond
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string PluginAPath =Path.Combine( Environment.CurrentDirectory , "pluginA");
            string PluginBPath = Path.Combine(Environment.CurrentDirectory , "pluginB");
            var tc = new TestAssemblyLoadContext(PluginAPath);
            tc.GetMyPlugin().LoadData();
            tc.Unload();


        }
    }
}
