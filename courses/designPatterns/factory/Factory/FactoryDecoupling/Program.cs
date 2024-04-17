using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FactoryDecoupling
{
    

    public class City
    {
        public City(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Person p1=new Person("Andrei","Cara Anghel Street ...");
            Console.WriteLine(p1.Adress.Adress);

            //ctor 2
            Person p2 = new Person("Andrei", "Cara Anghel Street ...", new City("Bucuresti"));
            Console.WriteLine(p1.Adress.Adress);
            //ctor 3
            var factory = AdressFactoryProvider.GetAdressFactoryForCity("Bucuresti");
            //we could add city, and so on
            Person p3 = new Person("Andrei", "Cara Anghel Street ...", factory);
            Console.WriteLine(p1.Adress.Adress);


        }
    }
}
