using WhtaCSharp13;

Console.WriteLine("Hello, World!");

Console.WriteLine(ShowParams.Sum(1,3,2));
Console.WriteLine(ShowParams.Sum2(1, 3, 2));
await MyLock.Lock();
await MyLock.Lock2();
await MyLock.LockScope();
Person person = new ();
person.Name = "Andrei Ignat";
Console.WriteLine(person.Name);
person.Age = -55;

//partial properties: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#more-partial-members
