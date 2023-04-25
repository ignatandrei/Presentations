using DiDemoObjects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIDemo
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
            var di = new ServiceCollection()           
           .AddSingleton<RepositoryService, RepositoryService>()           
           .AddSingleton<PersonList, PersonList>()
           .BuildServiceProvider();


            var ps = di.GetService<PersonList>();
            ps.Retrieve();
            Console.WriteLine(ps.Count);


        }
    }
}
