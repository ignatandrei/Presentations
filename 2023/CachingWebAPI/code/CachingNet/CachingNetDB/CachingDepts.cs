using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using TableDependency.SqlClient;

namespace CachingNetDB;
public class CachingDepts
{
    private readonly TestContext testContext;
    private readonly IMemoryCache cache;
    //see partial void OnModelCreatingPartial - for trigger!
    static SqlTableDependency<Department>? dep1= null;
    static CancellationTokenSource tokenSource=new();
    public CachingDepts(TestContext testContext, IMemoryCache cache)
    {
        
        this.testContext = testContext;
        this.cache = cache;
        var ConnectionString = testContext.Database.GetConnectionString()??"";
        //TODO: lock
        if (dep1 != null) return;
        dep1 = new SqlTableDependency<Department>(ConnectionString,"Department");
        dep1.OnChanged += Dep1_OnChanged;
        dep1.Start();
    }

    private void Dep1_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Department> e)
    {
        Console.WriteLine("changed" + e.ChangeType);
        tokenSource.Cancel();
        tokenSource.Dispose();
        tokenSource = new();
    }

    public async Task<Department[]> GetDepartmentsWithCaching()
    {
        if(!cache.TryGetValue<Department[]>("Deps",out var value))
        {
            Console.WriteLine("load data from dep");
            var data = await testContext.Departments.ToArrayAsync();
            var token = new CancellationChangeToken(tokenSource.Token);
            cache.Set<Department[]>("Deps", data, token);
        }
        else
        {
            Console.WriteLine("data from cache");
        }
        return cache.Get<Department[]>("Deps")!;   
    }

    
}
