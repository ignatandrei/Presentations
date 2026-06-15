#:sdk Aspire.AppHost.Sdk@13.4.4
#:package Aspire.Hosting.JavaScript@13.4.4

var builder = DistributedApplication.CreateBuilder(args);


builder.AddJavaScriptApp("myWebApp", "./web");
builder.Build().Run();
