using Meow.Helper;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// 测试32位浮点型操作
    /// </summary>
    public class FloatTest
    {
        /// <summary>
        /// 转换为32位浮点型
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
        [InlineData("12.346", 12.35, 2)]
        public void TestToFloat(object input, float result, int? digits)
        {
            Assert.Equal(result, Float.ToFloat(input, digits));
        }

        /// <summary>
        /// 转换为32位可空浮点型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, null)]
        [InlineData("1A", null, null)]
        [InlineData("0", 0f, null)]
        [InlineData("1", 1f, null)]
        [InlineData("1.2", 1.2f, null)]
        [InlineData("12.346", 12.35f, 2)]
        public void TestToFloatOrNull(object input, float? result, int? digits)
        {
            Assert.Equal(result, Float.ToFloatOrNull(input, digits));
        }
    }
}
