using PluginInterfaces;
using System;

namespace PluginA
{
    /// <summary>
    /// https://www.nuget.org/packages/DiamondD1_2/1.0.0
    /// </summary>
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
