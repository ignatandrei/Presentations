namespace InterceptAndreiConsole;
internal class Person
{
    public string? Name { get; set; }


    public string GetInfoAndrei(string data)
    {
        return $"He has the name = {Name} with data {data}";
    }
}
