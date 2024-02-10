namespace ZipAsAService.BLL;

public class MakeZip
{
    public byte[] ZipText(string text)
    {
        var memoryStream = new MemoryStream();
        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("text.txt");
            using (var entryStream = entry.Open())
            using (var streamWriter = new StreamWriter(entryStream))
            {
                streamWriter.Write(text);
                streamWriter.Flush();
            }
        }
        return memoryStream.ToArray();
    }
}
