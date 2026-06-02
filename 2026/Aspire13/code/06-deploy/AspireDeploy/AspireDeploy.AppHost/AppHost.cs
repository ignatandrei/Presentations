using Aspire.Hosting.Docker;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    .PublishAsDockerComposeService((resource, service) =>
{
    // Customize the generated Docker Compose service
    service.ContainerName ="ANDREI";
    service.Labels.Add("com.example.team", "backend");
    service.Restart = "unless-stopped";
});

IResourceBuilder<IResource> builderForDeploy;
//https://aspire.dev/deployment/deploy-with-aspire/#built-in-target-capabilities
if (builder.Environment.IsEnvironment("Andrei"))
{
    builderForDeploy = builder.AddDockerComposeEnvironment("dce");
}
else
{
    builderForDeploy = builder.AddKubernetesEnvironment("k8e");
    builderForDeploy = builder.AddAzureContainerAppEnvironment("acae");
}
var server = builder.AddProject<Projects.AspireDeploy_Server>("server")
    .WithReference(cache)
    .WaitFor(cache)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(server)
    .WaitFor(server);

//in .net core: app.UseFileServer();
server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.Build().Run();
