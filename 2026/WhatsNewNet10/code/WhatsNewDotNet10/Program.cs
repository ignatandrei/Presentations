using WhatsNewDotNet10;

Console.WriteLine("---- .NET 10 Features ----");
Console.WriteLine("---- string comparer with numeric ordering:"); 
Net10.String_ComparerDemo();
Console.WriteLine("---- JSON Duplicate Properties Handling:");
Net10.JSON_Duplicate();
//TODO: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/libraries#strict-json-serialization-options

Console.WriteLine("---- New Async Zip APIs:");
await Libraries.ZipLib();
//TODO: ? https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/libraries#websocket-enhancements
Console.WriteLine("---- dnx one-shot tool execution: ");
//https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk#one-shot-tool-execution
Console.WriteLine("Run dnx dotnetsay 'Hello, World!'");

Console.WriteLine("---- .NET CLI schema for tools: ");
//tools are listed here  :https://learn.microsoft.com/en-us/dotnet/navigate/tools-diagnostics/
Console.WriteLine("RUN dotnet clean --cli-schema");
Console.WriteLine("RUN dotnet nuget why --cli-schema");
Console.WriteLine("RUN dotnet ef --cli-schema");



Console.WriteLine("File based apps with publish ");
Console.WriteLine("https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk#file-based-apps-enhancements");


//TODO: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk#native-shell-tab-completion-scripts
//dotnet completions script pwsh | out-String | Invoke-Expression -ErrorAction SilentlyContinue

Console.WriteLine("---- C#14 ----");

Console.WriteLine("---- Extension members ----");
List<int>? numbers = [1, 2, 10];
Console.WriteLine($"Is numbers empty? {numbers.IsEmptyAndrei()}");

Console.WriteLine($"Is numbers empty New ? {numbers.IsEmptyNew()}");

Console.WriteLine($"Is numbers empty Property ? {numbers.IsEmptyProperty}");

Console.WriteLine($"Zero numbers array length: {List<int>.Zero.IsEmptyNew()}"); 

Console.WriteLine("---- python extensions ----");
Console.WriteLine("Test" * 3);
Console.WriteLine("---- javascript ---");    
Console.WriteLine("10" - 4);
Console.WriteLine("10" + "4");
Console.WriteLine("10" - "4");
Console.WriteLine("---  fsharp --- ");
var str = "Hello, C# code!"        
        | (s => s.Replace("C#", "F#-like "))
        | FunExtensions.ToUpper
;

Console.WriteLine(str); // NO! HELLO F#-LIKE CODE!

Console.WriteLine("--- field example ");
Person? p= new ();
p.FirstName = "andrei";
Console.WriteLine($"Person FirstName: {p.FirstName}");

Console.WriteLine("https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#more-partial-members");

Console.WriteLine("-- null conditional assignement");
//https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#null-conditional-assignment

p = null;
p?.FirstName = "Andrei";
Console.WriteLine($"Person FirstName with null conditional assignment: {p?.FirstName ?? "Person is null"}");


Console.WriteLine("---- End of C#14 ----");
/* More todo
 * 



 */
Console.WriteLine("---- End of .NET 10 Features ----");
