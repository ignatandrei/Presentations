using MethodsLoadingPlugins;
using System;

namespace ConsoleLoadPlugin
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculate[] plugins = LoadPlugins();
            var o = new Order(plugins);
            o.lines.Add(new LineOrder()
            {
                Number = 10,
                sellGood = new SellGood()
                {
                    Name = "bread",
                    Price = 15
                }
            });

            Console.WriteLine(o.CalculateAmount());
        }

        private static Calculate[] LoadPlugins()
        {
            return null;
        }
    }
}
