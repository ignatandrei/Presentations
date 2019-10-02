using PluginInterfaces;
using System;

namespace PluginA
{
    public class clsA : IPlugin
    {
        public string Name { get =>  "clsA";  }

        public string LoadData()
        {
            var diamond = new DiamondD1_2.clsTest();
            Console.WriteLine($"From A {diamond.GetMyBlog()}");
            return $"From A ";
        }
    }
}
