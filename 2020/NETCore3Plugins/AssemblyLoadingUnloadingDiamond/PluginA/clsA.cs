using PluginInterfaces;
using System;
using System.Collections.Generic;

namespace PluginA
{
    /// <summary>
    /// https://www.nuget.org/packages/DiamondD1_2/1.0.0
    /// </summary>
    public class clsA : IPlugin
    {
        public clsA()
        {
            for (int i = 0; i < 1000000; i++)
            {
                lst.Add("ASDFASDASDASDSd");
            }
        }
        public List<string> lst = new List<string>();
        public string Name { get =>  "clsA";  }

        public string LoadData()
        {
            var diamond = new DiamondD1_2.clsTest();
            Console.WriteLine($"From A {diamond.GetMyBlog()}");
            return $"From A ";
        }
    }
}
