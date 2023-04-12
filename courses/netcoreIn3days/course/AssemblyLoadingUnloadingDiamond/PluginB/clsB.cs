using PluginInterfaces;
using System;

namespace PluginB
{
    /// <summary>
    /// https://www.nuget.org/packages/DiamondD1_2/2.0.0
    /// </summary>
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
