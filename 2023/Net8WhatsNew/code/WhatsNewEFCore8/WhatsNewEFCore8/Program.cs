//https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew
//https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes
//docker pull mcr.microsoft.com/mssql/server:2022-latest
//docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
//dotnet tool install --global dotnet-ef --no-cache
//dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Test;UID=sa;PWD=yourStrong(!)Password;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

WriteLine("https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes");

long[] ids = [2, 34, 2, 89];
using var cnt=new TestContext();
//search for
//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Test;UID=sa;PWD=yourStrong(!)Password;TrustServerCertificate=true"
//        , o => o.UseCompatibilityLevel(120)
//        );
//}
//
var emps= cnt.Emps.Where(x=>ids.Contains(x.Id)).ToArray();

foreach (var item in emps)
{
    WriteLine($"{item.Id} {item.Name}");
}

var data = cnt
    .Database //no sql injection below
    .SqlQuery<Book>($"select * from Book ")
    .Where(it => it.ID < 10)
    .ToArray();

//WriteLine(data.Length);
foreach (var book in data)
{
    WriteLine($"{book.ID} {book.Name}");
}   