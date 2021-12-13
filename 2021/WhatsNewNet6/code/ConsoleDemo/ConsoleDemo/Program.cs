// See https://aka.ms/new-console-template for more information
// Demo: minimal program
// Demo: global usings: see See file globalusing.cs.
// Read more at https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives

WriteLine("Hello Worldx");
await ChunkWithThreads();
return;
CsharpVsPython();
MaxByExample();

//Demo: MaxBy
void MaxByExample()
{
    var list = Employee.GetFake();
    WriteLine( list.MaxBy(it => it.Salary));
}
//Demo: Chunk
async Task<int> ChunkWithThreads()
{
    Console.WriteLine(" ALL at once");
    var arr=Enumerable.Range(1,23).Select(it=>CreateT(it));
    await Task.WhenAll( arr.ToArray());
    Console.WriteLine(" in chunks");
    var data = arr.Chunk(Environment.ProcessorCount);
    foreach (var item in data)
    {
        var arrChunk = item.ToArray();
        await Task.WhenAll(arrChunk);
        Console.WriteLine("!!!batch completend for "+ arrChunk.Length);
    }
    return 0;
}

async Task<int> CreateT(int id)
{
    await Task.Delay(Random.Shared.Next(1000,2000));
    Console.WriteLine("task" + id);
    return id;
}


//https://www.red-gate.com/simple-talk/development/dotnet-development/10-reasons-python-better-than-c-sharp/
void CsharpVsPython() {
    //Point 1 -  Indented code blocks
    //With curly braces I can indent whatever way I want - I am not constrained


    foreach (var i in Range(1, 11))
    {
        if (i % 3 == 0)
            WriteLine("fizz");
        else if (i % 5 == 0)
            WriteLine("buzz");

    }

    //Also , when I want to comment something , with C# I know when the else if is
    //terminating without counting tabs. Just see where the { is closing with }



    //Point 2 -  Self-declaring Variables
    //Seriously? What is the difference between Python
    //meaning_of_life = 42
    //and C#

    var meaning_of_life = 42;


    //Point 3 -– Those modules
    //same code as Python
    foreach (var file in EnumerateFiles(@"C:\"))
    {
        WriteLine(file);
    }

    //Point 4 – Simplicity of Data Structures
    //Quoting : Purists will say that you need all the different data types that C# provides, but trust me, you don’t.
    //modelate a CallCenter waiting for phones - Stack or Queue ?
    //modelate a CallCenter waiting for phones with business consumer that has  priority  - https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue

    //Point 5 - Slicing
    //is the same
    var sins = new[]
    {

 "pride", "envy", "gluttony", "greed", "lust", "sloth", "wrath"
};
    //var selected_sins = sins[1..(sins.Length-1)];
    var selected_sins = sins[1..^1];
    WriteLine(Join(" ", selected_sins));


    //6 – For loops
    //same syntax in C#
    foreach (var i in Range(0, 5))
    {
        WriteLine(i);
    }
    // words in a string
    string[] words = { "Simple", "Talk" };
    foreach (string word in words)
    {
        WriteLine(word);
    }
    //Quoting :  "(unlike in C#, where sometimes you use for and sometimes foreach)"
    //then  enumerate an array backwards in python without reversing

    //7 – List comprehensions
    var nr = Range(1, 11).Where(it => it % 2 == 0).Select(it => Pow(it, 3));
    WriteLine(Join(" ", nr));

    //8 – Sets
    //C# example
    //this contains some duplicates
    var languages = new[] { "C#", "Python", "VB", "Java", "C#", "Java", "C#" };
    //more dense than Python
    languages = new HashSet<string>(languages).ToArray();
    //this will give: ['C#', 'Java', 'Python', 'VB']
    WriteLine(Join(" ", languages));

    //second example
    //create two sets: Friends characters and large Antarctic ice shelves
    var friends = new[] { "Rachel", "Phoebe", "Chandler", "Joey", "Monica", "Ross" };
    var ice_shelves = new[] { "Ronnie-Filchner", "Ross", "McMurdo" };
    // show the intersection (elements in both lists)
    WriteLine(Join(" ", friends.Intersect(ice_shelves)));
    //show the union (elements in either list)
    WriteLine(Join(" ", friends.Union(ice_shelves)));
    // show the friends who aren't ice shelves
    WriteLine(Join(" ", friends.Except(ice_shelves)));
    // elements in either set but not both -- more difficult
    WriteLine(Join(" ", friends.Union(ice_shelves).Except(friends.Intersect(ice_shelves))));

    //9 – Working with files and folders
    //# list of 3 people
    var people = new[] { "Shadrach", "Meshach", "Abednego" };
    //# write them to a file
    File.WriteAllLines(@"C:\all\a.txt", people);

    //10 – The quality of online help
    // https://docs.microsoft.com/ - good tutorials
}