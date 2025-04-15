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
await testIMemory(host);
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

