using System;
using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// 测试时间型操作
    /// </summary>
    public class DateTimeTest
    {
        /// <summary>
        /// 转换为日期，验证
        /// </summary>
        [Fact]
        public void TestToDate_Validate()
        {
            Assert.Equal(DateTime.MinValue, Meow.Helpers.DateTime.ToDate(null));
            Assert.Equal(DateTime.MinValue, Meow.Helpers.DateTime.ToDate(""));
            Assert.Equal(DateTime.MinValue, Meow.Helpers.DateTime.ToDate("1A"));
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        [Fact]
        public void TestToDate()
        {
            Assert.Equal(new DateTime(2000, 1, 1), Meow.Helpers.DateTime.ToDate("2000-1-1"));
        }

        /// <summary>
        /// 转换为可空日期，验证
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        public void TestToDateOrNull_Validate(object input, DateTime? result)
        {
            Assert.Equal(result, Meow.Helpers.DateTime.ToDateOrNull(input));
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        [Fact]
        public void TestToDateOrNull()
        {
            Assert.Equal(new DateTime(2000, 1, 1), Meow.Helpers.DateTime.ToDateOrNull("2000-1-1"));
        }
    }
}
