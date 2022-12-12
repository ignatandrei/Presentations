using System;
using System.Collections.Generic;

namespace EFCoreDemos;

public partial class Employee
{
    public long Idemployee { get; set; }

    public string Name { get; set; } = null!;

    public long Iddepartment { get; set; }

    public virtual Department IddepartmentNavigation { get; set; } = null!;
}
