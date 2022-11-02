global using OpenTelemetry.Trace; 
global using OpenTelemetry.Resources;
global using System.Runtime.CompilerServices;
global using System.Diagnostics;

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
        var activity = MyActivitySource.StartActivity("SayHello");

        activity?.SetTag("CallerMemberName", member);
        activity?.SetTag("line", line);
        activity?.SetTag("filePath", filePath);
        return activity;
    }
}