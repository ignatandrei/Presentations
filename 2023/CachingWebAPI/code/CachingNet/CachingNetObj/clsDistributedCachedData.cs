namespace CachingNetObj;

public class clsDistributedCachedData
{
    private readonly IDistributedCache cache;

    public clsDistributedCachedData(IDistributedCache cache)
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
        var data = await cache.GetStringAsync(nameof(GetTimeCached));
        if(string.IsNullOrWhiteSpace(data))
        {
            await cache.SetStringAsync(nameof(GetTimeCached), (await GetTime()).ToString("s"),
                
                new DistributedCacheEntryOptions()
                {
                     AbsoluteExpirationRelativeToNow= TimeSpan.FromSeconds(20)
                });
        }
        data = await cache.GetStringAsync(nameof(GetTimeCached));
        ArgumentNullException.ThrowIfNullOrEmpty(data);
        return DateTime.ParseExact(data, "s",null);
    }
    //https://github.com/mgravell/DistributedCacheDemo/blob/main/DistributedCacheExtensions.cs
    //https://devblogs.microsoft.com/dotnet/caching-abstraction-improvements-in-aspnetcore/
    public async Task<DateTime> GetTimeCachedSimpler()
    {
        var timeObject = await cache.GetAsync<DateTime>(nameof(GetTimeCachedSimpler),
       async (ct) =>
       {
           var data = await GetTime();
           return data; // this could be a complex object with multiple values
       }, CacheOptions.Seconds(20));

        return timeObject;

        //could be complicated
       // string foo = "ad"; string bar = "asd";
       // var timeObject = await cache.GetAsync(nameof(GetTimeCachedSimpler),
       //(Foo: foo, Bar: bar), // state values
       //async (state, ct) =>
       //{
       //    var data = await GetTime();
       //    return new { Time = data, state.Foo, state.Bar }; // this could be a complex object with multiple values
       //}, CacheOptions.Seconds(20));


    }


}