using System.Collections.Concurrent;

namespace HardWorkingBLL;
public class EmployeeHistory
{
    static ConcurrentDictionary<string, EmployeeData> EmployeeID = new ();
    
    public async Task<string> CalculateEmployeeHistory(int id)
    {
        string key = Guid.NewGuid().ToString("N");
        var empData=new EmployeeData(id);
        empData.CalculateHistory();
        EmployeeID.TryAdd(key,empData);
        return key;
    }
    public async Task<string?> GetEmployeeHistory(string guid)
    {
        if(!EmployeeID.TryGetValue(guid,out var data))
        {
            throw new ArgumentException("not found history");
        }
        //simulate that is harder to obtain data
        var l = EmployeeID.Count(it => !string.IsNullOrWhiteSpace(it.Value.cacheHistory));
        await Task.Delay (EmployeeID.Count * 100);
        return data.cacheHistory;        
    }


}