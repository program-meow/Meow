using System;
using Meow.Tests.Samples;
using Meow.Tests.XUnitHelpers;
using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// 测试枚举操作
    /// </summary>
    public class EnumTest
    {
        /// <summary>
        /// 测试获取枚举实例
        ///</summary>
        [Theory]
        [InlineData("C", EnumSample.C)]
        [InlineData("3", EnumSample.C)]
        [InlineData(1, EnumSample.A)]
        public void TestParse(object input, EnumSample sample)
        {
            Assert.Equal(sample, Meow.Helpers.Enum.Parse<EnumSample>(input));
        }

        /// <summary>
        /// 测试获取枚举实例 - 参数为空,抛出异常
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestParse_MemberIsEmpty(string input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { Meow.Helpers.Enum.Parse<EnumSample>(input); }, "member");
        }

        /// <summary>
        /// 测试获取枚举实例 - 可空枚举
        ///</summary>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        [InlineData("C", EnumSample.C)]
        [InlineData("3", EnumSample.C)]
        [InlineData(1, EnumSample.A)]
        public void TestParse_Nullable(object input, EnumSample? sample)
        {
            Assert.Equal(sample, Meow.Helpers.Enum.Parse<EnumSample?>(input));
        }
    }
}
