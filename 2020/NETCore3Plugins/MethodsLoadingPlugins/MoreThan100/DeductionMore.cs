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
            return o.Amount() * 90 / 100;
        }
    }
}
