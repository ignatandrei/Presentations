using System;

namespace CSharp8WhatsNew
{
    interface IOrnitorinc : IMamal, IReptile
    {

    }

    class Ornitorinc : IOrnitorinc
    {
        public void FeedChildren(float MilkLiters)
        {
            throw new NotImplementedException();
        }

        public IMamal[] GiveBirthToChildren()
        {
            throw new NotImplementedException();
        }

        public int NumberEggs()
        {
            throw new NotImplementedException();
        }
    }
}
