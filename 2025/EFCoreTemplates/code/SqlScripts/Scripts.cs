namespace SqlScripts;

public static class Scripts
{
    public static IEnumerable<string> GetScripts()
    {
        yield return SQL.Department;
        yield return SQL.Employee;
    }
}
