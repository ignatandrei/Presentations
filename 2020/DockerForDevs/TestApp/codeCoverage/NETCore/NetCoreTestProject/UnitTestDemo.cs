using NetCoreSimpleBusinessLogic;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTestProject
{
    public class UnitTestDemo
    {
        [Trait("All","Data")]
        [Theory(DisplayName ="Some example")]
        [InlineData(15, 5, 3)]
        [InlineData(10, 2, 5)]
        public async Task TestDivide(int x, int y, int result)
        {
            #region arrange
            var imp = new MyImportantClass();
            #endregion
            #region act
            var res = await imp.Divide(x, y);
            #endregion
            #region assert
            Assert.Equal(res, result);
            #endregion
        }
        //[Trait("All", "Data")]
        //[Theory(DisplayName = "Divide by 0")]
        //[InlineData(15, 0)]
        //public async Task TestDivide0(int x, int y)
        //{
        //    #region arrange
        //    var imp = new MyImportantClass();
        //    #endregion
        //    #region act

        //    var ex = await Record.ExceptionAsync(() => imp.Divide(x, y));
        //    #endregion
        //    #region assert
        //    Assert.NotNull(ex);
        //    Assert.IsType<ArgumentException>(ex);
        //    #endregion
        //}
    }
}
