global using Microsoft.Extensions.Configuration;
global  using static System.Console;
global using Microsoft.Extensions.DependencyInjection;
global using OpenTelemetry.Resources;
global using OpenTelemetry;
global using System.Diagnostics;
global using OpenTelemetry.Trace;
global using System.Diagnostics;
global using System.Reflection;
global using OpenTelemetry.Resources;
global using OpenTelemetry.Trace;
global using DemoConsoleOpenTelemetry;
global using System.Runtime.CompilerServices;

public class ActivityData
{
    static string serviceName = ThisAssembly.Project.AssemblyName;
    static ActivitySource MyActivitySource = new ActivitySource(serviceName);

    public static IDisposable? AddActivity(
        [CallerMemberName] string member = "",
         [CallerLineNumber] int line = 0,
         [CallerFilePath] string filePath = ""
        )
    {
        var activity = MyActivitySource.StartActivity(member);

        activity?.SetTag("CallerMemberName", member);
        activity?.SetTag("line", line);
        activity?.SetTag("filePath", filePath);
        return activity;
    }
}