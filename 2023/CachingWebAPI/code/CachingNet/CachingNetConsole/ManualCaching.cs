namespace CachingNetConsole;
internal class ManualCaching
{
    private static DateTime? Cached;
    public async Task<DateTime> GetTime()
    {
        await Task.Delay(1000);
        //TODO: lock
        if (Cached == null)
        {
            Cached = DateTime.Now;
        }
        return Cached.Value;
    }
}
