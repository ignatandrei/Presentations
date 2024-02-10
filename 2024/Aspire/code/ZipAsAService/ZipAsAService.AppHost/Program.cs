var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ZipAsAService_ApiService>("apiservice");

builder.AddProject<Projects.ZipAsAService_WebAssembly>("webfrontend")
    //.WithReference(cache)
    .WithReference(apiService)
    ;

builder.AddProject("tests", "../ZipAsAService.BLLTests/ZipAsAService.BLLTests.csproj")

;

builder.AddProject("winformsclient", "../ZipAsAService.WinForm/ZipAsAService.WinForm.csproj")
    .WithReference(apiService);

builder.Build().Run(); 
