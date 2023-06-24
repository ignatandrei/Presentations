using System.Numerics;
https://learn.microsoft.com/ro-ro/dotnet/csharp/whats-new/csharp-11

//1. File-scoped types  and required MyProperty 
var a1 = new Y() { MyProperty = 1 };
//var a2 = new X();//cannot 

//2. interpolation
var x = "\"Logging\": {\r\n    \"LogLevel\": {\r\n      \"Default\": \"Information\",\r\n      \"Microsoft\": \"Warning\",\r\n      \"Microsoft.Hosting.Lifetime\": \"Information\"\r\n    }\r\n  }";
var logLevelDefault = "Information";
var y = $$"""
"Logging": {
    "LogLevel": {
      "Default": "{{logLevelDefault}}",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
""";
Console.WriteLine(x);
Console.WriteLine(y);
Console.WriteLine(x == y);

//3. static in interfaces
//IParseable - see in ASP.NET  Core
//generic math => see Max below

Console.WriteLine(Sum<int, long>(1, 2));
Console.WriteLine(Sum<int, long>(int.MaxValue, int.MaxValue));
// see create checked below
//Console.WriteLine(Sum<long, int>(3, long.MaxValue));
Console.WriteLine(Sum<double, double>(1, 2));
static TResult Sum<T, TResult>(params T[] values)
    where T : INumberBase<T>
    where TResult : INumberBase<TResult>
{
    TResult result = TResult.Zero;

    foreach (var value in values)
    {
        result += TResult.CreateChecked(value);
    }

    return result;
}