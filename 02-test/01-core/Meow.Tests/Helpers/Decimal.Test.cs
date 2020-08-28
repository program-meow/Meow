using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// 测试128位浮点型操作
    /// </summary>
    public class DecimalTest
    {
        /// <summary>
        /// 转换为128位浮点型
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
        public void TestToDecimal(object input, decimal result, int? digits)
        {
            Assert.Equal(result, Meow.Helpers.Decimal.ToDecimal(input, digits));
        }

        /// <summary>
        /// 转换为128位可空浮点型，验证
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, null)]
        [InlineData("1A", null, null)]
        [InlineData("1A", null, 2)]
        public void TestToDecimalOrNull_Validate(object input, decimal? result, int? digits)
        {
            Assert.Equal(result, Meow.Helpers.Decimal.ToDecimalOrNull(input, digits));
        }

        /// <summary>
        /// 转换为128位可空浮点型，输入值为"0"
        /// </summary>
        [Fact]
        public void TestToDecimalOrNull()
        {
            Assert.Equal(0M, Meow.Helpers.Decimal.ToDecimalOrNull("0"));
            Assert.Equal(1.2M, Meow.Helpers.Decimal.ToDecimalOrNull("1.2"));
            Assert.Equal(23.46M, Meow.Helpers.Decimal.ToDecimalOrNull("23.456", 2));
        }
    }
}
