```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3155/23H2/2023Update/SunValley3)
13th Gen Intel Core i9-13905H, 1 CPU, 20 logical and 14 physical cores
.NET SDK 8.0.200
  [Host]     : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX2
  Job-JUPOKP : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX2

RunStrategy=Throughput  

```
| Method     | Mean      | Error     | StdDev    | Median   | Gen0   | Allocated |
|----------- |----------:|----------:|----------:|---------:|-------:|----------:|
| Reflection | 11.070 μs | 1.1496 μs | 3.3897 μs | 9.469 μs | 0.6943 |   8.53 KB |
| RSCG       |  7.896 μs | 0.1504 μs | 0.1333 μs | 7.873 μs | 0.6866 |   8.53 KB |
