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


//I'm not confortable
//https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk#use-net-msbuild-tasks-with-net-framework-msbuild
/*
* Starting with .NET 10, msbuild.exe and Visual Studio 2026 can run MSBuild tasks that are built for .NET. 
*/

Console.WriteLine("File based apps with publish ");
Console.WriteLine("https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk#file-based-apps-enhancements");


//TODO: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk#native-shell-tab-completion-scripts
//dotnet completions script pwsh | out-String | Invoke-Expression -ErrorAction SilentlyContinue

/*TODO ASPIRE 
https://aspire.dev/whats-new/aspire-13/#net-maui-integration

https://aspire.dev/whats-new/aspire-13/#aspire-mcp-server

https://aspire.dev/whats-new/aspire-13/#health-checks-last-run-time

https://aspire.dev/whats-new/aspire-13/#c-file-based-app-support

https://aspire.dev/whats-new/aspire-13/#network-identifiers

https://aspire.dev/whats-new/aspire-13/#named-references

https://aspire.dev/whats-new/aspire-13/#%EF%B8%8F-breaking-changes
*/
Console.WriteLine("---- C#14 ----");

Console.WriteLine("---- Extension members ----");
List<int>? numbers = [1, 2, 10];
Console.WriteLine($"Is numbers empty? {numbers.IsEmptyAndrei()}");

Console.WriteLine($"Is numbers empty New ? {numbers.IsEmptyNew()}");

Console.WriteLine($"Is numbers empty Property ? {numbers.IsEmptyProperty}");

Console.WriteLine($"Zero numbers array length: {List<int>.Zero.IsEmptyNew()}"); 

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


? https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#declarative-model-for-persisting-state-from-components-and-services

? https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#new-javascript-interop-features

? https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#blazor-router-has-a-notfoundpage-parameter

? https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#improved-form-validation

The requirement to declare the model types outside of Razor components (.razor files) is due to the fact that both the new validation feature and the Razor compiler itself are using a source generator. Currently, output of one source generator can't be used as an input for another source generator.


https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#new-inputhidden-component-to-handle-hidden-input-fields-in-forms

https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#support-for-server-sent-events-sse



? https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#avoid-cookie-login-redirects-for-known-api-endpoints




https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#new-json-patch-implementation-with-systemtextjson



https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew#improved-translation-for-parameterized-collection

https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew#support-for-the-net-10-leftjoin-and-rightjoin-operators

https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew#named-query-filters


breaking changes
https://learn.microsoft.com/en-us/dotnet/core/compatibility/breaking-changes
*/
Console.WriteLine("---- End of .NET 10 Features ----");
