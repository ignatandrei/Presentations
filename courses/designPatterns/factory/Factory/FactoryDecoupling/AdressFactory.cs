namespace FactoryDecoupling
{
    public class AdressFactory
    {
        //later  we can add the country
        public AdressFactory(City c)
        {
            
        }
        public Address CreateAddress(string adress)
        {
            return new Address(adress);
        }
    }

    public class AdressFactoryProvider
    {
        public static AdressFactory GetAdressFactoryForCity(string name)
        {
            City c=new City(name);
            return new AdressFactory(c);
        }
    }

}