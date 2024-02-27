using InterceptAndreiConsole;

Console.WriteLine("Hello, World!");
Person p=new ();
p.Name="Andrei Ignat";
Console.WriteLine(p.GetInfoAndrei("test"));

var data=p.GetInfoAndrei("newtest");

Console.WriteLine(data);