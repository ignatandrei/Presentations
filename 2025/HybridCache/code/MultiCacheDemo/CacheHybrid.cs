using DBData;
using DBData.genDBModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiCacheDemo;
public class CacheHybrid
{
    private readonly SimpleRepo simpleRepo;
    private readonly HybridCache hybrid;

    public CacheHybrid(SimpleRepo simpleRepo, HybridCache hybrid)
    {
        this.simpleRepo = simpleRepo;
        this.hybrid = hybrid;
    }
    public async Task<DepartmentsCache> Departments()
    {
        HybridCacheEntryOptions opt = new()
        {
            Expiration = TimeSpan.FromMinutes(5),
             Flags= HybridCacheEntryFlags.None,
            LocalCacheExpiration = TimeSpan.FromMinutes(2)
        };
        return await this.hybrid.GetOrCreateAsync(
            key: "departments",
            factory : async (ct) =>
            {
                DepartmentsCache departments = await this.simpleRepo.Departments();
                return departments;
            },
            options: opt,
            cancellationToken: default,
            tags: ["departments"]
            );

    }

    public async Task<int> UpdateDepartmentName(DepartmentTable departmentTable)
    {
        var ret = await this.simpleRepo.UpdateDepartmentName(departmentTable);
        await hybrid.RemoveByTagAsync("departments");
        return ret;
    }
    public async Task<EmployeesCache> EmployeeAsDisplay()
    {

        HybridCacheEntryOptions opt = new()
        {
            Expiration = TimeSpan.FromMinutes(5),
            Flags = HybridCacheEntryFlags.None,
            LocalCacheExpiration = TimeSpan.FromMinutes(3)
        };
        return await this.hybrid.GetOrCreateAsync(
            key: "departments",
            factory: async (ct) =>
            {
                EmployeesCache data = await this.simpleRepo.EmployeeAsDisplay();
                return data;
            },
            options: opt,
            cancellationToken: default,
            tags: ["departments"]
            );

    }

}
