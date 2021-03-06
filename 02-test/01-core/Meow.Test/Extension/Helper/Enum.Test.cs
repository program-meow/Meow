using System;
using Meow.Common.Test.Sample;
using Meow.Common.Test.XUnitHelper;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
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
        [InlineData("C", SampleEnum.C)]
        [InlineData("3", SampleEnum.C)]
        public void TestToEnum_String(string input, SampleEnum sample)
        {
            Assert.Equal(sample, input.ToEnum<SampleEnum>());
        }

        /// <summary>
        /// ����ת��ö�� - �ɿ�ö��
        ///</summary>
        [Theory]
        [InlineData(1, SampleEnum.A)]
        [InlineData(3, SampleEnum.C)]
        public void TestToEnum_Int(int input, SampleEnum sample)
        {
            Assert.Equal(sample, input.ToEnum<SampleEnum>());
        }

        /// <summary>
        /// ����ת��ö�� - �ɿ�ö��
        ///</summary>
        [Theory]
        [InlineData(1, SampleEnum.A)]
        [InlineData(3, SampleEnum.C)]
        public void TestToEnum_IntOrNull(int? input, SampleEnum? sample)
        {
            Assert.Equal(sample, input.ToEnum<SampleEnum>());
        }

        /// <summary>
        /// ����ת��ö�� - ����Ϊ��,�׳��쳣
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestToEnum_MemberIsEmpty(string input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<SampleEnum>(); }, "member");
        }

        /// <summary>
        /// ����ת��ö�� - ����Ϊ��,�׳��쳣
        ///</summary>
        [Theory]
        [InlineData(null)]
        public void TestToEnum_IntOrNull_MemberIsEmpty(int? input)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { input.ToEnum<SampleEnum>(); }, "member");
        }

        /// <summary>
        /// ���Ի�ȡö��ֵ
        /// </summary>
        [Fact]
        public void TestValue()
        {
            Assert.Equal(2, SampleEnum.B.Value());
            Assert.Equal("2", SampleEnum.B.Value<string>());
        }

        /// <summary>
        /// ���Ի�ȡö������
        /// </summary>
        [Fact]
        public void TestDescription()
        {
            Assert.Equal("B2", SampleEnum.B.Description());
        }
    }
}
