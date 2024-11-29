namespace WhtaCSharp13;

internal class Person
{
    public string Name {
        get;
        //set;
        set {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            field = value.ToUpper();
        }
    }
    public int Age
    {
        get;
        set => field = (value >= 0)
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Age must not be negative");
    }

}

