using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace ValidatorDemo;

public class MyAppOptions
{
    public const string ConfigName = "MyAppOptionsInConfig";
    [Required]
    [MinLength(3)]
    public string AppDisplayName { get; set; } = string.Empty;

    //[ValidateObjectMembers(
    //    typeof(SecondValidatorNoNamespace))]
    //public SecondModelNoNamespace? P2 { get; set; }
}

[OptionsValidator]
public partial class ValidatorForMyApp : IValidateOptions<MyAppOptions>
{
}
