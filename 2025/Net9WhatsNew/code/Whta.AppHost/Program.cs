var builder = DistributedApplication.CreateBuilder(args);

var asp = builder.AddProject<Projects.WhatsAspNet>("whatsaspnet");
//https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/app-host-overview?tabs=docker#waiting-for-resources
var ef= builder.AddProject<Projects.EFCoreConsole>("efcoreconsole")
    .WaitFor(asp);
//https://learn.microsoft.com/en-us/dotnet/aspire/whats-new/dotnet-aspire-9?tabs=windows#persistent-containers
var queue = builder.AddRabbitMQ("rabbit")
                   .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.WhtaCSharp13>("csharp13");

builder.Build().Run();

//not here: https://learn.microsoft.com/en-us/dotnet/aspire/app-host/eventing

