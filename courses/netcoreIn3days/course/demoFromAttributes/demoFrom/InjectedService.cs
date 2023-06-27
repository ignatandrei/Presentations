using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace demoFrom
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class InjectedService
    {
        static int id = 0;
        public InjectedService()
        {
            this.MyId=Interlocked.Increment(ref id);

        }
        
        public int MyId { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"ID={MyId}";
        }
    }
}
