namespace InterceptAndreiConsole;
internal class Person
{
    public string? Name { get; set; }


    public string GetInfoAndrei()
    {
        return $"He has the name = {Name} ";
    }
}
