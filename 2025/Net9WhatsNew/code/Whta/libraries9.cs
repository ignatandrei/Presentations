
namespace Whta;

internal partial class libraries9
{
    internal static void Base64Demo()
    {
        string toEncode = "StringToEncode";
        var bytes = Encoding.UTF8.GetBytes(toEncode);
        var base64 = Convert.ToBase64String(bytes);
        Console.WriteLine(base64);
        //https://learn.microsoft.com/en-us/dotnet/api/system.web.httputility.urlencode?view=net-9.0
        //
        var base64Url = Base64Url.EncodeToString(bytes);
        Console.WriteLine(base64Url);

    }
    internal static void BinaryFormatterDemo()
    {
        //https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-migration-guide/
        var obj = new { Name = "John", Age = 30 };
        //var formatter = new BinaryFormatter();
        //using (var stream = new MemoryStream())
        //{
        //    formatter.Serialize(stream, obj);
        //    stream.Position = 0;
        //    var obj2 = formatter.Deserialize(stream);
        //    Console.WriteLine(obj2);
        //}
    }
    internal static void OrderedDictionaryDemo()
    {
        //should array dictionary be used instead?
        OrderedDictionary<string, int> d = new()
        {
            ["a"] = 1,
            ["b"] = 2,
            ["c"] = 3,
        };
        foreach (KeyValuePair<string, int> entry in d)
        {
            Console.WriteLine(entry);
        }
        d.Add("d", 4);
        d.RemoveAt(0);
        d.RemoveAt(2);
        d.Insert(0, "e", 5);
        Console.WriteLine("---------------------------------------");
        foreach (KeyValuePair<string, int> entry in d)
        {
            Console.WriteLine(entry);
        }
    }
    internal static void TimeSpanDemo()
    {
        TimeSpan timeSpan1 = TimeSpan.FromSeconds(value: 101.832);
        Console.WriteLine($"timeSpan1 = {timeSpan1}");
        // timeSpan1 = 00:01:41.8319999

        TimeSpan timeSpan2 = TimeSpan.FromSeconds(seconds: 101, milliseconds: 832);
        Console.WriteLine($"timeSpan2 = {timeSpan2}");
        // timeSpan2 = 00:01:41.8320000
    }
    internal static void DebugAssertDemo()
    {
        int a = 1;
        int b = 2;
        a--; b--; b--;
        //public static void Assert([DoesNotReturnIf(false)] bool condition, [CallerArgumentExpression("condition")] string? message = null);
        Debug.Assert(a > 0 && b > 0);
    }
    internal static void LinqCountAggregateDemo()
    {
        string sourceText = """
    Lorem ipsum dolor sit amet, consectetur adipiscing elit.
    Sed non risus. Suspendisse lectus tortor, dignissim sit amet, 
    adipiscing nec, ultricies sed, dolor. Cras elementum ultrices amet diam.
""";

        // Find the most frequent word in the text.
        var words = sourceText
            .Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word.ToLowerInvariant())
            .ToArray();

        var counts = words.CountBy(word => word);
        var maxWord = counts.MaxBy(pair => pair.Value);

        Console.WriteLine(maxWord.Key + "- -" + maxWord.Value);
        var sum = counts.Aggregate(
            seed: 0,
             func: (acc, curr) => acc + curr.Value * curr.Key.Length,
             resultSelector: acc => acc
            );
        Console.WriteLine(sum);
        var aggregatedData1 =
            counts.AggregateBy(
                keySelector: entry => entry.Key,
                seed: 0,
                func: (totalScore, curr) => totalScore + curr.Value + curr.Key.Length
                );
        foreach ((int index, var kv) in aggregatedData1.Index())
        {
            Console.WriteLine($"Line number: {index + 1}, item: {kv.Key} {kv.Value}");
        }

    }
    //https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/libraries#searchvalues-expansion
    /// <summary>
    /// This new capability has an optimized implementation that takes advantage of the SIMD support in the underlying platform. 
    /// It also enables higher-level types to be optimized. 
    /// For example, Regex now utilizes this functionality as part of its implementation.
    /// </summary>
    public static void SearchValuesDemo()
    {
        SearchValues<string> s_animals =
    SearchValues.Create(["cat", "fox", "dog", "dolphin"], StringComparison.OrdinalIgnoreCase);
        var text =
"The quick brown fox jumps over the lazy dog";

        var item = text.AsSpan().IndexOfAny(s_animals);
        Console.WriteLine(item);
    }

    [GeneratedRegex(@"^a...e$")]
    public static partial Regex ExampleFunc();

    [GeneratedRegex(@"^a...e$")]
    public static partial Regex ExampleProperty { get; }

    public static async Task<bool> GenerateGuidV7()
    {
        var x = Guid.CreateVersion7(DateTime.UtcNow);
        Console.WriteLine(x);
        await Task.Delay(1);
        x = Guid.CreateVersion7(DateTime.UtcNow);
        Console.WriteLine(x);
        await Task.Delay(1);
        x = Guid.CreateVersion7(DateTime.UtcNow);
        Console.WriteLine(x);
        return true;

    }
    public static void BigMulDemo()
    {
        var maxInt = int.MaxValue;
        var mul = Math.BigMul(maxInt, maxInt);
        Console.WriteLine(mul);
        //checked
        {
            var y = maxInt * maxInt;
            Console.WriteLine(y);
        }
    }
    private static async Task<string> Delay(int nr)
    {
        await Task.Delay(nr);
        //if(nr == 3000)
        //{
        //    throw new Exception("3000");
        //}
        return "am asteptat "+ nr.ToString();
    }
    public static async Task<bool> WhenEachDemo()
    {

        Task<string> data1 = Delay(1000);
    
        Task<string> data2 = Delay(4000);
        Task<string> data3 = Delay(3000);

        await foreach (Task<string> t in Task.WhenEach(data1, data2, data3))
        {
            Console.WriteLine(t.IsCompletedSuccessfully + "--"+ t.Result);
        }
        return true;
    }
}