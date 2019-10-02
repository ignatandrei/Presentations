using PluginInterfaces;
using System;

namespace PluginB
{
    public class clsB : IPlugin
    {
        public string Name { get => "clsB"; }
        public string LoadData()
        {
            var diamond = new DiamondD1_2.clsTest();
            Console.WriteLine($"From B {diamond.GetMyBlog("http://serviciipeweb.ro/propriu/")}");
            return $"From B ";
        }
    }
}
