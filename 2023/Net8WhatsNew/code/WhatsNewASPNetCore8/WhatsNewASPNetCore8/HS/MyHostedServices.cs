    namespace WhatsNewASPNetCore8.HS;

public class MyHostedServices1 : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("start 1 at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        await Task.Delay(5000);

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("end at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        return Task.CompletedTask;
    }
}

public class MyHostedServices2 : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("start 2 at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        await Task.Delay(5000);

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("end at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        return Task.CompletedTask;
    }
}
