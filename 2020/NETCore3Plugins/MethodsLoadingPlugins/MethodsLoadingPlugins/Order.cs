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
        private double? amount;
        public double Amount()
        {
            if (amount.HasValue)
                return amount.Value;

            amount = 0;
            foreach (var line in lines)
            {
                amount += line.sellGood.Price * line.Number;
            }
            return amount.Value;
        }
        public double CalculateAmount() {
            amount = Amount();
            foreach (var calc in Calculates)
            {
                if (calc.CanCalculate(this))
                {
                    amount = calc.NewAmount(this);
                }
            }

            return amount.Value;

        }

    }


}
