﻿using System;
using System.Collections.Generic;

namespace EFCoreDemo;

public partial class Department
{
    public long Iddepartment { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
