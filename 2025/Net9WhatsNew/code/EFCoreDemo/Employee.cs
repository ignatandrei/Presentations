using System;
using System.Collections.Generic;

namespace EFCoreDemo;

public partial class Employee
{
    public long Idemployee { get; set; }

    public string Name { get; set; } = null!;

    public long Iddepartment { get; set; }

    public long Salary { get; set; }

    public virtual Department IddepartmentNavigation { get; set; } = null!;
}
