var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.whatsNewNet10_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.whatsNewNet10_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
