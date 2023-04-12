using System;

namespace CSharp9WhatsNew
{
    record Person(string firstName, string lastName)
    {
        public int x;
        public string CNP { get; init; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var p1 = new Person("andrei", "ignat") { CNP = "1" };
            var p2= new Person("andrei", "ignat") { CNP = "1" };
            Console.WriteLine(p1 == p2);
            Console.WriteLine(Object.ReferenceEquals(p1,p2));
            p1.x = 10;
            Console.WriteLine(p1 == p2);
            //p1.CNP = "asd";
            Program p3 = new();
            var p4 = new Program();

        }
    }
}
