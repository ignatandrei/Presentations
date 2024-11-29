// See https://aka.ms/new-console-template for more information
using WhtaCSharp13;

Console.WriteLine("Hello, World!");

Console.WriteLine(ShowParams.Sum(1,3,2));
Console.WriteLine(ShowParams.Sum2(1, 3, 2));
await MyLock.Lock();
await MyLock.Lock2();
await MyLock.LockScope();
//TODO: partial properties
Person person = new ();
person.Name = "Andrei Ignat";
Console.WriteLine(person.Name);
person.Age = -55;