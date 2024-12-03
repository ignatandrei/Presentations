using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo;

public partial class TestsContext : DbContext
{
    /// <summary>
    /// UseSeeding is called from the EnsureCreated method, and UseAsyncSeeding is called from the EnsureCreatedAsync method. When using this feature, it is recommended to implement both UseSeeding and UseAsyncSeeding methods using similar logic, even if the code using EF is asynchronous. EF Core tooling currently relies on the synchronous version of the method and will not seed the database correctly if the UseSeeding method is not implemented.
    /// https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding#use-seeding-method
    /// </summary>
    /// <param name="optionsBuilder"></param>
    partial void AfterOnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding((cnt,_) =>
        {
            if(!AddData(cnt)) return;
            cnt.SaveChanges();
        });
        optionsBuilder.UseAsyncSeeding(async (cnt, _, cancellationToken) =>
        {
            if (!AddData(cnt)) return;
            await cnt.SaveChangesAsync(cancellationToken);                       
        });
    }
    private bool AddData(DbContext context)
    {
        if(context.Set<Department>().Any()) return false;
        context.Set<Department>().Add(new Department { Name = "IT" });
        context.Set<Department>().Add(new Department { Name = "HR" });
        context.Set<Department>().Add(new Department { Name = "Finance" });
        return true;
    }
}
