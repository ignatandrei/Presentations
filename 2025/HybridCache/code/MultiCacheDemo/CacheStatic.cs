using DBData;
using DBData.genDBModels;

namespace MultiCacheDemo;

public class CacheStatic
{
    private readonly SimpleRepo simpleRepo;
    static EmployeeDisplay[]? employeeDisplayCache = null;
    static DepartmentDisplay[]? departmentCache = null;
    public CacheStatic(SimpleRepo simpleRepo)
    {
        this.simpleRepo = simpleRepo;
    }
    public async Task<DepartmentDisplay[]> Departments()
    {
        if(departmentCache != null)
        {
            return departmentCache;
        }
        departmentCache = await this.simpleRepo.Departments();
        return departmentCache;
    }
    public async Task<int> UpdateDepartmentName(DepartmentTable departmentTable)
    {
        var ret = await this.simpleRepo.UpdateDepartmentName(departmentTable);
        employeeDisplayCache = null; // Invalidate the cache
        departmentCache = null; // Invalidate the cache
        return ret;
    }
    public async Task<EmployeeDisplay[]> EmployeeAsDisplay()
    {
        if(employeeDisplayCache != null)
        {
            return employeeDisplayCache;
        }
        employeeDisplayCache = await this.simpleRepo.EmployeeAsDisplay();
        return employeeDisplayCache;
    }
}
