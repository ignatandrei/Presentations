using Microsoft.EntityFrameworkCore;
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
            //dictionary in EF
            var obj = (cont.DepartmentWithNumber.First());
            Console.WriteLine(obj["IDDepartment"]);

            //split query
            var dataQueryable = cont.Departments.Include(it => it.Employees);
            var full = dataQueryable.ToQueryString();
            var arr = dataQueryable.ToArray();


            dataQueryable = cont.Departments.AsSplitQuery().Include(it => it.Employees);
            var split = dataQueryable.ToQueryString();
            arr =dataQueryable .ToArray();

        }
    }
}
