using McMaster.NETCore.Plugins;
using PluginInterfaces;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

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
            Console.WriteLine("Hello plugins! -see memory in task manager");
            string PluginBPath = Path.Combine(Environment.CurrentDirectory, "pluginB");
            Console.ReadKey();
            LoadAssemblyA();
            Console.WriteLine("start collect after unload");
            for (int i = 0; i < 10; i++)
            {


                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(1000);
            }
            Console.WriteLine("after unload");
            Console.ReadKey();
            {
                var tcB = new TestAssemblyLoadContext(PluginBPath);
                tcB.GetMyPlugin().LoadData();
                tcB.Unload();
            }
            for (int i = 0; i < 10; i++)
            {


                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(1000);
            }
            Console.ReadKey();

        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void LoadAssemblyA()
        {
            string PluginAPath = Path.Combine(Environment.CurrentDirectory, "pluginA");
            

            //Ensure that you have copied the pluginA and pluginB
            var tcA = new TestAssemblyLoadContext(PluginAPath);
            var s = tcA.GetMyPlugin();
            s.LoadData();
            Console.WriteLine("before unload -see task manager memory");
            Console.ReadKey();
            tcA.Unload();



        }
    }
}
