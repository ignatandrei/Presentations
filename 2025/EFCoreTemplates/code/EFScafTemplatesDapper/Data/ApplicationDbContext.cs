namespace EFScafTemplatesDapper.Data;

public partial class ApplicationDbContext 
{

    //public virtual DbSet<Department> Departments { get; set; }
    public static string Department_SelectAll = "SELECT Id, Name FROM Department";
    public static string Department_SelectByPK = "SELECT Id, Name FROM Department WHERE Id = @Id";
    public static string Department_DeleteByPK = "DELETE FROM Department WHERE Id = @Id";
    public static string Department_UpdateByPK = "UPDATE Department SET Name = @Name WHERE Id = @Id";
    //public virtual DbSet<Employee> Employees { get; set; }
    public static string Employee_SelectAll = "SELECT Id, DepartmentId, Name FROM Employee";
    public static string Employee_SelectByPK = "SELECT Id, DepartmentId, Name FROM Employee WHERE Id = @Id";
    public static string Employee_DeleteByPK = "DELETE FROM Employee WHERE Id = @Id";
    public static string Employee_UpdateByPK = "UPDATE Employee SET DepartmentId = @DepartmentId, Name = @Name WHERE Id = @Id";
}

