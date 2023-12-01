using Microsoft.Extensions.Options;
//see also
//https://ignatandrei.github.io/RSCG_Examples/v2/docs/Microsoft.Extensions.Options.Generators.OptionsValidatorGenerator
namespace WhatsNewASPNetCore8;

[DebuggerDisplay("{AppDisplayName}")]
public class MyAppOptions
{
    public const string ConfigName = "MyAppOptionsInConfig";
    //https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#data-validation
    [Required]
    //[MinLength(3)]
    [Length(4, 20)]
    public string AppDisplayName { get; set; } = string.Empty;

}


[OptionsValidator]
public partial class ValidatorForMyApp
    : IValidateOptions<MyAppOptions>
{
}
