namespace DBData;
//dotnet sql-cache create "Server=127.0.0.1,1433;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=true;Initial Catalog=CachingData" dbo TestCache
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
            using (var reader = EmbeddedResources.sql_003_CreateCache_sql_Reader)
            {
                string sql = reader.ReadToEnd();
                yield return sql;
            }
        }
    }

} 
    

