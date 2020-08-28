using System;
using Meow.Extensions.Helpers;
using Meow.Tests.Samples;
using Meow.Tests.XUnitHelpers;
using Xunit;

namespace Meow.Tests.Extensions.Helpers
{
    /// <summary>
    /// ����ö�ٲ���
    /// </summary>
    public class EnumTest
    {
        /// <summary>
        /// ����ת��ö��
        ///</summary>
        [Theory]
        [InlineData("C", EnumSample.C)]
        [InlineData("3", EnumSample.C)]
        public void TestToEnum_String(string input, EnumSample sample)
        {
            Assert.Equal(sample, input.ToEnum<EnumSample>());
        }

        /// <summary>
        /// ����ת��ö�� - �ɿ�ö��
        ///</summary>
        [Theory]
        [InlineData(1, EnumSample.A)]
        [InlineData(3, EnumSample.C)]
        public void TestToEnum_Int(int input, EnumSample sample)
        {
            Assert.Equal(sample, input.ToEnum<EnumSample>());
        }

        /// <summary>
        /// ����ת��ö�� - �ɿ�ö��
        ///</summary>
        [Theory]
        [InlineData(1, EnumSample.A)]
        [InlineData(3, EnumSample.C)]
        public void TestToEnum_IntOrNull(int? input, EnumSample? sample)
        {
            Assert.Equal(sample, input.ToEnum<EnumSample>());
        }

        /// <summary>
        /// ����ת��ö�� - ����Ϊ��,�׳��쳣
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestToEnum_MemberIsEmpty(string input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<EnumSample>(); }, "member");
        }

        /// <summary>
        /// ����ת��ö�� - ����Ϊ��,�׳��쳣
        ///</summary>
        [Theory]
        [InlineData(null)]
        public void TestToEnum_IntOrNull_MemberIsEmpty(int? input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<EnumSample>(); }, "member");
        }
    }
}
