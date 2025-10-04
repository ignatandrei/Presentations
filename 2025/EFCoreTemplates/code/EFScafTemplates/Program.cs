//https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/templates?tabs=dotnet-core-cli
using EFScafTemplates.Data;
using EFScafTemplates.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("dotnet new install Microsoft.EntityFrameworkCore.Templates");
Console.WriteLine("Please run dotnet ef scaffold");

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__mydatabase");
var opt = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ApplicationDbContext>(), connectionString).Options;
ApplicationDbContext applicationDbContext = new ApplicationDbContext(opt);
var deps = await applicationDbContext.Departments.ToListAsync();
foreach (var d in deps)
{
    Console.WriteLine($"Department {d.Id} : {d.Name}");
}

Console.WriteLine(string.Join(Environment.NewLine, ApplicationDbContext.GetTableNames()));

Console.WriteLine(string.Join(Environment.NewLine, Employee.Columns()));