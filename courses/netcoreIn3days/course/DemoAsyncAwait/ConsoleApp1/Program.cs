using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var h = new HttpClient())
            {

            }
            
            //try
            //{

            //}
            //catch()
            //{

            //}
            //finally{
            //    //h.Dispose
            //}
                
                var x = 1;
            x++;
            Console.WriteLine(x);
        }
    }
}
