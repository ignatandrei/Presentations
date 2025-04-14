using DBData;
using DBData.genContext;
using DBData.genDBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiCacheDemo;
public class RunProgram
{
    public static async Task<HostApplicationBuilder> DI()
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();
        await Task.Delay(15000);
        builder.Configuration.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
        var conStringData = builder.Configuration.GetConnectionString("EmpDep");
        builder.Services.AddMemoryCache();
        builder.Services.AddTransient<SimpleRepo>();
        builder.Services.AddTransient<CacheIMemory>();
        builder.Services.AddTransient<CacheStatic>();
        builder.Services.AddDbContext<EmpDepContext>(options =>
            options.UseSqlServer(conStringData));
        builder.Services.AddTransient<CacheIDistributed>();
        builder.Services.AddTransient<CacheHybrid>();

        builder.Services.AddHybridCache(opt =>
        {
            opt.DisableCompression = false;
        });
        builder.Services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = builder.Configuration.GetConnectionString(
                "CachingData");
            options.SchemaName = "dbo";
            options.TableName = "TestCache";
        });


        return builder;

    }
    public static async Task<int> testHybrid(IHost host)
    {
        var cach = host.Services.GetRequiredService<CacheHybrid>();
        await cach.UpdateDepartmentName(new DepartmentTable()
        {
            Id = 1,
            Name = "IT"
        });

        var dataEmp = await cach.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        foreach (var emp in dataEmp.Data)
        {
            Console.WriteLine(emp.ToString("G"));
        }
        await Task.Delay(10_000);

        dataEmp = await cach.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");

        await cach.UpdateDepartmentName(new DepartmentTable()
        {
            Id = 1,
            Name = "test" + Guid.NewGuid().ToString()
        });
        dataEmp = await cach.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        foreach (var emp in dataEmp.Data)
        {
            Console.WriteLine(emp.ToString("G"));
        }
        return 1;

    }
    public static async Task<int> testDistributed(IHost host)
    {
        var cacheDist = host.Services.GetRequiredService<CacheIDistributed>();
        await cacheDist.UpdateDepartmentName(new DepartmentTable()
        {
            Id = 1,
            Name = "IT"
        });

        var dataEmp = await cacheDist.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        foreach (var emp in dataEmp.Data)
        {
            Console.WriteLine(emp.ToString("G"));
        }
        await Task.Delay(10_000);

        dataEmp = await cacheDist.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");

        await cacheDist.UpdateDepartmentName(new DepartmentTable()
        {
            Id = 1,
            Name = "test" + Guid.NewGuid().ToString()
        });
        dataEmp = await cacheDist.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        foreach (var emp in dataEmp.Data)
        {
            Console.WriteLine(emp.ToString("G"));
        }
        return 1;

    }
    public static async Task<int> testStatic(IHost host)
    {

        var cacheStatic = host.Services.GetRequiredService<CacheStatic>();
        //await cacheStatic.UpdateDepartmentName(new DepartmentTable()
        //{
        //    Id = 1,
        //    Name = "IT"
        //});

        var dataEmp = await cacheStatic.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        foreach (var emp in dataEmp.Data)
        {
            Console.WriteLine(emp.ToString("G"));
        }
        await Task.Delay(10_000);

        dataEmp = await cacheStatic.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");

        //await cacheStatic.UpdateDepartmentName(new DepartmentTable()
        //{
        //    Id = 1,
        //    Name = "test" + Guid.NewGuid().ToString()
        //});
        //dataEmp = await cacheStatic.EmployeeAsDisplay();
        //Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        //foreach (var emp in dataEmp.Data)
        //{
        //    Console.WriteLine(emp.ToString("G"));
        //}
        return 1;
    }

    public static async Task<int> testIMemory(IHost host)
    {

        var cacheIMemory = host.Services.GetRequiredService<CacheIMemory>();
        await cacheIMemory.UpdateDepartmentName(new DepartmentTable()
        {
            Id = 1,
            Name = "IT"
        });

        var dataEmp = await cacheIMemory.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        foreach (var emp in dataEmp.Data)
        {
            Console.WriteLine(emp.ToString("G"));
        }
        await Task.Delay(10_000);

        dataEmp = await cacheIMemory.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");

        await cacheIMemory.UpdateDepartmentName(new DepartmentTable()
        {
            Id = 1,
            Name = "test" + Guid.NewGuid().ToString()
        });
        dataEmp = await cacheIMemory.EmployeeAsDisplay();
        Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
        foreach (var emp in dataEmp.Data)
        {
            Console.WriteLine(emp.ToString("G"));
        }
        return 1;
    }

}