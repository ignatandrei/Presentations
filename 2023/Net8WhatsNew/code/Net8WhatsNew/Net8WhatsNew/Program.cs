using Microsoft.Extensions.Time.Testing;
using System;

Console.WriteLine("Zip file to stream");

var curDir = Environment.CurrentDirectory;
var pathToExtract = Path.Combine(curDir, "test");
if(Directory.Exists(pathToExtract) )
    Directory.Delete(pathToExtract, true);
byte[] zipBytes;

using (MemoryStream ms = new())
{
    ZipFile.CreateFromDirectory(curDir, ms);
    zipBytes = ms.ToArray();
}

using MemoryStream ms1=new(zipBytes);
ZipFile.ExtractToDirectory(ms1, pathToExtract);

// time zone provider, use DI
TimeProvider tp;

tp=TimeProvider.System;

Console.WriteLine($"Data: {GetDayOfWeekNow(tp)} ");
tp = new FakeTp();
Console.WriteLine($"Data: {GetDayOfWeekNow(tp)} ");

var fake= new FakeTimeProvider(DateTimeOffset.UtcNow.AddDays(-3));
tp = fake;
Console.WriteLine($"Data: {GetDayOfWeekNow(tp)} ");
fake.Advance(TimeSpan.FromDays(3));
Console.WriteLine($"Data: {GetDayOfWeekNow(tp)} ");

Console.WriteLine("Delay 1 second");
//await 1 second
await Task.WhenAny(Task.Delay(TimeSpan.FromSeconds(1 )), DelayMoreSeconds(10, fake));
Console.WriteLine("end delay one second Delay");

Console.WriteLine("Delay 10 second");

//await 10 seconds because of fake time provider
await Task.WhenAny(Task.Delay(TimeSpan.FromSeconds(1),tp), DelayMoreSeconds(10, fake));

Console.WriteLine("end delay ten second Delay");

static DayOfWeek GetDayOfWeekNow(TimeProvider tp)
{
    return tp.GetUtcNow().DayOfWeek;
}

async Task DelayMoreSeconds(int seconds, FakeTimeProvider tp)
{
    //on system
    await Task.Delay(TimeSpan.FromSeconds(seconds));
    tp.Advance(TimeSpan.FromSeconds(seconds));
}
