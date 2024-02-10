using System.IO.Compression;

namespace ZipAsAService.BLL.Tests;

[TestClass()]
public class MakeZipTests
{
    [TestMethod()]
    public void ZipText_ShouldReturnByteArray_GivenNonEmptyString()
    {
        // Arrange
        var makeZip = new MakeZip();
        var text = "Hello, World!";

        // Act
        var result = makeZip.ZipText(text);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<byte[]>();
        result.Length.ShouldBeGreaterThan(0);
        result.Length.ShouldBe(135);
    }

    [TestMethod]
    public void ZipText_ShouldReturnOriginalText_WhenUnzipped()
    {
        // Arrange
        var makeZip = new MakeZip();
        var originalText = "Hello, World!";

        // Act
        var zippedBytes = makeZip.ZipText(originalText);

        // Unzip the bytes and retrieve the text
        using var memoryStream = new MemoryStream(zippedBytes);
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read);
        var entry = archive.GetEntry("text.txt");
        entry.ShouldNotBeNull();
        using var reader = new StreamReader(entry.Open());
        var unzippedText = reader.ReadToEnd();

        unzippedText.ShouldNotBeNullOrEmpty();
        unzippedText.ShouldBe(originalText);
    }
}