using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreDemo
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public long Iddepartment { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
