using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// 测试32位整型操作
    /// </summary>
    public class InitTest
    {
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("1A", 0)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("1778019.78", 1778020)]
        [InlineData("1778019.7801684", 1778020)]
        public void TestToInt(object input, int result)
        {
            Assert.Equal(result, Meow.Helpers.Init.ToInt(input));
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("1778019.78", 1778020)]
        [InlineData("1778019.7801684", 1778020)]
        public void TestToIntOrNull(object input, int? result)
        {
            Assert.Equal(result, Meow.Helpers.Init.ToIntOrNull(input));
        }
    }
}
