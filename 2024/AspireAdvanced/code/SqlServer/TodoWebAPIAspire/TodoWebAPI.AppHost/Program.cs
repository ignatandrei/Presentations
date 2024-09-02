var builder = DistributedApplication.CreateBuilder(args);

//ParameterResourceBuilderExtensions.

var addressBookDb = builder.AddSqlServer("sqlserver", port:1433)
    
    // Mount the init scripts directory into the container.
    .WithBindMount("./sqlserverconfig", "/usr/config")
    // Mount the SQL scripts directory into the container so that the init scripts run.
    .WithBindMount("data", "/docker-entrypoint-initdb.d")
    // Run the custom entrypoint script on startup.
    .WithEntrypoint("/usr/config/entrypoint.sh")
    // Add the database to the application model so that it can be referenced by other resources.
    .AddDatabase("AddressBook")
    ;

builder.AddProject<Projects.Todo>("todo")
    .WithReference(addressBookDb);

builder.Build().Run();
