// See https://aka.ms/new-console-template for more information
using EFCoreDemo;

Console.WriteLine("Hello, World!");
using TestsContext cnt = new ();
//make breakpoint to partial void AfterOnConfiguring to see the seeding
cnt.Database.EnsureCreated();
