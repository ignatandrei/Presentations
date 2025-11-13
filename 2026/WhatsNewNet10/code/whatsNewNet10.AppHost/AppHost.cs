var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.whatsNewNet10_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.whatsNewNet10_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithUrl("/weather")
    .WithReference(apiService)
    .WaitFor(apiService);

builder
    .AddProject<Projects.WhatsNewDotNet10>("RunMeToSeeWhatsNewInNET10")
    .WithExplicitStart();


var app = builder.Build();
var serv = app.Services;
app.Run();
