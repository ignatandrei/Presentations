//see https://ignatandrei.github.io/RSCG_Examples/v2/docs/PrimaryParameter
//for more details to transform automatically to record
namespace WhatsNewC12;
internal class PersonClass(string firstName, string lastName)
{
    public string firstName { get; } = firstName;
    public string FullName => $"{firstName} {lastName}";

}
