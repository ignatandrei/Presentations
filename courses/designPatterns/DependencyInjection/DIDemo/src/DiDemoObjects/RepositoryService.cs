using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiDemoObjects
{
    public class RepositoryService
    {
        public virtual List<Person> RetrievePersons()
        {
            //retrieve from some sort of storage
            return new List<Person>() { new Person { FirstName = "Andrei" } };

        }
    }
    public class PersonList : List<Person>
    {
        RepositoryService rs;
        public PersonList(RepositoryService rs)
        {
            this.rs = rs;

        }
        public void Retrieve()
        {
            this.AddRange(rs.RetrievePersons());
        }

    }
    public class Person
    {
        public string FirstName { get; set; }
    }
}