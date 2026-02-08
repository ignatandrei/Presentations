var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.PlayrightDemo_ApiService>("apiservice");


builder.AddProject<Projects.Demo1_Console>("demo1-console")
//    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.AddProject<Projects.Demo2_Show>("demo2-show")
    .WithReference(apiService); 

builder.Build().Run();
