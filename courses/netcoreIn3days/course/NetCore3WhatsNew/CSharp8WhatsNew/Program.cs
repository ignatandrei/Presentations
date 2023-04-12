using System;

namespace CSharp8WhatsNew
{
    class Program
    {

        /// <summary>
        /// Reference: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8
        /// https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0
        /// https://devblogs.microsoft.com/dotnet/announcing-net-core-3-0/
        /// edit the .csproj
        /// dotnet publish -c Release -r win-x64 --self-contained true
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //not shown here:Readonly members
            #region default inheritance
            Example1DefaultInterfaceMembers();
            Example2MultipleInheritance();
            #endregion
            #region switch
            //copied
            Example3Switch();
            //original: 
            Example4Deconstruct();
            //see more at https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/pattern-matching
            #endregion
            //not shown here
            //using declarations
            //Static local functions
            //Disposable ref structs
            //Nullable reference types

        }

        private static void Example4Deconstruct()
        {
            var data = FromRGB(new RGBColor(0xFF, 0x00, 0x00));
            Console.WriteLine(data);
        }

        private static void Example3Switch()
        {
            var data = FromRainbow(Rainbow.Blue);

            //TODO: uncomment following line
            //data = FromRainbow(Rainbow.Violet);
        }
        public static Rainbow FromRGB(RGBColor rgb) =>
    rgb switch
    {
        (0xFF, 0x00, 0x00) => Rainbow.Red,
        (0xFF, 0x7F, 0x00) => Rainbow.Orange ,
        _ => throw new ArgumentException(message: $"invalid enum value {rgb}", paramName: nameof(rgb)),
    };
        public static RGBColor FromRainbow(Rainbow colorBand) =>
    colorBand switch
    {
        Rainbow.Red => new RGBColor(0xFF, 0x00, 0x00),
        Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
        Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
        Rainbow.Green => new RGBColor(0x00, 0xFF, 0x00),
        Rainbow.Blue => new RGBColor(0x00, 0x00, 0xFF),
        Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),        
        _ => throw new ArgumentException(message: $"invalid enum value {colorBand}", paramName: nameof(colorBand)),
    };

        private static void Example2MultipleInheritance()
        {
            IOrnitorinc o = new Ornitorinc();
            //ambigous call : https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0121
            //Console.WriteLine(o.baseType());

        }

        static void Example1DefaultInterfaceMembers()
        {
            var pig= new Pig();
            IMamal Mamalpig = pig;
            
            //TODO1: uncomment this line - not for the class
            //Console.WriteLine(pig.baseType());
            Console.WriteLine(Mamalpig.baseType());

            // inheritance on interface            
            //TODO:   uncomment following lines and 
            // IMamal : IAnimal
            //What is this line doing: 
            //IAnimal pig = new Pig();

        }



    }
}
