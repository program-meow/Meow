using System;
using System.Linq;
using Meow.Test.Sample;
using Meow.Test.XUnitHelper;
using Xunit;

namespace Meow.Test.Helper
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
        [InlineData("C", SampleEnum.C)]
        [InlineData("3", SampleEnum.C)]
        public void TestParse(string memeber, SampleEnum sample)
        {
            Assert.Equal(sample, Meow.Helper.Enum.Parse<SampleEnum>(memeber));
        }

        /// <summary>
        /// ���Ի�ȡö��ʵ�� - ����Ϊ��,�׳��쳣
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestParse_MemberIsEmpty(string member)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { Meow.Helper.Enum.Parse<SampleEnum>(member); }, "member");
        }

        /// <summary>
        /// ���Ի�ȡö��ʵ�� - �ɿ�ö��
        ///</summary>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        [InlineData("C", SampleEnum.C)]
        [InlineData("3", SampleEnum.C)]
        public void TestParse_Nullable(string memeber, SampleEnum? sample)
        {
            Assert.Equal(sample, Meow.Helper.Enum.Parse<SampleEnum?>(memeber));
        }

        /// <summary>
        /// ���Ի�ȡö�ٳ�Ա��
        ///</summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("C", "C")]
        [InlineData(3, "C")]
        [InlineData(SampleEnum.C, "C")]
        public void TestGetName(object member, string name)
        {
            Assert.Equal(name, Meow.Helper.Enum.GetName<SampleEnum>(member));
        }

        /// <summary>
        /// ���Ի�ȡö�ٳ�Ա�� - ��֤�����ö�ٲ�������ö������
        /// </summary>
        [Fact]
        public void TestGetName_Validate()
        {
            Assert.Equal(string.Empty, Meow.Helper.Enum.GetName(typeof(Sample.Sample), 3));
        }

        /// <summary>
        /// ���Ի�ȡö�ٳ�Ա�� - �ɿ�ö��
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("C", "C")]
        [InlineData(3, "C")]
        [InlineData(SampleEnum.C, "C")]
        public void TestGetName_Nullable(object member, string name)
        {
            Assert.Equal(name, Meow.Helper.Enum.GetName<SampleEnum?>(member));
        }

        /// <summary>
        /// ���Ի�ȡö�ٳ�Աֵ - ��֤
        /// </summary>
        [Fact]
        public void TestGetValue_Validate()
        {
            AssertHelper.Throws<ArgumentNullException>(() => Meow.Helper.Enum.GetValue<SampleEnum>(null), "member");
            AssertHelper.Throws<ArgumentNullException>(() => Meow.Helper.Enum.GetValue<SampleEnum>(string.Empty), "member");
            AssertHelper.Throws<ArgumentNullException>(() => Meow.Helper.Enum.GetValue<Sample.Sample>(string.Empty), "member");
        }

        /// <summary>
        /// ���Ի�ȡö�ٳ�Աֵ
        /// </summary>
        [Theory]
        [InlineData("C", 3)]
        [InlineData(3, 3)]
        [InlineData(SampleEnum.C, 3)]
        public void TestGetValue(object member, int value)
        {
            Assert.Equal(value, Meow.Helper.Enum.GetValue<SampleEnum>(member));
        }

        /// <summary>
        /// ���Ի�ȡö�ٳ�Աֵ - �ɿ�ö��
        /// </summary>
        [Theory]
        [InlineData("C", 3)]
        [InlineData(3, 3)]
        [InlineData(SampleEnum.C, 3)]
        public void TestGetValue_Nullable(object member, int value)
        {
            Assert.Equal(value, Meow.Helper.Enum.GetValue<SampleEnum?>(member));
        }

        /// <summary>
        /// ���Ի�ȡö������
        ///</summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("A", "A")]
        [InlineData("B", "B2")]
        [InlineData(2, "B2")]
        [InlineData(SampleEnum.B, "B2")]
        public void TestGetDescription(object member, string description)
        {
            Assert.Equal(description, Meow.Helper.Enum.GetDescription<SampleEnum>(member));
        }

        /// <summary>
        /// ���Ի�ȡö������ - �ɿ�ö��
        ///</summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("A", "A")]
        [InlineData("B", "B2")]
        [InlineData(2, "B2")]
        [InlineData(SampleEnum.B, "B2")]
        public void TestGetDescription_Nullable(object member, string description)
        {
            Assert.Equal(description, Meow.Helper.Enum.GetDescription<SampleEnum?>(member));
        }

        /// <summary>
        /// ���Ի�ȡ���
        /// </summary>
        [Fact]
        public void TestGetItems()
        {
            var items = Meow.Helper.Enum.GetItems<SampleEnum>();
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        /// ���Ի�ȡ���
        /// </summary>
        [Fact]
        public void TestGetItems_Type()
        {
            var items = Meow.Helper.Enum.GetItems(typeof(SampleEnum));
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        /// ���Ի�ȡ��� - �ɿ�ö��
        /// </summary>
        [Fact]
        public void TestGetItems_Nullable()
        {
            var items = Meow.Helper.Enum.GetItems<SampleEnum?>();
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        /// ���Ի�ȡ��� - �ɿ�ö��
        /// </summary>
        [Fact]
        public void TestGetItems_Nullable_Type()
        {
            var items = Meow.Helper.Enum.GetItems(typeof(SampleEnum?));
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        /// ���Ի�ȡ��� - ��֤ö������
        /// </summary>
        [Fact]
        public void TestGetItems_Validate()
        {
            AssertHelper.Throws<InvalidOperationException>(() => {
                Meow.Helper.Enum.GetItems<Sample.Sample>();
            }, "���� Meow.Test.Sample.Sample ����ö��");
        }

        /// <summary>
        /// ���Ի�ȡ���Ƽ���
        /// </summary>
        [Fact]
        public void TestGetNames()
        {
            var names = Meow.Helper.Enum.GetNames<SampleEnum>().OrderBy(t => t).ToList();
            Assert.Equal(5, names.Count);
            Assert.Equal("A", names[0]);
            Assert.Equal("D", names[3]);
            Assert.Equal("E", names[4]);
        }
    }
}
