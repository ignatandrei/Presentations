using System;

namespace NullObject
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
        // Summary:
        //     Represents the empty string. This field is read-only.
        //public static readonly String Empty;
            var s = String.Empty;
            var x = "";
            Console.WriteLine($"s == x:{s == x}");
            var e = new WithEvents();
            e.x = 100;
            e.ModifyNoArgs += E_ModifyNoArgs;
            e.ModifyX += E_ModifyX;
            e.x = 200;
            

            string x1 = null;
            //null conditional 
            Console.WriteLine($"x1?.Length == null : {x1?.Length == null}");
            //null coalescing
            Console.WriteLine($@"(x1??"").Length : {(x1??"").Length}");

        }

        private static void E_ModifyX(object sender, int e)
        {
            Console.WriteLine($"new value:{e}");
        }

        private static void E_ModifyNoArgs(object sender, EventArgs e)
        {
            
            Console.WriteLine($"e == null:{e == null}");
        }
    }
}
