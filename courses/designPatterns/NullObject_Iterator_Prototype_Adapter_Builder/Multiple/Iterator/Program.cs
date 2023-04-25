using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Iterator
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var list = new List<int>() { 1, 2, 30 };
            foreach(var item in list)
            {
                Console.WriteLine($"from foreach:{item}");
            }
            list.ForEach(it =>
            {
                Console.WriteLine($"from ForEach {it}");
            });

            foreach(var item in new MyEnumerable())
            {
                Console.WriteLine($"from MyEnumerable {item}");
            }
            Console.WriteLine(YieldExample().First());
            Console.WriteLine(YieldExample().Count());
            Console.WriteLine(YieldExample().First());
            //Directory.EnumerateFiles
            //Directory.GetFiles
        }
        static IEnumerable<int> YieldExample()
        {
            Console.WriteLine("first");
            yield return 10;
            Console.WriteLine("next");
            yield return 20;
        }
    }
}
