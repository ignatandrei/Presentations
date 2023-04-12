using MethodsLoadingPlugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ConsoleLoadPlugin
{
    class PluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        public PluginLoadContext(string pluginPath)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Calculate[] plugins = LoadPlugins();
            Console.WriteLine($" number of plugins : {plugins.Length}");
            var o = new Order(plugins);
            o.lines.Add(new LineOrder()
            {
                Number = 10,
                sellGood = new SellGood()
                {
                    Name = "bread",
                    Price = 15
                }
            });

            Console.WriteLine(o.CalculateAmount());
        }

        private static Calculate[] LoadPlugins()
        {
            var list = new List<Calculate>();
            var f = Directory.GetFiles("plugins", "*.dll");
            foreach(var item in f)
            {
                Assembly a = Assembly.LoadFrom(item);
                var types = a.GetTypes();
                //var p = new PluginLoadContext(item);
                //var ass= p.LoadFromAssemblyName()
                //var types = p.Assemblies.SelectMany(it => it.GetTypes()).ToArray();
                var s = types.Count();
                //var calcs = types.Where(it => it.IsAssignableFrom(typeof(Calculate))). Cast<Calculate>().ToArray();
                var calcs = types.Where(it =>
                {
                    try
                    {
                        var s = a.CreateInstance(it.FullName);
                        bool IsCalculate= s is Calculate c;
                        return IsCalculate;
                    }
                    catch
                    {
                        return false;
                    }
                })
                    .Select(it=> (Calculate)a.CreateInstance(it.FullName))
                    .ToArray();

                if (calcs.Length > 0)
                    list.AddRange(calcs);

            }
            return list.ToArray();
        }
    }
}
