using InterceptAndreiConsole;

Console.WriteLine("Hello, World!");
Person p=new ();
p.Name="Andrei Ignat";
Console.WriteLine(p.GetInfoAndrei("test"));


var pers=new Person();
var data=pers.GetInfoAndrei("newtest");

Console.WriteLine(data);