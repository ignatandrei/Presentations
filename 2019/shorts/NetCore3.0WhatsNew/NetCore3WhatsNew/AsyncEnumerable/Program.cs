using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    class Program
    {
        /// <summary>
        /// see also https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/generate-consume-asynchronous-stream
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var context = new MyDataContext())
            {
                
                for (int i = 0; i < 1000; i++)
                {
                    var p = new PersonWithBlog();
                    //p.Id = i;
                    p.Name = "Andrei Ignat : " +i;
                    p.Url = "http://msprogrammer.serviciipeweb.ro/";
                    context.PersonWithBlog.Add(p);
                    //Console.WriteLine(p.Id);
                }
                
                await context.SaveChangesAsync();
                await foreach(var q in GetPerson(context, 997))
                {
                    Console.WriteLine("      receiving " + q.Name);
                }
                
                
            }

            
        }
        static async IAsyncEnumerable<PersonWithBlog> GetPerson(MyDataContext cnt,  int from)
        {
            var query = cnt.PersonWithBlog.Where(it => it.Id > from);
            await foreach (var p in query.AsAsyncEnumerable())
            {
                
                Console.WriteLine($"from database {p.Name}");
                //simulate processing
                p.Id = p.Id + 1;
                yield return p;
            }
        }
    }
}
