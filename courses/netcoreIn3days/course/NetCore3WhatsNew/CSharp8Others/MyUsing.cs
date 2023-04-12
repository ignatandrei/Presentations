using System;

namespace CSharp8Others
{
    class MyUsing : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("disposed");
        }

        internal void WriteText()
        {
            Console.WriteLine("inside using");
        }
    }
}
