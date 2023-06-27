using System.ComponentModel.DataAnnotations;

public class Person: IValidatableObject
{
    //public bool IsValid()
    //{

    //}
    public int id { get; set; }
    public string?  Name { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.id % 2 == 1)
            yield return new ValidationResult
                (" asdadasdid is not valid", new string[] { "id" });

    }
}
