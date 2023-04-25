using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator2Sealed
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var con = new OleDbConnection())
            {
                using(var b=new DbConnectionLogging(con))
                {
                    Console.WriteLine(con.State);
                    Console.WriteLine(b.State);

                }
            }

            
        }
    }
}
