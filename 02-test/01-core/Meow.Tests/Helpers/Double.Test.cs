using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// 测试浮点型操作
    /// </summary>
    public class DoubleTest
    {
        /// <summary>
        /// 转换为64位浮点型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, 0, null)]
        [InlineData("", 0, null)]
        [InlineData("1A", 0, null)]
        [InlineData("0", 0, null)]
        [InlineData("1", 1, null)]
        [InlineData("1.2", 1.2, null)]
        [InlineData("12.235", 12.24, 2)]
        [InlineData("12.345", 12.34, 2)]
        [InlineData("12.3451", 12.35, 2)]
        [InlineData("12.346", 12.35, 2)]
        public void TestToDouble(object input, double result, int? digits)
        {
            Assert.Equal(result, Meow.Helpers.Double.ToDouble(input, digits));
        }

        /// <summary>
        /// 转换为64位可空浮点型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, null)]
        [InlineData("1A", null, null)]
        [InlineData("0", 0d, null)]
        [InlineData("1", 1d, null)]
        [InlineData("1.2", 1.2, null)]
        [InlineData("12.355", 12.36, 2)]
        public void TestToDoubleOrNull(object input, double? result, int? digits)
        {
            Assert.Equal(result, Meow.Helpers.Double.ToDoubleOrNull(input, digits));
        }
    }
}
