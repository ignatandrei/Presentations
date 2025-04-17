using DBData;
using DBData.genContext;
using DBData.genDBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiCacheDemo;
public class RunProgram
{
    public static async Task<HostApplicationBuilder> DI()
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();
        await Task.Delay(15000);
        builder.Configuration.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
        var conStringData = builder.Configuration.GetConnectionString("EmpDep");
        builder.Services.AddMemoryCache();
        builder.Services.AddTransient<SimpleRepo>();
        builder.Services.AddTransient<CacheIMemory>();
        builder.Services.AddTransient<CacheStatic>();
        builder.Services.AddDbContext<EmpDepContext>(options =>
            options.UseSqlServer(conStringData));
        builder.Services.AddTransient<CacheIDistributed>();
        builder.Services.AddTransient<CacheHybrid>();

        builder.Services.AddHybridCache(opt =>
        {
            opt.DisableCompression = false;
        });
        builder.Services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = builder.Configuration.GetConnectionString(
                "CachingData");
            options.SchemaName = "dbo";
            options.TableName = "TestCache";
        });


        return builder;

    }
    
}