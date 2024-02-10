var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TODO: put in settings file
//builder.HostEnvironment.BaseAddress
var hostAPI = "http://localhost:5511";


builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(hostAPI), Timeout= TimeSpan.FromSeconds(20) 

});

builder.Services.AddScoped<WeatherApiClient>();
// Add service defaults & Aspire components.
//builder.AddServiceDefaults();
//builder.AddRedisOutputCache("cache");
builder.Services.AddBlazorDownloadFile();
//builder.Services.AddHttpClient<WeatherApiClient>(client => client.BaseAddress = new("http://apiservice"));


await builder.Build().RunAsync();
