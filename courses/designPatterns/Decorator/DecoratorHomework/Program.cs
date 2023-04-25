using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://media.netflix.com/en/only-on-netflix#/new?page=5
/// </summary>
namespace DecoratorHomework
{
    public abstract class NetflixItem
    {
        public NetflixItem(string title)
        {
            Title = title;
        }
        public string Category { get; protected set; }
        public string Title { get; set; }
        public virtual string Description()
        {
            return $"{Category} {Title} ";
        }

    }
    public class Series:NetflixItem
    {
        public Series(string title):base(title)
        {
            Category = "Series";
        }
    }
    public class Documentary:NetflixItem
    {
        public Documentary(string title) : base(title)
        {
            Category = "Documentary";
        }
    }
    public class Film : NetflixItem
    {
        public Film(string title) : base(title)
        {
            Category = "Film";
        }
    }
    public class OriginalStory : NetflixItem
    {
        public OriginalStory(string title) : base(title)
        {
            Category = "OriginalStory";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("make netflix items viewable for every derived class");
            Console.WriteLine("when viewing, record name of the user");
            Console.WriteLine("when viewing, also record time start and time end");
            Console.WriteLine("also, on display, tell if is viewed now or not");
            Film f = new Film("The babysitter");
            Console.WriteLine(f.Description());
            Series s = new Series("Matrix");
            Console.WriteLine(s.Description());
            //TODO: make film and series viewable

        }
    }
}
