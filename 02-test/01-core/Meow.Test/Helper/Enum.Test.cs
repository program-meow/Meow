using System;
using System.Linq;
using Meow.Test.Sample;
using Meow.Test.XUnitHelper;
using Xunit;

namespace Meow.Test.Helper
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
        [InlineData("C", SampleEnum.C)]
        [InlineData("3", SampleEnum.C)]
        public void TestParse(string memeber, SampleEnum sample)
        {
            Assert.Equal(sample, Meow.Helper.Enum.Parse<SampleEnum>(memeber));
        }

        /// <summary>
        /// 测试获取枚举实例 - 参数为空,抛出异常
        ///</summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestParse_MemberIsEmpty(string member)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { Meow.Helper.Enum.Parse<SampleEnum>(member); }, "member");
        }

        /// <summary>
        /// 测试获取枚举实例 - 可空枚举
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
        /// 测试获取枚举成员名
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
        /// 测试获取枚举成员名 - 验证传入的枚举参数并非枚举类型
        /// </summary>
        [Fact]
        public void TestGetName_Validate()
        {
            Assert.Equal(string.Empty, Meow.Helper.Enum.GetName(typeof(Sample.Sample), 3));
        }

        /// <summary>
        /// 测试获取枚举成员名 - 可空枚举
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
        /// 测试获取枚举成员值 - 验证
        /// </summary>
        [Fact]
        public void TestGetValue_Validate()
        {
            AssertHelper.Throws<ArgumentNullException>(() => Meow.Helper.Enum.GetValue<SampleEnum>(null), "member");
            AssertHelper.Throws<ArgumentNullException>(() => Meow.Helper.Enum.GetValue<SampleEnum>(string.Empty), "member");
            AssertHelper.Throws<ArgumentNullException>(() => Meow.Helper.Enum.GetValue<Sample.Sample>(string.Empty), "member");
        }

        /// <summary>
        /// 测试获取枚举成员值
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
        /// 测试获取枚举成员值 - 可空枚举
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
        /// 测试获取枚举描述
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
        /// 测试获取枚举描述 - 可空枚举
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
        /// 测试获取项集合
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
        /// 测试获取项集合
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
        /// 测试获取项集合 - 可空枚举
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
        /// 测试获取项集合 - 可空枚举
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
        /// 测试获取项集合 - 验证枚举类型
        /// </summary>
        [Fact]
        public void TestGetItems_Validate()
        {
            AssertHelper.Throws<InvalidOperationException>(() => {
                Meow.Helper.Enum.GetItems<Sample.Sample>();
            }, "类型 Meow.Test.Sample.Sample 不是枚举");
        }

        /// <summary>
        /// 测试获取名称集合
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
