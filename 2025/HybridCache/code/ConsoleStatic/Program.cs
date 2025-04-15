// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiCacheDemo;

Console.WriteLine("Hello, World!");
var builder = await RunProgram.DI();
using IHost host = builder.Build();
await host.StartAsync();
Console.WriteLine("Host started");
var test = await testStatic(host);
async Task<int> testStatic(IHost host)
{

    var cacheStatic = host.Services.GetRequiredService<CacheStatic>();
    //await cacheStatic.UpdateDepartmentName(new DepartmentTable()
    //{
    //    Id = 1,
    //    Name = "IT"
    //});
    var dataDep = await cacheStatic.Departments();
    Console.WriteLine($"Departments number {dataDep.Data.Length} cached at {dataDep.CreatedString} : seconds ago {dataDep.SecondsElapsed} ");
    foreach (var dep in dataDep.Data)
    {
        Console.WriteLine(dep.ToString("G"));
    }
    Console.WriteLine("-------------------------------------------------");
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
