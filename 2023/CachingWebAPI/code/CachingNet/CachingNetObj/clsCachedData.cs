namespace CachingNetObj;

public class clsCachedData
{
    private readonly IMemoryCache cache;

    public clsCachedData(IMemoryCache cache)
    {
        this.cache = cache;
    }
    public async Task<DateTime> GetTime()
    {
        await Task.Delay(1000);
        return DateTime.Now;
    }
    public async Task<DateTime> GetTimeCached()
    {
        
        await Task.Delay(1000);
        if(!cache.TryGetValue(nameof(GetTimeCached),out var date))
        {            
            cache.Set(nameof(GetTimeCached),await GetTime(),TimeSpan.FromSeconds( 20));
        }
        return cache.Get<DateTime>(nameof(GetTimeCached));
    }

}