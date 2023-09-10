// See https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory for more information


WriteLine("Hello, World!" + DateTime.Now.ToString("mm_ss"));
ManualCaching mc = new();
WriteLine((await mc.GetTime()).ToString("mm_ss"));
await Task.Delay(3000);

WriteLine("cached "+(await mc.GetTime()).ToString("mm_ss"));
WriteLine("now " + DateTime.Now.ToString("mm_ss"));

WriteLine("now with expiration, press any key to continue");
ReadLine();
var cached = new clsCachedData(new MemoryCache(new MemoryCacheOptions()));

while (true)
{
    await Delay(3000);
    WriteLine("cached "+ (await cached.GetTimeCached()).ToString("mm_ss"));
    WriteLine("not cached"+(await cached.GetTime()).ToString("mm_ss"));
}