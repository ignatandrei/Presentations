using DBData;
using DBData.genContext;
using DBData.genDBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiCacheDemo;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
await Task.Delay(15000);
builder.Configuration.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
var conStringData = builder.Configuration.GetConnectionString("EmpDep");
builder.Services.AddMemoryCache();
builder.Services.AddTransient<SimpleRepo>();
builder.Services.AddTransient<CacheIMemory>();
builder.Services.AddTransient<CacheStatic>();
builder.Services.AddDbContext<EmpDepContext>(options =>
    options.UseSqlServer(conStringData));


using IHost host = builder.Build();

await host.StartAsync();
//await testIMemory(host);
await testStatic(host);
async Task<int> testStatic(IHost host)
{

    var cacheStatic = host.Services.GetRequiredService<CacheStatic>();
    await cacheStatic.UpdateDepartmentName(new DepartmentTable()
    {
        Id = 1,
        Name = "IT"
    });

    var dataEmp = await cacheStatic.EmployeeAsDisplay();
    Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
    foreach (var emp in dataEmp.Data)
    {
        Console.WriteLine(emp.ToString("G"));
    }
    await Task.Delay(10_000);

    dataEmp = await cacheStatic.EmployeeAsDisplay();
    Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");

    await cacheStatic.UpdateDepartmentName(new DepartmentTable()
    {
        Id = 1,
        Name = "test" + Guid.NewGuid().ToString()
    });
    dataEmp = await cacheStatic.EmployeeAsDisplay();
    Console.WriteLine($"Employees number {dataEmp.Data.Length} cached at {dataEmp.CreatedString} : seconds ago {dataEmp.SecondsElapsed} ");
    foreach (var emp in dataEmp.Data)
    {
        Console.WriteLine(emp.ToString("G"));
    }
    return 1;
}

async Task<int> testIMemory(IHost host)
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