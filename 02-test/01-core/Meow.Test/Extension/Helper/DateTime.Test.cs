using System;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// DateTime扩展
    /// </summary>
    public class DateTimeTest
    {
        /// <summary>
        /// 测试是否空值 - 集合
        /// </summary>
        [Fact]
        public void TestIsEmpty_List()
        {
            Assert.True(DateTime.MinValue.IsEmpty());
            Assert.True(DateTime.MaxValue.IsEmpty());
            DateTime? dateTime = null;
            Assert.True(dateTime.IsEmpty());
            dateTime = DateTime.Now;
            Assert.False(dateTime.IsEmpty());
        }
    }
}
