
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("rediscache")
    //.WithRedisInsight()
    //.WithRedisCommander()
    .WithDbGate()
    ;
var password = builder.AddParameter("password","P@ssw0rd");
var sqlserver = builder.AddSqlServer("sqlserver",password,1433)
    .WithDbGate()
    // Configure the container to store data in a volume so that it persists across instances.
    //.WithDataVolume()
    // Keep the container running between app host sessions.
    //.WithLifetime(ContainerLifetime.Persistent)
    ;
var filesToExecute = DBData.DBFiles.FilesToCreateDB.ToArray();
var str= string.Join("\r\n GO \r\n", filesToExecute);
var addressBookDb = sqlserver.AddDatabase("EmpDep")
    .WithCreationScript(str); 

var apiService = builder.AddProject<Projects.HybridCacheDemo_ApiService>("apiservice")
    .WithHttpsHealthCheck("/health");

builder.AddProject<Projects.HybridCacheDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.ConsoleMemoryCache>("console")
    .WithReference(cache)
    .WaitFor(cache)
    .WithExplicitStart()
    ;

builder.Build().Run();
