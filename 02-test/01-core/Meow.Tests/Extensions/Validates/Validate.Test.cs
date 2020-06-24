using System;
using Meow.Extensions.Validates;
using Meow.Tests.XUnitHelpers;
using Xunit;

namespace Meow.Tests.Extensions.Validates
{
    /// <summary>
    /// 测试是验证扩展
    /// </summary>
    public class ValidateTest
    {
        /// <summary>
        /// 检查空值，不为空则正常执行
        /// </summary>
        [Fact]
        public void TestCheckNull()
        {
            object test = new object();
            test.CheckNull("test");
        }

        /// <summary>
        /// 检查空值，值为null则抛出异常
        /// </summary>
        [Fact]
        public void TestCheckNull_Null_Throw()
        {
            AssertHelper.Throws<ArgumentNullException>(() => {
                object test = new object();
                test = null;
                test.CheckNull("test");
            }, "test");
        }
    }
}
