namespace GlobalUsings;
record Employee
{
    public int id { get; set; }

    public int Salary { get; set; }

    public static Employee[] GetFake()
    {

        return Enumerable.Range(1, 10).Select(it =>
             new Employee()
             {
                 id = it,
                 Salary = 100 - it
             })
        .ToArray();
    }
}
