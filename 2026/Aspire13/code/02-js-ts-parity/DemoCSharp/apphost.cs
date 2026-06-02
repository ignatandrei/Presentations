#:sdk Aspire.AppHost.Sdk@13.3.0
#:package Aspire.Hosting.JavaScript@13.3.0

var builder = DistributedApplication.CreateBuilder(args);


builder.AddJavaScriptApp("myWebApp", "./web");
builder.Build().Run();
