using Projects;

var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedisContainer("cache");

var weatherapi = builder.AddProject<Projects.AspireWithNode_AspNetCoreApi>("weatherapi");

var node = builder.AddNpmApp("frontend", "../NodeFrontend", "watch")
    .WithReference(weatherapi)
  //  .WithReference(cache)
    .WithServiceBinding(scheme: "http")
    ;

builder.AddProject<Projects.WebAPICaller>(nameof(Projects.WebAPICaller))
    .WithReference(weatherapi)
    .WithEnvironment(act =>
    {
        act.EnvironmentVariables.Add("NODE_URL", node.GetEndpoint("http").UriString);
    });
builder.Build().Run(); 
