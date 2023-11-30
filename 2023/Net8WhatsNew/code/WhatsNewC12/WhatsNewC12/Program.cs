// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12
using WhatsNewC12;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Point = (int x, int y);
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
Point p=new(1,2);
Console.WriteLine(p.ToString());

int[] a=[1,2,3];
List<int> b = [1,2,3];
int[] moreNumbers = [.. a, .. b, 7, 8, 9];
//Dictionary<int, string> c = [1:"one", 2:"two", 3:"three"];

