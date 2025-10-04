//https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/templates?tabs=dotnet-core-cli
using EFScafTemplatesDapper.Data;
using EFScafTemplatesDapper.Models;
using Microsoft.Data.SqlClient;
using Dapper;

Console.WriteLine("dotnet new install Microsoft.EntityFrameworkCore.Templates");

Console.WriteLine("Please run dotnet new ef-templates / dotnet ef scaffold");

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__mydatabase");
SqlConnection sqlConnection = new SqlConnection(connectionString);
await sqlConnection.OpenAsync();


var deps = await sqlConnection.QueryAsync<Department>(ApplicationDbContext.Department_SelectAll);
foreach (var d in deps)
{
    Console.WriteLine($"Department {d.Id} : {d.Name} ");
}
var d1=deps.First();
d1.Name+= "X";
await sqlConnection.ExecuteAsync(ApplicationDbContext.Department_UpdateByPK, d1);

deps = await sqlConnection.SelectAllDepartment();
foreach (var d in deps)
{
    Console.WriteLine($"Department {d.Id} : {d.Name} ");
}
