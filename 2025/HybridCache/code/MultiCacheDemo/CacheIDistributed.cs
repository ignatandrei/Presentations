using DBData;
using DBData.genDBModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiCacheDemo;
public class CacheIDistributed
{
    private readonly SimpleRepo simpleRepo;
    private readonly IDistributedCache distributedCache;

    public CacheIDistributed(SimpleRepo simpleRepo, IDistributedCache distributedCache)
    {
        this.simpleRepo = simpleRepo;
        this.distributedCache = distributedCache;
    }
    public async Task<DepartmentsCache> Departments()
    {
        DistributedCacheEntryOptions opt = new ()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        var ret = await distributedCache.GetStringAsync("departments");
        if(string.IsNullOrWhiteSpace(ret))
        {
            DepartmentsCache departments = await this.simpleRepo.Departments();
            var json = System.Text.Json.JsonSerializer.Serialize(departments);
            await distributedCache.SetStringAsync("departments", json, opt);
            return departments;
        }
        return JsonSerializer.Deserialize<DepartmentsCache>(ret)!;
    }

    public async Task<int> UpdateDepartmentName(DepartmentTable departmentTable)
    {
        var ret = await this.simpleRepo.UpdateDepartmentName(departmentTable);
        await distributedCache.RemoveAsync("departments");
        //distributedCache.RemoveAsync("employees");
        return ret;
    }
    public async Task<EmployeesCache> EmployeeAsDisplay()
    {
        DistributedCacheEntryOptions opt = new ()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        var ret = await distributedCache.GetStringAsync("employees");
        if(string.IsNullOrEmpty(ret))
        {
            EmployeesCache data = await this.simpleRepo.EmployeeAsDisplay();
            var json = System.Text.Json.JsonSerializer.Serialize(data);
            await distributedCache.SetStringAsync("employees", json, opt);
            return data;
        }
        
        return JsonSerializer.Deserialize<EmployeesCache>(ret)!;
    }

}
