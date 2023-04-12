using System;
using System.Collections.Generic;

namespace CSharp8Others
{
    class Program
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0
        /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8
        ///  edit the .csproj
        /// dotnet publish -c Release -r win-x64 --self-contained true
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            UsingUsage();
            StaticLocalFunctions();
            Nullable();
            Version();
            Indexes();
            NullCoalescing();
        }

        private static void NullCoalescing()
        {
            List<int> list=new List<int>();
            AddToList(list, 1);
            AddToList(list, 2);
        }
        static void AddToList(List<int> a, int b)
        {
            a ??= new List<int>();
            //other operations

            a.Add(b);

        }

        private static void Indexes()
        {
            var words = new string[]
{
                // index from start    index from end
    "The",      // 0                   ^9
    "quick",    // 1                   ^8
    "brown",    // 2                   ^7
    "fox",      // 3                   ^6
    "jumped",   // 4                   ^5
    "over",     // 5                   ^4
    "the",      // 6                   ^3
    "lazy",     // 7                   ^2
    "dog"       // 8                   ^1
};
            Console.WriteLine($"The last word is {words[^1]}");

            var xAll = words[0..^0];

            var xStart = words[2..];
            var xStart1 = words[^2..];
            var xEnd = words[..2];            
            var xEnd1 = words[^2..];

            var r = 1..4;
            
            var ex = words[r];

            Console.WriteLine($"{words.Length} {xAll.Length}");
            
            



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
            using (var x = new MyUsing())
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
