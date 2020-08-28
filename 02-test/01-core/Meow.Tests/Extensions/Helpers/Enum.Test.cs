using System;
using Meow.Extensions.Helpers;
using Meow.Tests.Samples;
using Meow.Tests.XUnitHelpers;
using Xunit;

namespace Meow.Tests.Extensions.Helpers
{
    /// <summary>
    /// 测试枚举操作
    /// </summary>
    public class EnumTest
    {
        /// <summary>
        /// 测试转换枚举
        ///</summary>
        [Theory]
        [InlineData("C", EnumSample.C)]
        [InlineData("3", EnumSample.C)]
        public void TestToEnum_String(string input, EnumSample sample)
        {
            Assert.Equal(sample, input.ToEnum<EnumSample>());
        }

        /// <summary>
        /// 测试转换枚举 - 可空枚举
        ///</summary>
        [Theory]
        [InlineData(1, EnumSample.A)]
        [InlineData(3, EnumSample.C)]
        public void TestToEnum_Int(int input, EnumSample sample)
        {
            Assert.Equal(sample, input.ToEnum<EnumSample>());
        }

        /// <summary>
        /// 测试转换枚举 - 可空枚举
        ///</summary>
        [Theory]
        [InlineData(1, EnumSample.A)]
        [InlineData(3, EnumSample.C)]
        public void TestToEnum_IntOrNull(int? input, EnumSample? sample)
        {
            Assert.Equal(sample, input.ToEnum<EnumSample>());
        }

        /// <summary>
        /// 测试转换枚举 - 参数为空,抛出异常
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestToEnum_MemberIsEmpty(string input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<EnumSample>(); }, "member");
        }

        /// <summary>
        /// 测试转换枚举 - 参数为空,抛出异常
        ///</summary>
        [Theory]
        [InlineData(null)]
        public void TestToEnum_IntOrNull_MemberIsEmpty(int? input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<EnumSample>(); }, "member");
        }
    }
}
