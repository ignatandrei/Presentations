using DBData;
using DBData.genDBModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCacheDemo;
public class CacheIMemory
{
    private readonly SimpleRepo simpleRepo;
    private readonly IMemoryCache memoryCache;

    public CacheIMemory(SimpleRepo simpleRepo, IMemoryCache memoryCache)
    {
        this.simpleRepo = simpleRepo;
        this.memoryCache = memoryCache;
    }
    public async Task<DepartmentsCache> Departments()
    {
        MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        var ret= await memoryCache.GetOrCreateAsync("departments", async (mc) => 
        {
            var departments = await this.simpleRepo.Departments();
            return new DepartmentsCache( departments);
        }, memoryCacheEntryOptions);
        return ret!;
    }

    public async Task<int> UpdateDepartmentName(DepartmentTable departmentTable)
    {
        var ret = await this.simpleRepo.UpdateDepartmentName(departmentTable);
        memoryCache.Remove("departments");
        memoryCache.Remove("employees");
        return ret;
    }
    public async Task<EmployeesCache> EmployeeAsDisplay()
    {
        MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        var ret = await memoryCache.GetOrCreateAsync("employees", async (mc) =>
        {
            var data = await this.simpleRepo.EmployeeAsDisplay();
            return new EmployeesCache(data);
        }, memoryCacheEntryOptions);
        return ret!;
    }

}
