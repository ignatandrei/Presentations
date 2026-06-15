#:sdk Aspire.AppHost.Sdk@13.3.0
#:package Aspire.Hosting.Redis@13.3.0

var builder = DistributedApplication.CreateBuilder(args);
builder.AddRedis("cache");
#pragma warning disable ASPIRECSHARPAPPS001
builder.AddCSharpApp("worker", "./netcoreTel.cs");
#pragma warning restore ASPIRECSHARPAPPS001

builder.Build().Run();
