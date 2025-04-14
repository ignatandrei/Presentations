using DBData.genContext;
using DBData.genDBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBData;
public class SimpleRepo
{
    private readonly EmpDepContext empDepContext;

    public SimpleRepo(EmpDepContext empDepContext)
    {
        this.empDepContext = empDepContext;
    }
    public async Task<int> UpdateDepartmentName(DepartmentTable departmentTable)
    {
        var department = await this.empDepContext.Department.FindAsync(departmentTable.Id);
        if (department == null)
        {
            return 0;
        }
        department.Name = departmentTable.Name;
        await this.empDepContext.SaveChangesAsync();
        return department.Id;
    }
    public async Task<EmployeeDisplay[]> EmployeeAsDisplay()
    {
        Console.WriteLine("****** Obtaining Employees from Database ******");
        var ret= this.empDepContext.Employee.Select(e => new EmployeeDisplay
        {
            Id = e.Id,
            Name = e.FirstName + " " + e.LastName,
            DepartmentName = e.IdDepartmentNavigation.Name
        }).ToArrayAsync();

        return await ret;
    }
    public Task<DepartmentDisplay[]> Departments()
    {
        Console.WriteLine("****** Obtaining Departments from Database ******");
        var ret = this.empDepContext.Department
            .Select(it=>
                new DepartmentDisplay
                {
                    Id = it.Id,
                    Name = it.Name,
                    Employees = it.Employee.Count()
                }
            ).ToArrayAsync();

        return ret;
    }
}
