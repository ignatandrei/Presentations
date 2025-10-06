using Dapper;
using Microsoft.Data.SqlClient;
using EFScafTemplatesDapper.Models;
namespace EFScafTemplatesDapper.Data;

public static class ApplicationDbContext 
{

    //public virtual DbSet<Department> Departments { get; set; }
    public static string Department_SelectAll = "SELECT Id, Name FROM Department";
    public static string Department_SelectByPK = "SELECT Id, Name FROM Department WHERE Id = @Id";
    public static string Department_DeleteByPK = "DELETE FROM Department WHERE Id = @Id";
    public static string Department_UpdateByPK = "UPDATE Department SET Name = @Name WHERE Id = @Id";
    public static string Department_Insert = "Insert into Department(Name) Values(@Name)";
    
    public static async Task<IEnumerable<Department>> SelectAllDepartment(this SqlConnection con){
        return await con.QueryAsync<Department>(Department_SelectAll);
    }
    public static async Task<Department> SelectByPKDepartment(this SqlConnection con, Department item){
        return await con.QuerySingleAsync<Department>(Department_SelectByPK,item);
    }
    public static async Task<int> UpdateByPKDepartment(this SqlConnection con, Department item){
        return await con.ExecuteAsync(Department_UpdateByPK,item);
    }
    public static async Task<int> DeleteByPKDepartment(this SqlConnection con, Department item){
        return await con.ExecuteAsync(Department_DeleteByPK,item);
    }
    public static async Task<int> InsertDepartment(this SqlConnection con, Department item){
        return await con.ExecuteAsync(Department_Insert,item);
    }

    //public virtual DbSet<Employee> Employees { get; set; }
    public static string Employee_SelectAll = "SELECT Id, DepartmentId, Name FROM Employee";
    public static string Employee_SelectByPK = "SELECT Id, DepartmentId, Name FROM Employee WHERE Id = @Id";
    public static string Employee_DeleteByPK = "DELETE FROM Employee WHERE Id = @Id";
    public static string Employee_UpdateByPK = "UPDATE Employee SET DepartmentId = @DepartmentId, Name = @Name WHERE Id = @Id";
    public static string Employee_Insert = "Insert into Employee(DepartmentId, Name) Values(@DepartmentId, @Name)";
    
    public static async Task<IEnumerable<Employee>> SelectAllEmployee(this SqlConnection con){
        return await con.QueryAsync<Employee>(Employee_SelectAll);
    }
    public static async Task<Employee> SelectByPKEmployee(this SqlConnection con, Employee item){
        return await con.QuerySingleAsync<Employee>(Employee_SelectByPK,item);
    }
    public static async Task<int> UpdateByPKEmployee(this SqlConnection con, Employee item){
        return await con.ExecuteAsync(Employee_UpdateByPK,item);
    }
    public static async Task<int> DeleteByPKEmployee(this SqlConnection con, Employee item){
        return await con.ExecuteAsync(Employee_DeleteByPK,item);
    }
    public static async Task<int> InsertEmployee(this SqlConnection con, Employee item){
        return await con.ExecuteAsync(Employee_Insert,item);
    }

}

