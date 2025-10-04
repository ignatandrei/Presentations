
using DotnetGlobalToolsExtensionAspire;

var builder = DistributedApplication.CreateBuilder(args);

var sqlserver = builder
        .AddSqlServer("sqlserver")
        .WithLifetime(ContainerLifetime.Persistent)
        ;
builder.AddDotnetGlobalTools("dotnet-ef");
var database = sqlserver
    .AddDatabase("mydatabase")
    .ExecuteSqlServerScripts(Scripts.GetScripts())
    .WithSqlPadViewerForDB(sqlserver)
    ; 

builder.Build().Run();
