
class FakeTp : TimeProvider
{



    public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
    {
        return base.CreateTimer(callback, state, dueTime, period);
    }
    public override DateTimeOffset GetUtcNow()
    {
        return new DateTimeOffset(1970, 4, 16, 15, 01, 01, TimeSpan.Zero);
    }


}