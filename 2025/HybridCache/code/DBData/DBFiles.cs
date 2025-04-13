namespace DBData;

public static class DBFiles
{
    public static IEnumerable<string> FilesToCreateEmpDep
    {
        get
        {
            using (var reader = EmbeddedResources.sql_001_CreateDB_sql_Reader)
            {
                string sql = reader.ReadToEnd();
                yield return sql;
            }
            
        }
    }
    public static IEnumerable<string> FilesToCreateCache
    {
        get
        {
            using (var reader = EmbeddedResources.sql_003_CreateCache_sql_Reader)
            {
                string sql = reader.ReadToEnd();
                yield return sql;
            }
            
        }
    }

} 
    

