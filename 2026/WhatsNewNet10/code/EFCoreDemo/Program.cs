// See https://aka.ms/new-console-template for more information
using EFCoreDemo.Data;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__database");
Console.WriteLine($"cn : {connectionString}");

var opt = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ApplicationDbContext>(), connectionString).Options;
using ApplicationDbContext applicationDbContext = new(opt);
var emps = await applicationDbContext.Employees.ToArrayAsync();
Console.WriteLine("RECORDS with default filter:" + emps.Length);

foreach (var d in emps)
{
    Console.WriteLine($"Employee active {d.Id} : {d.Name}");
}
Console.WriteLine("--------------------");
//https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew#named-query-filters

emps = await applicationDbContext
    .Employees
    .IgnoreQueryFilters(["NoLongerActive"])
    .ToArrayAsync();

Console.WriteLine("RECORDS withOUT filter:" + emps.Length);

foreach (var d in emps)
{
    Console.WriteLine($"Employee {d.Id} : {d.Name} {d.TerminationDate}");
}

//https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew#support-for-the-net-10-leftjoin-and-rightjoin-operators

var empsWithDepExisting =await applicationDbContext.Employees
        .Select(it=>new {Employee = it, it.Department })
        .ToArrayAsync();
Console.WriteLine("--------------------");
Console.WriteLine("RECORDS with existing join : "+ empsWithDepExisting.Length);
foreach (var item in empsWithDepExisting)
{
    Console.WriteLine($"Employee {item.Employee.Id} : {item.Employee.Name} Dept: {item.Department?.Name}");
}


var empsWithDepManualJoin = await applicationDbContext.Employees.Join(
    applicationDbContext.Departments,
    e => e.DepartmentId,
    d => d.Id,
    (e, d) => new { Employee = e, Department = d })
    .ToArrayAsync();

Console.WriteLine("--------------------");
Console.WriteLine("RECORDS with Manual join : " + empsWithDepManualJoin.Length);
foreach (var item in empsWithDepManualJoin)
{
    Console.WriteLine($"Employee {item.Employee.Id} : {item.Employee.Name} Dept: {item.Department?.Name}");
}

var empsWithDepLeftJoin = await applicationDbContext.Employees
    .LeftJoin(
        applicationDbContext.Departments,
        e => e.DepartmentId,
        d => d.Id,
        (e, d) => new { Employee = e, Department = d })    
    .ToArrayAsync();

Console.WriteLine("--------------------");

Console.WriteLine("RECORDS with left join :" + empsWithDepLeftJoin.Length);
foreach (var item in empsWithDepLeftJoin)
{
    Console.WriteLine($"Employee {item.Employee.Id} : {item.Employee.Name} Dept: {item.Department?.Name}");
}

//breaking changes
//https://learn.microsoft.com/en-us/dotnet/core/compatibility/breaking-changes

