using DBData;

namespace MultiCacheDemo;

public class CacheStatic
{
    private readonly SimpleRepo simpleRepo;
    static EmployeeDisplay[]? employeeDisplayCache = null;
    public CacheStatic(SimpleRepo simpleRepo)
    {
        this.simpleRepo = simpleRepo;
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
