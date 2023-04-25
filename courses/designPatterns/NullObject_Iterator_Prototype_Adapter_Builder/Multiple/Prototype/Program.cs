using System;

namespace Prototype
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var c = new Child();
            c.Age = 10;
            var p = new Parent();
            p.Age = 50;
            p.MyChild = c;
            var shallow = p.ShallowClone();
            shallow.Age = 51;
            Console.WriteLine($"shallow.Age: {shallow.Age}");
            Console.WriteLine($"p.Age: {p.Age}");
            shallow.MyChild.Age = 11;
            Console.WriteLine($"shallow.MyChild.Age: {shallow.MyChild.Age}");
            Console.WriteLine($"p.MyChild.Age: {p.MyChild.Age}");

            var deep = p.Clone()as Parent;
            Console.WriteLine($"deep.Age: {deep.Age}");
            Console.WriteLine($"p.Age: {p.Age}");
            deep.MyChild.Age = 12;
            Console.WriteLine($"deep.MyChild.Age: {shallow.MyChild.Age}");
            Console.WriteLine($"p.MyChild.Age: {p.MyChild.Age}");



        }
    }
}
