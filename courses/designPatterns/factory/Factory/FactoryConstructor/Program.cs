using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryConstructor
{
    class Program
    {
        static void Main(string[] args)
        {
            FactoryTimeSpan();
            FactoryConvert();
            FactoryWebRequest();

        }

        private static void FactoryWebRequest()
        {
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create("ftp://www.yahoo.com");
            HttpWebRequest hwr= (HttpWebRequest)WebRequest.Create("http://www.yahoo.com");
        }

        private static void FactoryConvert()
        {
            Console.WriteLine(Convert.ToInt32("1"));
            string value = "1,500";
            //Console.WriteLine(Convert.ToDouble(value));

            #region stayHidden
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");

            Console.WriteLine(Convert.ToDouble(value));

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
            //Thread.CurrentThread.CurrentUICulture= CultureInfo.CreateSpecificCulture("fr-FR");
            Console.WriteLine(Convert.ToDouble(value));
            #endregion
        }

        static void FactoryTimeSpan()
        {
            var ts = new TimeSpan(15000);
            //it is the same?
            var ts1 = TimeSpan.FromMilliseconds(15000);
            //look at MyTimeSpan - source of TimeSpan
            Console.WriteLine(ts.TotalMilliseconds);
            Console.WriteLine(ts1.TotalMilliseconds);
            
            //factory
        }
        public class TimeSpanFactory
        {
            public static TimeSpan FromMilliSeconds(double value)
            {
                //
                return TimeSpan.FromMinutes(1000);
            }
        }
    }


}
