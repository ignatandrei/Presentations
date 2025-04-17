
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("rediscache")
//    //.WithRedisInsight()
//    //.WithRedisCommander()
//    .WithDbGate()
//    ;
//TODO: https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/custom-resource-commands

var password = builder.AddParameter("password","P@ssw0rd");
var sqlserver = builder.AddSqlServer("sqldata",password,1433)
    .WithDbGate()
    // Configure the container to store data in a volume so that it persists across instances.
    .WithDataVolume()
    // Keep the container running between app host sessions.
    .WithLifetime(ContainerLifetime.Persistent)
    ;
var filesEmpDep = DBData.DBFiles.FilesToCreateEmpDep.ToArray();
var str= string.Join("\r\n GO \r\n", filesEmpDep);
var databaseEmpDep = sqlserver.AddDatabase("EmpDep")
    .WithCreationScript(str);

var filesCache = DBData.DBFiles.FilesToCreateCache.ToArray();
str = string.Join("\r\n GO \r\n", filesCache);
var databaseCache = sqlserver.AddDatabase("CachingData")
    .WithCreationScript(str);

//File.WriteAllText("script.txt", str);
var apiService = builder.AddProject<Projects.HybridCacheDemo_ApiService>("apiservice")
    .WithHttpsHealthCheck("/health")
    .WithHttpCommand(
        "/static/employees",
        "Get Employees",
        commandOptions: new HttpCommandOptions
        {
            Description = "Get Employees",
            Method = HttpMethod.Get,
            IconName = "DocumentLightning",
            IsHighlighted = true
        })
    .WithReference(databaseEmpDep)
    .WaitFor(databaseEmpDep)
    .WithReference(databaseCache)
    .WaitFor(databaseCache)

    ;

//builder.AddProject<Projects.HybridCacheDemo_Web>("webfrontend")
//    .WithExternalHttpEndpoints()
//    .WithHttpsHealthCheck("/health")
//    .WithReference(apiService)
//    .WaitFor(apiService);

builder.AddProject<Projects.ConsoleStatic>("ConsoleStatic")
    .WithReference(databaseEmpDep)
    .WaitFor(databaseEmpDep)
    .WithReference(databaseCache)
    .WaitFor(databaseCache)
    .WithExplicitStart()
    ;
builder.AddProject<Projects.ConsoleIMemory>("ConsoleIMemory")
    .WithReference(databaseEmpDep)
    .WaitFor(databaseEmpDep)
    .WithReference(databaseCache)
    .WaitFor(databaseCache)
    .WithExplicitStart()
    ;
builder.AddProject<Projects.ConsoleIDistributed>("ConsoleIDistributed")
    .WithReference(databaseEmpDep)
    .WaitFor(databaseEmpDep)
    .WithReference(databaseCache)
    .WaitFor(databaseCache)
    .WithExplicitStart()
    ;
builder.AddProject<Projects.ConsoleHybrid>("ConsoleHybrid")
    .WithReference(databaseEmpDep)
    .WaitFor(databaseEmpDep)
    .WithReference(databaseCache)
    .WaitFor(databaseCache)
    .WithExplicitStart()
    ;

builder.Build().Run();
