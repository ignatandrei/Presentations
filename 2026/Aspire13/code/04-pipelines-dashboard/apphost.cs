#:sdk Aspire.AppHost.Sdk@13.3.3

var builder = DistributedApplication.CreateBuilder(args);

// Type is for evaluation and may change in future versions.
#pragma warning disable ASPIREPIPELINES001
builder.Pipeline.AddStep("demo-check", async context =>
{
    await Task.Delay(5_000);
    System.Console.WriteLine("This is a demo check step. It does nothing and always succeeds.");
    return ;
});
#pragma warning restore ASPIREPIPELINES001
#pragma warning disable ASPIRECSHARPAPPS001
builder.AddCSharpApp("worker", "./netcoreTel.cs");
#pragma warning restore ASPIRECSHARPAPPS001


builder.Build().Run();
