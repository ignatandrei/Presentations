var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WhatsAspNet>("whatsaspnet");

builder.Build().Run();
