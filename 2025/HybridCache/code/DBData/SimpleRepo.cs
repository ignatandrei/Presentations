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
    //public async Task<int> UpdateDepartmentName(string name)
    public async Task<EmployeeDisplay[]> EmployeeAsDisplay()
    {
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
