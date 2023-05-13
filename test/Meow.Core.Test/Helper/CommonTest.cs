using Xunit;

namespace Meow.Core.Test.Helper
{
    /// <summary>
    /// 公共操作 - 测试
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, "")]
        [InlineData(" ", "")]
        [InlineData(" 1A ", "1A")]
        [InlineData(0, "0")]
        [InlineData("   1778019.78", "1778019.78")]
        public void SafeStringTest(object input, string result)
        {
            Assert.Equal(result, Meow.Helper.Common.SafeString(input));
        }

    }
}
