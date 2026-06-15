#:sdk Aspire.AppHost.Sdk@13.4.4
#:package Aspire.Hosting.Python@13.4.4

#pragma warning disable ASPIREHOSTINGPYTHON001

var builder = DistributedApplication.CreateBuilder(args);

// Demo note:
// In a full example, add a Python resource with:
builder.AddPythonApp("worker", "./src", "app.py");

builder.Build().Run();
