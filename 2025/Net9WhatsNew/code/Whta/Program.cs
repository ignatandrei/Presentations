//TODO: https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-9/
Console.WriteLine("Hello, World!");
runtime9.FeatureSwitchDemo();
runtime9.LoopCounterDemo();
libraries9.Base64Demo();
libraries9.BinaryFormatterDemo();
libraries9.OrderedDictionaryDemo();
libraries9.TimeSpanDemo();
//libraries9.DebugAssertDemo();
libraries9.LinqCountAggregateDemo();
libraries9.SearchValuesDemo();


string testString = "apple";
if(libraries9.ExampleFunc().IsMatch(testString))
{
    Console.WriteLine("Matched");
}
else
{
    Console.WriteLine("Not Matched");
}

if (libraries9.ExampleProperty.IsMatch(testString))
{
    Console.WriteLine("Matched");
}
else
{
    Console.WriteLine("Not Matched");
}
//JSON Serialization/Deserialization
//mention: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/extract-schema
//mention:https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/use-utf8jsonreader#read-multiple-json-documents
//mention: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/libraries#dependency-injection---activatorutilitiescreateinstance-constructor
//mention:PersistedAssemblyBuilder 
//https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/libraries#persisted-assemblies

await libraries9.GenerateGuidV7();
libraries9.BigMulDemo();
await libraries9.WhenEachDemo();