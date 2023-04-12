using AsyncAwait;
using System;
using System.Threading.Tasks;

namespace ConsoleAsyncAwait
{
    class Program
    {     
        //convert to async
        async static Task Main(string[] args)
        {
            var t = new TwoTasks();

            var res = await t.Await2Task();
                //.GetAwaiter().GetResult();

            Console.WriteLine(res);
            
        }
    }
}
