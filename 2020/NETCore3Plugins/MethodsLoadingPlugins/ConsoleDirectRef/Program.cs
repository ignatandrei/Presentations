using MethodsLoadingPlugins;
using MoreThan100;
using System;

namespace ConsoleDirectRef
{
    class Program
    {
        static void Main(string[] args)
        {
            var plugin = new DeductionMore();
            var o = new Order(plugin);
            o.lines.Add(new LineOrder()
            {
                Number = 10,
                sellGood = new SellGood()
                {
                    Name = "bread",
                    Price = 10
                }
            });

            Console.WriteLine(o.CalculateAmount());
        }
    }
}
