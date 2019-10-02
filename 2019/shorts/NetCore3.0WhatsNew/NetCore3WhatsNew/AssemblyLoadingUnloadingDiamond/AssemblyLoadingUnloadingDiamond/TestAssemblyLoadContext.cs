using PluginInterfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AssemblyLoadingUnloadingDiamond
{
    //https://www.strathweb.com/2019/01/collectible-assemblies-in-net-core-3-0/
    //https://docs.microsoft.com/en-us/dotnet/standard/assembly/unloadability
    class TestAssemblyLoadContext : AssemblyLoadContext
    {
        private readonly string mainAssemblyToLoadPath;
        private readonly string namePlugin;
        private AssemblyDependencyResolver _resolver;

        public TestAssemblyLoadContext(string mainAssemblyToLoadPath) : base(isCollectible: true)
        {
            this.mainAssemblyToLoadPath = mainAssemblyToLoadPath;
            _resolver = new AssemblyDependencyResolver(mainAssemblyToLoadPath);
            namePlugin = new DirectoryInfo(mainAssemblyToLoadPath).Name;
        }
        public IPlugin GetMyPlugin()
        {
            
            var file = Path.Combine(mainAssemblyToLoadPath, namePlugin + ".dll");
            //using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //  var assembly = this.LoadFromStream(fs);
                var assembly = this.LoadFromAssemblyName(new AssemblyName(namePlugin));
                var type = assembly.GetTypes();
                
                foreach (var t in type)
                {
                    var instance = Activator.CreateInstance(t);
                    var q = instance as IPlugin;
                    return q;
                }


                return null;
            }

        }
        protected override Assembly Load(AssemblyName name)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(name);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }
            string fullPossibleName = Path.Combine(mainAssemblyToLoadPath, name.Name + ".dll");
            if (File.Exists(fullPossibleName))
            {
                return this.LoadFromAssemblyPath(fullPossibleName);
            }
            return null;
        }
    }
}
