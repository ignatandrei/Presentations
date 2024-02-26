using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;
using System.Text.Json;

namespace SerializerBench;

[HtmlExporter]
//[InProcess]
//[RankColumn, MinColumn, MaxColumn, Q1Column, Q3Column, AllStatisticsColumn]
[MemoryDiagnoser]
[SimpleJob(RunStrategy.Throughput)]
public class BenchMarkData
{
    private static List<PersonReflection> personReflection = new ();
    private static List<PersonRSCG> personRSCG = new ();
    static BenchMarkData()
    {
        for (int i = 0; i < 100; i++)
        {
            personReflection.Add(new ()
            {
                FirstName = "Andrei",
                LastName = "Ignat"
            });
            personRSCG.Add(new()
            {
                FirstName = "Andrei",
                LastName = "Ignat"
            });
        }
    }

    [Benchmark]
    public string? Reflection()
    {
        return JsonSerializer.Serialize(personReflection);
    }
    [Benchmark]
    public string? RSCG()
    {
        return JsonSerializer.Serialize(personRSCG);
    }

}