namespace FactoryDecoupling
{
    class Person
    {
        public Person(string name,string adress)
        {
            this.Name = name;
            this.Adress=new Address(adress);
        }
        public Person(string name, string adress, City city)
        {
            this.Name = name;
            this.Adress = new Address(adress,city);

        }

        public Person(string name, string adress, AdressFactory afp)
        {
            this.Name = name;
            this.Adress = afp.CreateAddress(adress);

        }
       
        public string Name { get; set; }
        public Address Adress { get; set; }
    }
    public class Address
    {
        public Address(string adress)
        {
            this.Adress = adress;
        }
        ////step 2 => person will change his constructor
        public Address(string adress, City city)
        {
            this.Adress = adress;
        }

        public string Adress { get; set; }


    }
}