namespace DBData;

public static class DBFiles
{
    public static IEnumerable<string> FilesToCreateDB
    {
        get
        {
            using (var reader = EmbeddedResources.sql_001_CreateDB_sql_Reader)
            {
                string sql = reader.ReadToEnd();
                yield return sql;
            }
            using (var reader = EmbeddedResources.sql_002_InsertData_sql_Reader)
            {
                string sql = reader.ReadToEnd();
                yield return sql;
            }
        }
    }

} 
    

