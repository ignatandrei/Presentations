namespace HardWorkingBLL;

public record EmployeeData(int id)
{
    public string? cacheHistory { get; private set; }
    public async Task<string> CalculateHistory()
    {
        if(cacheHistory != null)
            return cacheHistory;
        await Task.Delay(Random.Shared.Next(id*10, 100_000));
        cacheHistory = "history for employee " + id;
        return cacheHistory;
    }

}
