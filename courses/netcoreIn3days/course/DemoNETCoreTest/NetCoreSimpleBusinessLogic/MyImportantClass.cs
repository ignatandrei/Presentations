using System;
using System.Threading.Tasks;
namespace NetCoreSimpleBusinessLogic
{
    public class MyImportantClass
    {
        public async Task<int> Divide(int x, int y)
        {
            await Task.Delay(5 * 1000);
            //security
            if (y == 0)
                throw new ArgumentException("cannot divide by 0");
            return x / y;
        }


    }
}
