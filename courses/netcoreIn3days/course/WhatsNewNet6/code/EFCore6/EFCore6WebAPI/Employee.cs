#nullable disable
namespace EFCore6WebAPI;

public partial class Employee
{
    public long Idemployee { get; set; }
    public string Name { get; set; }
    public long Iddepartment { get; set; }

    public virtual Department IddepartmentNavigation { get; set; }
}

