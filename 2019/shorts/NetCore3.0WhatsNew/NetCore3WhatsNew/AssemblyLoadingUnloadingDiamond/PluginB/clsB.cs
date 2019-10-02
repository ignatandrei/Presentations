using PluginInterfaces;
using System;

namespace PluginB
{
    public class clsB : IPlugin
    {
        public string Name { get => "clsB"; }
        public string LoadData()
        {
            Console.WriteLine("From B");
            return "From B";
        }
    }
}
