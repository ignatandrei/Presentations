using Microsoft.AspNetCore.Mvc;

namespace WhatsNewASPNetCore8;

public interface ICache
{
    object Get(string key);
}
public class BigCache : ICache
{
    public object Get(string key) => $"Resolving {key} from big cache.";
}

public class SmallCache : ICache
{
    public object Get(string key) => $"Resolving {key} from small cache.";
}
public static class MapCacheExtensions
{
    public static RouteGroupBuilder MapCacheEndpoints(this RouteGroupBuilder endpoints)
    {
        endpoints
            .MapGet("/big", ([FromKeyedServices("big")] ICache bigCache) => bigCache.Get("date"));
        
        endpoints.MapGet("/small", ([FromKeyedServices("small")] ICache smallCache) =>
                                                                       smallCache.Get("date"));
        endpoints.MapGet("/{name}",([FromServices]IKeyedServiceProvider keyedServiceProvider, string name) =>
        {
            var cache = keyedServiceProvider.GetRequiredKeyedService<ICache>(name);
            return cache.Get("date");
        });
        return endpoints;

    }
}