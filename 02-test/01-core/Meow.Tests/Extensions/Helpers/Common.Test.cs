using Meow.Extensions.Helpers;
using Xunit;

namespace Meow.Tests.Extensions.Helpers
{
    /// <summary>
    /// 测试公共操作
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        /// 测试安全获取值
        /// </summary>
        [Fact]
        public void TestSafeValue()
        {
            int? number = null;
            Assert.Equal(0, number.SafeValue());
            number = 1;
            Assert.Equal(1, number.SafeValue());
        }
    }
}
