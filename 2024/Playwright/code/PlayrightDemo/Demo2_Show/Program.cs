// See https://aka.ms/new-console-template for more information
using Microsoft.Playwright;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
var curDir = Directory.GetCurrentDirectory();
var curDirVideos = Path.Combine(curDir, "videos");
if(!Directory.Exists(curDirVideos))
{
    Directory.CreateDirectory(curDirVideos);
}
var curDirScreenshots = Path.Combine(curDir, "screenshots");
if (!Directory.Exists(curDirScreenshots))
{
    Directory.CreateDirectory(curDirScreenshots);
}
using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync();
var context = await browser.NewContextAsync(new()
{
    RecordVideoDir = curDirVideos
});
var page = await context.NewPageAsync();
await page.Clock.InstallAsync(new()
{
    TimeDate = new DateTime(1970, 4, 16, 0, 0, 0)
});
await page.GotoAsync("http://localhost:5348/");
var pathFilePng = Path.Combine(curDirScreenshots, "screenshot1.png");
await page.ScreenshotAsync(new PageScreenshotOptions { Path = pathFilePng });
Process.Start(new ProcessStartInfo(pathFilePng) { UseShellExecute = true });
Console.WriteLine("Waiting for the page to load...");
await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
var str = await page.ContentAsync();
pathFilePng = Path.Combine(curDirScreenshots, "screenshot2.png");
await page.ScreenshotAsync(new PageScreenshotOptions { Path = pathFilePng });
Process.Start(new ProcessStartInfo(pathFilePng) { UseShellExecute = true });
Console.WriteLine("See videos at "+curDirVideos);
await context.CloseAsync();
await browser.CloseAsync();