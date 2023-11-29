
namespace WhatsNewC12;
internal record PersonRecord(string firstName, string lastName)
{
    public string FullName => $"{firstName} {lastName}";

}
