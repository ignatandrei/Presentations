using System;
using System.Linq;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var cont = new testsContext();
            //cont.Database.EnsureCreated();
            Console.WriteLine("Nr deps:"+ cont.Departments.Count());
            var obj = (cont.DepartmentWithNumber.First());
            Console.WriteLine(obj["IDDepartment"]);
        }
    }
}
