// See https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory for more information
WriteLine("Hello, World!");

var cached = new clsCachedData(new MemoryCache(new MemoryCacheOptions()));

while (true)
{
    await Delay(3000);
    WriteLine("cached "+ (await cached.GetTimeCached()).ToString("mm_ss"));
    WriteLine("not cached"+(await cached.GetTime()).ToString("mm_ss"));
}