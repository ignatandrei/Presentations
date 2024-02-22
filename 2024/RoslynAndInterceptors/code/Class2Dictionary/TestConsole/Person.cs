using RSCG_TemplatingCommon;

namespace TestConsole;

[IGenerateDataFromClass("ClassPropByName")]
public partial class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
