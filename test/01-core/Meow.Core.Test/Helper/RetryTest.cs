using Meow.Common.Test.Function;
using Xunit;

namespace Meow.Core.Test.Helper
{
    /// <summary>
    /// 重试操作 - 测试
    /// </summary>
    public class RetryTest
    {
        /// <summary>
        /// 试着调用
        /// </summary>
        [Fact]
        public void TryInvokeTest()
        {
            Assert.False(Meow.Helper.Retry.TryInvoke(FunctionError.Void).Data);
            Assert.False(Meow.Helper.Retry.TryInvoke(FunctionError.ReturnBool).Data);
        }
    }
}
