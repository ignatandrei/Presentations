using System;
using System.Collections.Generic;

namespace DBData.genDBModels;

public partial class Employee
{
    public int Id { get; set; }

    public int IdDepartment { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;
}
