var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var grafana = 
    builder.AddContainer("grafana", "grafana/grafana")
                     .WithVolumeMount("../grafana/config", "/etc/grafana")
                     .WithVolumeMount("../grafana/dashboards", "/var/lib/grafana/dashboards")
                     .WithServiceBinding(containerPort: 3000, hostPort: 3000, name: "grafana-http", scheme: "http");


var apiService = builder.AddProject<Projects.ZipAsAService_ApiService>("apiservice");

builder.AddProject<Projects.ZipAsAService_WebAssembly>("webfrontend")
    //.WithReference(cache)
    .WithReference(apiService)
    .WithEnvironment("GRAFANA_URL", grafana.GetEndpoint("grafana-http"));
;

builder.AddProject("tests", "../ZipAsAService.BLLTests/ZipAsAService.BLLTests.csproj")

;

builder.AddProject("winformsclient", "../ZipAsAService.WinForm/ZipAsAService.WinForm.csproj")
    .WithReference(apiService);

builder.AddNpmApp("angular", "../ZipAsAService.Angular")
    .WithReference(apiService)
    .WithServiceBinding(containerPort: 5500, scheme: "http", env: "PORT")
    //.AsDockerfileInManifest()
    ;


//TODO: add grafana
//TODO: add sql server container or direct

builder.Build().Run(); 
