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
TimeProvider tp = TimeProvider.System;

Console.WriteLine($"Data: {GetDayOfWeekNow(tp)} ");
tp = new FakeTp();
Console.WriteLine($"Data: {GetDayOfWeekNow(tp)} ");

static DayOfWeek GetDayOfWeekNow(TimeProvider tp)
{
    return tp.GetUtcNow().DayOfWeek;
}