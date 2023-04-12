using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace demoFrom
{
    public class InjectedService
    {
        static int id = 0;
        public InjectedService()
        {
            this.MyId=Interlocked.Increment(ref id);

        }
        
        public int MyId { get; set; }
    }
}
