using System;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// DateTime��չ
    /// </summary>
    public class DateTimeTest
    {
        /// <summary>
        /// �����Ƿ��ֵ - ����
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
