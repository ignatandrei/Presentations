using System;

namespace CSharp8Others
{
    class Program
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            UsingUsage();
            StaticLocalFunctions();
            Nullable();
            Version();
        }

        private static void Version()
        {
            System.Console.WriteLine($"Environment.Version: {System.Environment.Version}");

            // Old result
            //   Environment.Version: 4.0.30319.42000
            //
            // New result
            //   Environment.Version: 3.0.0
        }

        private static void Nullable()
        {
            //https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references
            //see csproj file , nullable
            string? x = GetMyX();
            Console.WriteLine(x);
        }
        static string? GetMyX()
        {
            return null;
        }
        private static void StaticLocalFunctions()
        {
            var x = add(10, 20);
            var y = 1;
            Console.WriteLine(y);
            static int add(int a, int b)
            {
                //y = 10;
                return a + b;
                
            }
        }

        private static void UsingUsage()
        {
            using(var x=new MyUsing())
            {
                x.WriteText();
            }

            Console.WriteLine("outside");
            using var y = new MyUsing();
            y.WriteText();
            Console.WriteLine("STILL inside");
        }
    }
}
