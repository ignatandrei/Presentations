using BenchmarkDotNet.Running;
using SerializerBench;

var summary = BenchmarkRunner.Run<BenchMarkData>();