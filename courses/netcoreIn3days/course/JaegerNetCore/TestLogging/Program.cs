using NLog;
using NLog.Config;
using System;

namespace TestLogging
{
    class Program
    {
        /// <summary>
        /// docker run -p 5601:5601 -p 9200:9200 -p 5044:5044 -it --name elk sebp/elk
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");

            var l = LogManager.GetCurrentClassLogger();
            while (true)
            {
                var rnd = new Random();
                var number = rnd.Next(0, 6);
                var level = LogLevel.FromOrdinal(number);


                l.Error("I am at  " + DateTime.Now.ToString("r"));


                l.Log(level, $" this is a random level {number} {DateTime.Now.ToString("s")} ");
                Console.ReadLine();
            }

        }
    }
}
