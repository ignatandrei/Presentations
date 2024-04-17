using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FactoryAbstract
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory1DbProvider();
            Factory2MVC();
        }

        private static void Factory2MVC()
        {            
            ControllerBuilder.Current.SetControllerFactory(typeof(MyControllerFactory));
        }

        static string ProviderName()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["myConnection"];
            //if (connectionString != null)
            //    return connectionString.ProviderName;

            var item = DbProviderFactories.GetFactoryClasses().Rows[0]["InvariantName"];
            return item.ToString();

        }

        static void Factory1DbProvider()
        {


            var providerName = ProviderName();
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            {
                using (var cn = factory.CreateConnection())
                {
                }
            }
        }
    }
}
