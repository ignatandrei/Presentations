using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Iddepartment { get; set; }

        public virtual Department IddepartmentNavigation { get; set; }
    }
}
