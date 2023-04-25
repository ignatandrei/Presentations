using System;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace Adaptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainSqliteAdapterAsync().GetAwaiter().GetResult();
            AdapterStringByte();
            FuncDelegateAdapter();
        }
        //not static
        public delegate void MyDel(int number);
        static void Example(int nr)
        {
            Console.WriteLine($"from {nameof(Example)} with argument {nr}");
        }


        static void FuncDelegateAdapter()
        {
            
            MyDel d = Example;
            d += Example;
            d(10);
            






        }

        /// <summary>
        ///Adaptee  - string 
        ///Target  - bytes
        ///Adapter  - encoding
        ///Target Method - GetBytes
        /// </summary>
        static void AdapterStringByte()
        {
            var url = "http://msprogrammer.serviciipeweb.ro";
            Encoding e = new ASCIIEncoding();
            var b= e.GetBytes(url);
            
            Console.WriteLine($"from {e.EncodingName} number bytes {b.Length}");
            e = new UTF32Encoding();
            b = e.GetBytes(url);
            Console.WriteLine($"from {e.EncodingName} number bytes {b.Length}");

        }
        /// <summary>
        ///Adaptee  - Command 
        ///Target  - DataTable
        ///Adapter  - SqlDataAdapter
        ///Target Method - Fill(Dataset instance)
        /// </summary>
        /// <returns></returns>
        static async Task MainSqliteAdapterAsync()
        {
            var dataFormats= new DataTable();
            Console.WriteLine(dataFormats.Rows.Count);
            Console.WriteLine(dataFormats.Columns.Count);
            using (var con = new SQLiteConnection())
            {
                con.ConnectionString = "Data Source=CatalogRo.sqlite3";
                await con.OpenAsync();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.CommandText = "select * from Format";
                    cmd.Connection = con;
                    using (var adapt = new SQLiteDataAdapter(cmd))
                    {
                        adapt.Fill(dataFormats);
                    }
                }

            }
            Console.WriteLine(dataFormats.Rows.Count);
            Console.WriteLine(dataFormats.Columns.Count);
        }
    }
}
