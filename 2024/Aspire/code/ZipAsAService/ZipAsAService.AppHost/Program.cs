var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var grafana = 
    builder.AddContainer("grafana", "grafana/grafana")
                     .WithVolumeMount("../grafana/config", "/etc/grafana")
                     .WithVolumeMount("../grafana/dashboards", "/var/lib/grafana/dashboards")
                     .WithServiceBinding(containerPort: 3000, hostPort: 3000, name: "grafana-http", scheme: "http");


var db = builder.AddSqlServer("ad");
var addressBookDb = 
    builder.AddSqlServerContainer("sqlserver")
    // Mount the init scripts directory into the container.
    .WithVolumeMount("./sqlserverconfig", "/usr/config", VolumeMountType.Bind)
    // Mount the SQL scripts directory into the container so that the init scripts run.
    .WithVolumeMount("../DatabaseContainers.ApiService/data/sqlserver", "/docker-entrypoint-initdb.d", VolumeMountType.Bind)
    // Run the custom entrypoint script on startup.
    .WithArgs("/usr/config/entrypoint.sh")
    // Add the database to the application model so that it can be referenced by other resources.
    .AddDatabase("AddressBook");


var apiService = builder
    .AddProject<Projects.ZipAsAService_ApiService>("apiservice")
    .WithEnvironment("GRAFANA_URL", grafana.GetEndpoint("grafana-http"))
    .WithReference(addressBookDb)
;


builder.AddProject<Projects.ZipAsAService_WebAssembly>("webfrontend")
    //.WithReference(cache)
    .WithReference(apiService)
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





builder.Build().Run(); 
