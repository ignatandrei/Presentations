using System;

namespace DemoNetCoreClickOnceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
//            2020.111.114.1018

//https://github.com/ignatandrei/presentations/clickonce/democonsole
            Console.WriteLine($"Hello World from {ThisAssembly.Project.AssemblyName} version {ThisAssembly.Info.Version} ");
        }
    }
}
