using System.IO.Compression;

namespace WhatsNewDotNet10;

internal class Libraries
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/libraries#new-async-zip-apis
    /// </summary>
    /// <returns></returns>
    public static async Task ZipLib()
    {
        string archive=Path.Combine(Environment.CurrentDirectory, "archive_async.zip");
        if(File.Exists(archive)) File.Delete(archive);
        await ZipFile.CreateFromDirectoryAsync(Path.Combine(Environment.CurrentDirectory,"obj"), archive);
        Console.WriteLine("Created archive: "+archive);
    }
}
