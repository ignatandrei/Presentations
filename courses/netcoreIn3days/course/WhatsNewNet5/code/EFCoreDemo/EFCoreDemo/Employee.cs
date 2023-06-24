using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreDemo
{
    public partial class Employee
    {
        public long Idemployee { get; set; }
        public string Name { get; set; }
        public long Iddepartment { get; set; }

        public virtual Department IddepartmentNavigation { get; set; }
    }
}
