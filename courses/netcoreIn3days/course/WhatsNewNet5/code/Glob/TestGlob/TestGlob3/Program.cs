using System;

namespace TestGlob3
{
    //https://github.com/dotnet/runtime/issues/43736
    //https://jimmybogard.com/mind-your-strings-with-net-5-0/
    class Program
    {
        static void Main(string[] args)
        {
            var actual = "Detail of supported commands\n============\n## Documentation produced for DelegateDecompiler, version 0.28.0 on Thursday, 22 October 2020 16:03\n\r\nThis file documents what linq commands **DelegateDecompiler** supports when\r\nworking with [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) (EF).\r\nEF has one of the best implementations for converting Linq `IQueryable<>` commands into database\r\naccess commands, in EF's case T-SQL. Therefore it is a good candidate for using in our tests.\r\n\r\nThis documentation was produced by compaired direct EF Linq queries against the same query implemented\r\nas a DelegateDecompiler's `Computed` properties. This produces a Supported/Not Supported flag\r\non each command type tested. Tests are groups and ordered to try and make finding things\r\neasier.\r\n\r\nSo, if you want to use DelegateDecompiler and are not sure whether the linq command\r\nyou want to use will work then clone this project and write your own tests.\r\n(See [How to add a test](HowToAddMoreTests.md) documentation on how to do this). \r\nIf there is a problem then please fork the repository and add your own tests. \r\nThat will make it much easier to diagnose your issue.\r\n\r\n*Note: The test suite has only recently been set up and has only a handful of tests at the moment.\r\nMore will appear as we move forward.*\r\n\r\n\r\n### Group: Unit Test Group\n#### [My Unit Test1](../TestGroup01UnitTestGroup/Test01MyUnitTest1):\n- Supported\n  * Good1 (line 1)\n  * Good2 (line 2)\n\r\n#### [My Unit Test2](../TestGroup01UnitTestGroup/Test01MyUnitTest2):\n- Supported\n  * Good1 (line 1)\n  * Good2 (line 2)\n\r\n\r\n\nThe End\n";

            var expected = "\n#### [My Unit Test2](";

            Console.WriteLine($"actual.Contains(expected): {actual.Contains(expected)}");
            Console.WriteLine($"actual.IndexOf(expected): {actual.IndexOf(expected)}");
        }
    }
}
