#:sdk Aspire.AppHost.Sdk@13.3.0
#:package Aspire.Hosting.Redis@13.3.0

var builder = DistributedApplication.CreateBuilder(args);
builder.AddRedis("cache");
builder.Build().Run();
