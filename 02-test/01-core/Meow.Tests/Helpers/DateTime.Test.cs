using System;
using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// ����ʱ���Ͳ���
    /// </summary>
    public class DateTimeTest
    {
        /// <summary>
        /// ת��Ϊ���ڣ���֤
        /// </summary>
        [Fact]
        public void TestToDate_Validate()
        {
            Assert.Equal(DateTime.MinValue, Meow.Helpers.DateTime.ToDate(null));
            Assert.Equal(DateTime.MinValue, Meow.Helpers.DateTime.ToDate(""));
            Assert.Equal(DateTime.MinValue, Meow.Helpers.DateTime.ToDate("1A"));
        }

        /// <summary>
        /// ת��Ϊ����
        /// </summary>
        [Fact]
        public void TestToDate()
        {
            Assert.Equal(new DateTime(2000, 1, 1), Meow.Helpers.DateTime.ToDate("2000-1-1"));
        }

        /// <summary>
        /// ת��Ϊ�ɿ����ڣ���֤
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        public void TestToDateOrNull_Validate(object input, DateTime? result)
        {
            Assert.Equal(result, Meow.Helpers.DateTime.ToDateOrNull(input));
        }

        /// <summary>
        /// ת��Ϊ�ɿ�����
        /// </summary>
        [Fact]
        public void TestToDateOrNull()
        {
            Assert.Equal(new DateTime(2000, 1, 1), Meow.Helpers.DateTime.ToDateOrNull("2000-1-1"));
        }
    }
}
