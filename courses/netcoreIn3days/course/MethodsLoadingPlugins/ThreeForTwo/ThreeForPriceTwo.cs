using MethodsLoadingPlugins;
using System;
using System.Linq;

namespace ThreeForTwo
{
    public class ThreeForPriceTwo : Calculate
    {
        public bool CanCalculate(Order o)
        {
            return o.lines
                     .Where(it => it.Number > 2)
                     .Any();
        }

        public double NewAmount(Order o)
        {
            Console.WriteLine($"entered plugin  {nameof(ThreeForPriceTwo)}  with amount {o.Amount()}");
            var amount = o.Amount() -20;
            Console.WriteLine($"exit plugin  {nameof(ThreeForPriceTwo)}  with amount {amount}");
            return amount;

        }
    }
}
