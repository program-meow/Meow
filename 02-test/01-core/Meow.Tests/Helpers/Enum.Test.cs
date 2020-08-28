using System;
using Meow.Tests.Samples;
using Meow.Tests.XUnitHelpers;
using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// ����ö�ٲ���
    /// </summary>
    public class EnumTest
    {
        /// <summary>
        /// ���Ի�ȡö��ʵ��
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
        /// ���Ի�ȡö��ʵ�� - ����Ϊ��,�׳��쳣
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestParse_MemberIsEmpty(string input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { Meow.Helpers.Enum.Parse<EnumSample>(input); }, "member");
        }

        /// <summary>
        /// ���Ի�ȡö��ʵ�� - �ɿ�ö��
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
