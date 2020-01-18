using System.Collections.Generic;

namespace MethodsLoadingPlugins
{
    public class Order
    {
        public Order(params Calculate[] calculates)
        {
            Calculates = calculates;
            lines = new List<LineOrder>(); 
        }
        public List<LineOrder> lines { get; set; }
        public Calculate[] Calculates { get; }

        public double Amount()
        {
            double amount = 0;
            foreach (var line in lines)
            {
                amount += line.sellGood.Price * line.Number;
            }
            return amount;
        }
        public double CalculateAmount() {
            double amount = Amount();
            foreach (var calc in Calculates)
            {
                if (calc.CanCalculate(this))
                {
                    return calc.NewAmount(this);
                }
            }

            return amount;

        }

    }


}
