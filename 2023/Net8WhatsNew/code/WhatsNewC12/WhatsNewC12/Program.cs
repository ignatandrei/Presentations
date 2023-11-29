// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12
using WhatsNewC12;
Console.WriteLine("More differences at https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12");
Console.WriteLine("record vs class");
var pc=new PersonClass("Ignat", "Andrei");
var pr=new PersonRecord("Ignat", "Andrei");
Console.WriteLine(pc.FullName);
Console.WriteLine(pr.FullName);
Console.WriteLine(pc.firstName);
Console.WriteLine(pr.firstName);
//this does not exists - because it is a class!
//Console.WriteLine(pc.LastName);
Console.WriteLine(pr.lastName);