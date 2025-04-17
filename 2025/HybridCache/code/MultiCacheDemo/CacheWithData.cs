using DBData;

namespace MultiCacheDemo;
public class CacheWithData<T>
{
    public DateTime Created { get; set; }
    
    public int SecondsElapsed => (int)(DateTime.Now - Created).TotalSeconds;
    public string CreatedString => Created.ToString("HH:mm:ss");
    public T Data { get; set; }
    public CacheWithData(T data)
    {
        Created = DateTime.Now;
        Data = data;
        TypeOfData= typeof(T).Name;
    }
    public string TypeOfData { get; set; }
}

public class EmployeesCache : CacheWithData<EmployeeDisplay[]>
{
    public EmployeesCache(EmployeeDisplay[] data) : base(data)
    {
    }

    public static implicit operator EmployeesCache(EmployeeDisplay[] data)
    {
        return new EmployeesCache(data);
    }
}

public class DepartmentsCache : CacheWithData<DepartmentDisplay[]>
{
    public DepartmentsCache(DepartmentDisplay[] data) : base(data)
    {
    }

    public static implicit operator DepartmentsCache(DepartmentDisplay[] data)
    {
        return new DepartmentsCache(data);
    }
}
