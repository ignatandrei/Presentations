// See https://aka.ms/new-console-template for more information
//https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli

Console.WriteLine("Please run dotnet ef");
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__mydatabase");
var opt= SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ApplicationDbContext>(), connectionString).Options;
using ApplicationDbContext applicationDbContext = new (opt);
var deps= await applicationDbContext.Departments.ToListAsync();
foreach(var d in deps)
{
    Console.WriteLine($"Department {d.Id} : {d.Name}");
}
var d1 = deps.First();
d1.Name += "X";
await applicationDbContext.SaveChangesAsync();
var d2 = new Department();
d2.Name += "Y";
applicationDbContext.Departments.Add(d2);
await applicationDbContext.SaveChangesAsync();
deps = await applicationDbContext.Departments.ToListAsync();
foreach (var d in deps)
{
    Console.WriteLine($"Department {d.Id} : {d.Name}");
}