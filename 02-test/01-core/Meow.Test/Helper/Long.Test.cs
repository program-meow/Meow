using Meow.Helper;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// 测试64位整型操作
    /// </summary>
    public class LongTest
    {
        /// <summary>
        /// 转换为64位整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("1A", 0)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("1778019.7801684", 1778020)]
        [InlineData("177801978016841234", 177801978016841234)]
        public void TestToLong(object input, long result)
        {
            Assert.Equal(result, Long.ToLong(input));
        }

        /// <summary>
        /// 转换为64位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        [InlineData("0", 0L)]
        [InlineData("1", 1L)]
        [InlineData("1778019.7801684", 1778020L)]
        [InlineData("177801978016841234", 177801978016841234L)]
        public void TestToLongOrNull(object input, long? result)
        {
            Assert.Equal(result, Long.ToLongOrNull(input));
        }
    }
}
