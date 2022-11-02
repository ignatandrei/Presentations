global using OpenTelemetry.Trace; 
global using OpenTelemetry.Resources;
global using System.Runtime.CompilerServices;
global using System.Diagnostics;

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
        activity.SetStatus(ActivityStatusCode.Ok);
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
        activity.SetStatus(ActivityStatusCode.Error);
        activity?.SetTag("CallerMemberName", member);
        activity?.SetTag("line", line);
        activity?.SetTag("filePath", filePath);
        activity?.RecordException(ex);
        return activity;
    }
}