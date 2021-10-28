using System;

namespace Console_TimeBombComment
{
    partial class Program
    {
        
        static void Main(string[] args)
        {
            //TB: 2021-09-13 this is a comment transformed into an error
            //TB: and this is a warning
            //TB: 9999-12-30 and this will not appear
            Console.WriteLine("See the TB comment above ? ");

            //and here Test1 with Obsolete
            Console.WriteLine(Test1());
        }
        [Obsolete("should be deleted", TB_20210915)]
        static string Test1()
        {
            return "asdasd";
        }
    }
}
