using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// https://assist-software.net/blog/implementation-decorator-pattern-c
/// </summary>
namespace Decorator3Coffee
{
    public interface ICoffee
    {
        string GetDescription();
        double GetCost();
    }
    public class Filtered: ICoffee
    {
        public string GetDescription()
        {
            return "Filtered with care";
        }

        public double GetCost()
        {
            return 1.99;
        }
    }

    public class Espresso : ICoffee
    {
        public string GetDescription()
        {
            return "Espresso made with care";
        }

        public double GetCost()
        {
            return 2.99;
        }
    }
    public abstract class CondimentDecorator : ICoffee
    {
        ICoffee _coffee;

        protected string _name = "undefined condiment";
        protected double _price = 0.0;

        public CondimentDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public string GetDescription()
        {
            return string.Format("{0}, {1}", _coffee.GetDescription(), _name);
        }

        public double GetCost()
        {
            return _coffee.GetCost() + _price;
        }
    }
    public class MilkDecorator : CondimentDecorator
    {
        public MilkDecorator(ICoffee coffee)
            : base(coffee)
        {
            _name = "Milk";
            _price = 0.19;
        }
    }

    public class ChocolateDecorator : CondimentDecorator
    {
        public ChocolateDecorator(ICoffee coffee)
            : base(coffee)
        {
            _name = "Chocolate";
            _price = 0.29;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var coffee = new ChocolateDecorator(new MilkDecorator(new Espresso()));
            Console.WriteLine(coffee.GetDescription());
        }
    }
}
