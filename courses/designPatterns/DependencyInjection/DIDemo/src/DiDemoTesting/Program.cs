using DiDemoObjects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DiDemoTesting
{
    class MyReposityService: RepositoryService
    {
        public override List<Person> RetrievePersons()
        {
            return new List<Person>();
        }
    }
    public class Program
    {

        public static void Main(string[] args)
        {

            var di = new ServiceCollection()
           .AddSingleton<RepositoryService, MyReposityService>()
           .AddSingleton<PersonList, PersonList>()
           .BuildServiceProvider();


            var ps = di.GetService<PersonList>();
            ps.Retrieve();
            Debug.Assert(ps.Count == 0);
            Console.WriteLine(ps.Count);
            

        }
    }
}
