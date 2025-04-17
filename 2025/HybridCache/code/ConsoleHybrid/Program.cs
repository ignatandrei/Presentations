// See https://aka.ms/new-console-template for more information
using DBData.genDBModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiCacheDemo;
Console.WriteLine("Hello, World!");
var builder = await RunProgram.DI();
using IHost host = builder.Build();
await host.StartAsync();
Console.WriteLine("Host started");
await testHybrid(host);

static async Task<int> testHybrid(IHost host)
{
    var cach = host.Services.GetRequiredService<CacheHybrid>();
    await cach.UpdateDepartmentName(new DepartmentTable()
    {
        Id = 1,
        Name = "IT"
    });
    var deps = await cach.Departments();
    Console.WriteLine($" Dep cached at {deps.CreatedString}");
    foreach (var dep in deps.Data)
    {
        Console.WriteLine(dep.ToString("G"));
    }
    Console.WriteLine("-----------------");
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
