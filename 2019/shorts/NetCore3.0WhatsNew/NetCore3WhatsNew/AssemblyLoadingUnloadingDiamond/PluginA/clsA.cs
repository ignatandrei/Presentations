using PluginInterfaces;
using System;

namespace PluginA
{
    public class clsA : IPlugin
    {
        public string Name { get =>  "clsA";  }

        public string LoadData()
        {
            Console.WriteLine("From A");
            return "From A";
        }
    }
}
