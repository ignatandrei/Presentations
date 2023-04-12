using NetCoreSimpleBusinessLogic;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTestProject
{
    // http://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx/
    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory

    public class UnitTestDemo
    {
        [Trait("All","Data")]
        [Theory(DisplayName ="Some example")]
        [InlineData(15, 5, 3)]
        [InlineData(10, 2, 5)]
        public async Task TestDivide(int x, int y, int divResult)
        {
            #region arrange
            var imp = new MyImportantClass();
            #endregion
            #region act
            var res = await imp.Divide(x, y);
            #endregion
            #region assert
            Assert.Equal(res, divResult);
            #endregion
        }
        [Trait("All", "Data")]
        [Theory(DisplayName = "Divide by 0")]
        [InlineData(15, 0)]        
        public async Task TestDivide0(int x, int y)
        {
            #region arrange
            var imp = new MyImportantClass();
            #endregion
            #region act
            
            var ex= await Record.ExceptionAsync(()
                => imp.Divide(x, y));
            #endregion
            #region assert
            Assert.NotNull(ex);
            Assert.IsType<ArgumentException>(ex);
            #endregion
        }
    }
}
