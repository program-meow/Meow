using System;
using Meow.Extension.Helper;
using Meow.Test.Sample;
using Meow.Test.XUnitHelper;
using Xunit;

namespace Meow.Test.Extension.Helper
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
        [InlineData("C", SampleEnum.C)]
        [InlineData("3", SampleEnum.C)]
        public void TestToEnum_String(string input, SampleEnum sample)
        {
            Assert.Equal(sample, input.ToEnum<SampleEnum>());
        }

        /// <summary>
        /// 测试转换枚举 - 可空枚举
        ///</summary>
        [Theory]
        [InlineData(1, SampleEnum.A)]
        [InlineData(3, SampleEnum.C)]
        public void TestToEnum_Int(int input, SampleEnum sample)
        {
            Assert.Equal(sample, input.ToEnum<SampleEnum>());
        }

        /// <summary>
        /// 测试转换枚举 - 可空枚举
        ///</summary>
        [Theory]
        [InlineData(1, SampleEnum.A)]
        [InlineData(3, SampleEnum.C)]
        public void TestToEnum_IntOrNull(int? input, SampleEnum? sample)
        {
            Assert.Equal(sample, input.ToEnum<SampleEnum>());
        }

        /// <summary>
        /// 测试转换枚举 - 参数为空,抛出异常
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestToEnum_MemberIsEmpty(string input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<SampleEnum>(); }, "member");
        }

        /// <summary>
        /// 测试转换枚举 - 参数为空,抛出异常
        ///</summary>
        [Theory]
        [InlineData(null)]
        public void TestToEnum_IntOrNull_MemberIsEmpty(int? input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<SampleEnum>(); }, "member");
        }
    }
}
