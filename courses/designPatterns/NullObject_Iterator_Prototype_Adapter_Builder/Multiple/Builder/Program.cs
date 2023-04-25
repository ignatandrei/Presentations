using System;
using System.Data.SqlClient;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionString();
            UriMod();
        }

        private static void UriMod()
        {
            var uri = new Uri("http://msprogrammer.serviciipeweb.ro/category/friday-links/");
            //uri.Scheme = "https";
            var b = new UriBuilder(uri);
            b.Scheme = "https";
            Console.WriteLine(b.Uri);
            b.Path = "2018/03/05/design-patterns-class/";
            b.Scheme = "http";
            Console.WriteLine(b.Uri);
        }

        static void ConnectionString()
        {
            var build = new SqlConnectionStringBuilder();
            build.DataSource = ".";
            build.InitialCatalog = "MyDatabase";
            build.ConnectTimeout = 30;
            Console.WriteLine(build.ConnectionString);
        }
    }
}
