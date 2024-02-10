var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ZipAsAService_ApiService>("apiservice");

builder.AddProject<Projects.ZipAsAService_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService)
    ;

builder.AddProject("tests", "../ZipAsAService.BLLTests/ZipAsAService.BLLTests.csproj")

;
builder.Build().Run(); 
