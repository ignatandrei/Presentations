var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WhatsAspNet>("whatsaspnet");
builder.AddProject<Projects.EFCoreConsole>("efcoreconsole");
builder.AddProject<Projects.WhtaCSharp13>("csharp13");


builder.Build().Run();
