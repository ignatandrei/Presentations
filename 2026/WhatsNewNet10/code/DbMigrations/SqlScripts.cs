using System.Text;

namespace DbMigrations;

public partial class SqlScripts
{
    [EmbedResourceCSharp.FolderEmbed("data/","*.sql")]
    public static partial System.ReadOnlySpan<byte> GetContentOfFiles(System.ReadOnlySpan<char> path);

    public static IEnumerable<string> GetAllSqlScripts()
    {
         yield return Encoding.UTF8.GetString(GetContentOfFiles("001.db.sql"));
        yield return Encoding.UTF8.GetString(GetContentOfFiles("002.data.sql"));

    }

}
