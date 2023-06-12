using System;
using System.Collections.Generic;

namespace DalManual;

public partial class Department
{
    public long Iddepartment { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employee { get; } = new List<Employee>();
}
