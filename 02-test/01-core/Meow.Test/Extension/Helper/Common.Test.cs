using System;
using Meow.Extension.Helper;
using Meow.Test.Sample;
using Xunit;

namespace Meow.Test.Extension.Helper
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

        /// <summary>
        /// 安全转换为字符串
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, "")]
        [InlineData("  ", "")]
        [InlineData(1, "1")]
        [InlineData(" 1 ", "1")]
        public void TestSafeString(object input, string result)
        {
            Assert.Equal(result, input.SafeString());
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        [Fact]
        public void TestTo()
        {
            Assert.Null("".To<string>());
            Assert.Equal("1A", "1A".To<string>());
            int? int1 = null;
            Assert.Equal(0, int1.To<int>());
            Assert.Equal(0, "".To<int>());
            Assert.Equal(0, "2A".To<int>());
            Assert.Equal(1, "1".To<int>());
            Assert.Null(int1.To<int?>());
            Assert.Null("".To<int?>());
            Assert.Null("3A".To<int?>());
            Assert.Equal(Guid.Empty, "".To<Guid>());
            Assert.Equal(Guid.Empty, "4A".To<Guid>());
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), "B9EB56E9-B720-40B4-9425-00483D311DDC".To<Guid>());
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), "B9EB56E9-B720-40B4-9425-00483D311DDC".To<Guid?>());
            Assert.Equal(12.5, "12.5".To<double>());
            Assert.Equal(12.5, "12.5".To<double?>());
            Assert.Equal(12.5M, "12.5".To<decimal>());
            Assert.True("true".To<bool>());
            Assert.Equal(new DateTime(2000, 1, 1), "2000-1-1".To<DateTime>());
            Assert.Equal(new DateTime(2000, 1, 1), "2000-1-1".To<DateTime?>());
            var guid = Guid.NewGuid();
            Assert.Equal(guid.ToString(), guid.To<string>());
            Assert.Equal(SampleEnum.C, "c".To<SampleEnum>());
        }
    }
}
