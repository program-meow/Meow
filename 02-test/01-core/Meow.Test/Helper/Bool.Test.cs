using Meow.Helper;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// 测试布尔型操作
    /// </summary>
    public class BoolTest
    {
        /// <summary>
        /// 转换为布尔型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1A", false)]
        [InlineData("0", false)]
        [InlineData("否", false)]
        [InlineData("不", false)]
        [InlineData("no", false)]
        [InlineData("No", false)]
        [InlineData("false", false)]
        [InlineData("fail", false)]
        [InlineData("1", true)]
        [InlineData("是", true)]
        [InlineData("yes", true)]
        [InlineData("true", true)]
        [InlineData("ok", true)]
        public void TestToBool(object input, bool result)
        {
            Assert.Equal(result, Bool.ToBool(input));
        }

        /// <summary>
        /// 转换为可空布尔型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        [InlineData("0", false)]
        [InlineData("否", false)]
        [InlineData("不", false)]
        [InlineData("no", false)]
        [InlineData("No", false)]
        [InlineData("false", false)]
        [InlineData("fail", false)]
        [InlineData("1", true)]
        [InlineData("是", true)]
        [InlineData("yes", true)]
        [InlineData("true", true)]
        [InlineData("ok", true)]
        public void TestToBoolOrNull(object input, bool? result)
        {
            Assert.Equal(result, Bool.ToBoolOrNull(input));
        }
    }
}
