using System;
using System.Threading.Tasks;
namespace NetCoreSimpleBusinessLogic
{
    public class MyImportantClass
    {
        public async Task<int> Divide(int x, int y)
        {
			//change this to 0.5 or 1
            await Task.Delay(1); 
            //security
            if (y == 0)
                throw new ArgumentException("cannot divide by 0");
            return x / y;
        }


    }
}
