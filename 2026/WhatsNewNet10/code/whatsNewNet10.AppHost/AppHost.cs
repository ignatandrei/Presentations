//https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#support-for-the-localhost-top-level-domain
//see url of apphost
//also see url of the api service from the web project

var builder = DistributedApplication.CreateBuilder(args);

HttpCommandOptions defGet= new()
{
    Method = HttpMethod.Get
};
var apiService = builder
    .AddProject<Projects.whatsNewNet10_ApiService>("apiservice")
    .WithHttpCommand("/openapi/v1.yaml","yaml",commandOptions: defGet)
    .WithHttpCommand("/openapi/v1.json","json",commandOptions: defGet)
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
