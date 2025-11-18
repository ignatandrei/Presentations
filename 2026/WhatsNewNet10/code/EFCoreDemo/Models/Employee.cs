using System;
using System.Collections.Generic;

namespace EFCoreDemo.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public DateOnly? TerminationDate { get; set; }

    public virtual Department? Department { get; set; }
}
