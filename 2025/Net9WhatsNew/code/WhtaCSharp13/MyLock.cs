namespace WhtaCSharp13;

internal class MyLock
{
    static object _lock = new object();
    static Lock lock1 = new Lock();
    public static async Task<bool> Lock()
    {
        lock (_lock)
        {
            Console.WriteLine("Locked");
            //await Data();
        }
        return true;
    }
    static async Task<bool> Data()
    {
        await Task.Delay(1000);
        return true;
    }

    public async static Task<bool> Lock2()
    {
        await Task.Delay(1000);
        //see with IL what is generated
        lock (lock1)
        {
            Console.WriteLine("Locked");
            //await Data();
        }
        return true;
    }
    public async static Task<bool> LockScope()
    {
        await Task.Delay(1000);
        //use semaphore / mutex
        using (lock1.EnterScope())
        {
            Console.WriteLine("Locked");
            //await Data();
        }
        return true;
    }

}
