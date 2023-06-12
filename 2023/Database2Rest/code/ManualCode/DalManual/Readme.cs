namespace DalManual;

public class Readme
{
    /// <summary>
    /// execute this in readme.cs folder
    /// </summary>
    /// <returns>connection string</returns>
    public static string Scaffold()
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string
        return """
        dotnet ef dbcontext scaffold "Trusted_Connection=No;Data Source=.;Initial Catalog=testsDatabase2Rest;UID=sa;PWD=<YourStrong@Passw0rd>;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer --force --no-pluralize  --json        
        """;
    }
}