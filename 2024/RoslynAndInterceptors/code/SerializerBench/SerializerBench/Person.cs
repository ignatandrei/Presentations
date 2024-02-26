using System.Text.Json.Serialization;

namespace SerializerBench;
internal class PersonReflection
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

[JsonSerializable(typeof(PersonRSCG))]
internal partial class PersonContext : JsonSerializerContext
{
}