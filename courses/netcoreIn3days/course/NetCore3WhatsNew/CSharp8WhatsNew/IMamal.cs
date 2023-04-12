using System.Collections.Generic;
using System.Text;

namespace CSharp8WhatsNew
{
    interface IMamal:IAnimal
    {
        //when deriving from IAnimal , should be new...
        string baseType ()=> "Mamal";
        void FeedChildren(float MilkLiters);
        //not so correct here
        IMamal[] GiveBirthToChildren();

    }
}
