using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace StrategyNET
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> al=new List<int>();
            al.Add(102);
            al.Add(201);
            //sort ascending
            al.Sort((x,y)=>x.CompareTo(y));
            
            for (int i = 0; i < al.Count; i++)
            {
                Console.WriteLine(al[i]);
            }

            Console.WriteLine("---------------");

            //sort descending
            al.Sort((y, x) => x.CompareTo(y));
            for (int i = 0; i < al.Count; i++)
            {
                Console.WriteLine(al[i]);
            }
            Console.WriteLine("---------------");
            //sort custom
            al.Sort((x, y) =>LastDigit(x).CompareTo(LastDigit(y))  );
            for (int i = 0; i < al.Count; i++)
            {
                Console.WriteLine(al[i]);
            }

            var array = al.FindAll(it => it > 10);


        }

        static int LastDigit(int x)
        {
            return x % 10;
        }
    }
}
