using System;

namespace DemoNetCoreClickOnceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello World from {ThisAssembly.Project.AssemblyName} version {ThisAssembly.Info.Version} ");
        }
    }
}
