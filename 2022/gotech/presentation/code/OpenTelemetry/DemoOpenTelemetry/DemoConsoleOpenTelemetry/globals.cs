global using Microsoft.Extensions.Configuration;
global  using static System.Console;
global using Microsoft.Extensions.DependencyInjection;
global using OpenTelemetry.Resources;
global using OpenTelemetry;
global using System.Diagnostics;
global using OpenTelemetry.Trace;
global using DemoConsoleOpenTelemetry;
global using System.Runtime.CompilerServices;

public class ActivityData
{
    static string serviceName = ThisAssembly.Project.AssemblyName;
    static ActivitySource MyActivitySource = new ActivitySource(serviceName);

    public static Activity? AddActivity(
        [CallerMemberName] string member = "",
         [CallerLineNumber] int line = 0,
         [CallerFilePath] string filePath = ""
        )
    {
        
        var activity = MyActivitySource.StartActivity(member);
        activity.SetStatus (ActivityStatusCode.Ok);
        activity?.SetTag("CallerMemberName", member);
        activity?.SetTag("line", line);
        activity?.SetTag("filePath", filePath);
        return activity;
    }
    public static Activity? AddActivityException(
        Exception ex,
        [CallerMemberName] string member = "",
         [CallerLineNumber] int line = 0,
         [CallerFilePath] string filePath = ""
        )
    {
        var activity = MyActivitySource.StartActivity(member);
        activity.SetStatus (ActivityStatusCode.Error);
        activity?.SetTag("CallerMemberName", member);
        activity?.SetTag("line", line);
        activity?.SetTag("filePath", filePath);
        activity?.RecordException(ex);
        return activity;
    }
}