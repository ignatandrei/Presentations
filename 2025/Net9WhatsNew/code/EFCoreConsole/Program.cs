// See https://aka.ms/new-console-template for more information
using EFCoreDemo;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
using TestsContext cnt = new ();
//make breakpoint to partial void BeforeOnConfiguring to see the seeding
cnt.Database.EnsureCreated();
//Order => OrderBy PK 
var lastDepartment=cnt.Departments.OrderDescending().First();
Console.WriteLine(lastDepartment.Iddepartment);
//see sql profiler
//table pruning
//https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew#table-pruning
var deps = await cnt.Departments.Where(it=>it.Employees.Any() ).ToArrayAsync();
Console.WriteLine(deps.Length);
//dotnet ef dbcontext optimize