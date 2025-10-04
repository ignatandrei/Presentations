using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sqlserver = builder
        .AddSqlServer("sqlserver")
        .WithDbGate()

        .WithLifetime(ContainerLifetime.Persistent)
        ;

builder.AddDotnetGlobalTools("dotnet-ef");
var database = sqlserver
    .AddDatabase("mydatabase")
    .ExecuteSqlServerScripts(Scripts.GetScripts())
    .WithSqlPadViewerForDB(sqlserver)
    ;
builder.AddProject<EFScaf0>("EFScaffoldingFirst")
    .WithExplicitStart()
    .WaitFor(database)
    .WithReference(database)
    .ExecuteScaffoldEF(database);

builder.AddProject<EFScafTemplates>("EFScaffoldingTemplates")
    .WithExplicitStart()
    .WaitFor(database)
    .WithReference(database)
    .ExecuteScaffoldEF(database);

builder.Build().Run();
