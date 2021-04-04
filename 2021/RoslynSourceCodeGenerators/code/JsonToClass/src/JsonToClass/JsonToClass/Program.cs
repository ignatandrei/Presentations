using System;
using Microsoft.Extensions.Configuration;
using JsonToClass.Json.Persons;
namespace JsonToClass
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Person();
            p1.Blog = "http://msprogrammer.serviciipeweb.ro/";
            var config = new ConfigurationBuilder()
              .AddJsonFile("persons.json")
              .Build();

            var p = config.Get<Person>();
            Console.WriteLine($"{p.FirstName}");

            var p2 = Person.FromConfig(config);
            //Console.Write(p2.FirstName);
        }
    }
}
