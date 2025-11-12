namespace WhatsNewDotNet10;

internal class Person
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#the-field-keyword
    /// </summary>
    public string FirstName
    {
        get
        {
            if(field.Length<2)  return field.ToUpper();
            return char.ToUpper(field[0]) + field.Substring(1);
        }
        set
        {
            field = value ?? throw new ArgumentNullException(nameof(value));
        }
    } = string.Empty;
   
}
