using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Factory1Enumerator();
            Factory2DBCommand();

        }

        static string ProviderName()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["myConnection"];
            //if (connectionString != null)
            //    return connectionString.ProviderName;

                var item = DbProviderFactories.GetFactoryClasses().Rows[0]["InvariantName"];
                return item.ToString();

        }
        static void Factory2DBCommand()
        {
            
            
            var providerName = ProviderName();
            var factory = DbProviderFactories.GetFactory(providerName);
            {
                using (var cn = factory.CreateConnection())
                {
                    //create command creates a DBCommand specific to the provider
                    using (var cmd = cn.CreateCommand())
                    {
                        //the create parameter method is from abstract DBCommand class
                        var p = cmd.CreateParameter();

                    }
                }
            }
        }

        static void Factory1Enumerator()
        {
            IEnumerator enumerator = null;

            List<int> myList = new List<int>();
            myList.Add(1);
            enumerator = ((IEnumerable)myList).GetEnumerator();
            Console.WriteLine(enumerator.GetType());

            var arr = new[] { 1 };
            enumerator = ((IEnumerable)arr).GetEnumerator();
            Console.WriteLine(enumerator.GetType());

            var q = new Queue();
            q.Enqueue(1);
            enumerator = ((IEnumerable)q).GetEnumerator();
            Console.WriteLine(enumerator.GetType());

        }
        static void Display(IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
    }
}
