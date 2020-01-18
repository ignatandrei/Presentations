using MethodsLoadingPlugins;
using MoreThan100;
using System;
using ThreeForTwo;

namespace ConsoleDirectRef
{
    class Program
    {
        static void Main(string[] args)
        {
            var plugin1 = new DeductionMore();
            var plugin2 = new ThreeForPriceTwo();
            var o = new Order(plugin1,plugin2);
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
    }
}
