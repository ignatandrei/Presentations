using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace WhatsNewDotNet10;

internal class Net10
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/libraries#numeric-ordering-for-string-comparison
    /// </summary>
    public static void String_ComparerDemo()
    {
        StringComparer stringComparer = StringComparer.Create(CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);

        StringComparer numericStringComparer = StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);

        Console.WriteLine(numericStringComparer.Equals("02", "2"));
        Console.WriteLine(stringComparer.Equals("02", "2"));

        var arr = new[] { "Windows 8", "Windows 10", "Windows 11" };
        foreach (string os in arr.Order(numericStringComparer))
        {
            Console.WriteLine(os);
        }

        Console.WriteLine("----");

        foreach (string os in arr.Order(stringComparer))
        {
            Console.WriteLine(os);
        }

        HashSet<string> set = new HashSet<string>(numericStringComparer) { "007" };
        Console.WriteLine(set.Contains("7"));

        HashSet<string> set1 = new HashSet<string>(stringComparer) { "007" };
        Console.WriteLine(set1.Contains("7"));
    }

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/libraries#option-to-disallow-duplicate-json-properties
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/libraries#strict-json-serialization-options
    /// </summary>
    public static void JSON_Duplicate()
    {
        string json = """
            { 
            "Value": 1, 
            "Value": -1 
            }
            """;
        JsonDocumentOptions duplicate = new() { AllowDuplicateProperties = true };
        JsonDocument.Parse(json, duplicate);

        Console.WriteLine("updated value is "+JsonSerializer.Deserialize<MyRecord>(json)!.Value); // -1

        JsonSerializerOptions options = new() { AllowDuplicateProperties = true };
        JsonSerializer.Deserialize<MyRecord>(json, options);                
        JsonSerializer.Deserialize<JsonObject>(json, options);              
        JsonSerializer.Deserialize<Dictionary<string, int>>(json, options); 

        JsonDocumentOptions notDuplicate = new() { AllowDuplicateProperties = false };
        try
        {
            JsonDocument.Parse(json, notDuplicate);   // throws JsonException
            //same for deserialization
        }
        catch (JsonException)
        {
            Console.WriteLine("JsonException thrown as expected.");
        }


    }
}

record MyRecord(int Value);
