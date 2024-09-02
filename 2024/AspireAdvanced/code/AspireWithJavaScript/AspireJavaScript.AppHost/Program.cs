var builder = DistributedApplication.CreateBuilder(args);

var weatherApi = builder.AddProject<Projects.AspireJavaScript_MinimalApi>("weatherapi")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("angular", "../AspireJavaScript.Angular")
    .WithReference(weatherApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    //https://learn.microsoft.com/en-us/dotnet/aspire/deployment/manifest-format
    //dotnet run --output-path manifest.json --publisher manifest
    //.PublishAsDockerFile()
    ;

builder.Build().Run();
