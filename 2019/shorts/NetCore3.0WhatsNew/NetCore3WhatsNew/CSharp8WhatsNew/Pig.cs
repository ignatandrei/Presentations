using System;

namespace CSharp8WhatsNew
{
    class Monkey : IMamal
    {
        public void FeedChildren(float MilkLiters)
        {
            throw new NotImplementedException();
        }

        public IMamal[] GiveBirthToChildren()
        {
            throw new NotImplementedException();
        }
    }
    class HumanChild : IAnimal //just for the sake of the demo
    {

    }
    class Human: Monkey //just for the sake of the demo
    {

    }
    class Pig : IMamal
    {
        public void FeedChildren(float MilkLiters)
        {
            Console.WriteLine("just feed");
        }

        public IMamal[] GiveBirthToChildren()
        {
            return null;
        }
    }
}
