using ClientAppsIntegration.WinForms;
using Microsoft.Extensions.Hosting;

namespace ZipAsAService.WinForm;

internal static class Program
{
    public static IServiceProvider Services { get; private set; } = default!;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {

        var builder = Host.CreateApplicationBuilder();
        Microsoft.Extensions.Hosting.Extensions.AddServiceDefaults(builder);
        //builder.AddAppDefaults();

        var scheme = builder.Environment.IsDevelopment() ? "http" : "https";
        scheme="http";
        builder.Services.AddHttpClient<WeatherApiClient>(client => client.BaseAddress = new($"{scheme}://apiservice"));

        var app = builder.Build();
        Services = app.Services;
        app.Start();



        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        //Application.Run(new Form1());
        Application.Run(ActivatorUtilities.CreateInstance<Form1>(app.Services));
    }
}