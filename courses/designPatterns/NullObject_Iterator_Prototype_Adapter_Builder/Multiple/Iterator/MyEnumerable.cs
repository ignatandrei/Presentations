using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    partial class Program
    {
        class MyEnumerable : IEnumerable<int> //List<int>
        {
            List<int> arr = new List<int> { 1, 2, 10 };
            public IEnumerator<int> GetEnumerator()
            {
                return arr.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return arr.GetEnumerator();
            }
        }
    }
}
