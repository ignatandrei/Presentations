using MethodsLoadingPlugins;
using System;

namespace MoreThan100
{
    public class DeductionMore : Calculate
    {
        public bool CanCalculate(Order o)
        {
            return o.Amount() >= 100;
        }

        public double NewAmount(Order o)
        {
            Console.WriteLine($"entered plugin  {nameof(DeductionMore)}  with amount {o.Amount()}");
            var amount = o.Amount() * 90 / 100;
            Console.WriteLine($"exit plugin  {nameof(DeductionMore)}  with amount {amount}");
            return amount;
            
        }
    }
}
