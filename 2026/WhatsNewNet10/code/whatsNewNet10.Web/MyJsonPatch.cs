using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using System;
using System.Net;
using System.Text.Json;

namespace whatsNewNet10.Web;

public class MyJsonPatch
{
    public static void ApplyPatch()
    {
        // Original object
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@gmail.com",
            Address = new Address
            {
                Street = "123 Main St",
                City = "Anytown",
                State = "TX"
            }
        };

        // Raw JSON Patch document
        var jsonPatch = """
[
  { "op": "replace", "path": "/FirstName", "value": "Jane" },
  { "op": "remove", "path": "/Email"},
  { "op": "add", "path": "/Address/ZipCode", "value": "90210" },
  
]
""";

        JsonSerializerOptions serializerOptions = new(JsonSerializerOptions.Default);
        serializerOptions.AllowTrailingCommas = true;

        Console.WriteLine(JsonSerializer.Serialize(person, serializerOptions));

        // Deserialize the JSON Patch document
        var patchDoc = JsonSerializer.Deserialize<JsonPatchDocument<Person>>(jsonPatch,serializerOptions);

        // Apply the JSON Patch document
        patchDoc!.ApplyTo(person);
        // Output updated object
        Console.WriteLine(JsonSerializer.Serialize(person, serializerOptions));

        
    }
}
