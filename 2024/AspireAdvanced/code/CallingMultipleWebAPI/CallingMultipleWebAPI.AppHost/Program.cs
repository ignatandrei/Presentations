using AddAutomation;

using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.WebAPIBase64>("base64")
    .WithSwaggerUI();
    
var rot13=builder.AddProject<Projects.WebAPIROT13>("webapirot13")
    .WithSwaggerUI();
builder.Build().Run();
