using System;
using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// 测试Guid操作
    /// </summary>
    public class GuidTest
    {
        /// <summary>
        /// 转换为Guid，验证
        /// </summary>
        [Fact]
        public void TestToGuid_Validate()
        {
            Assert.Equal(Guid.Empty, Meow.Helpers.Guid.ToGuid(null));
            Assert.Equal(Guid.Empty, Meow.Helpers.Guid.ToGuid(""));
            Assert.Equal(Guid.Empty, Meow.Helpers.Guid.ToGuid("1A"));
        }

        /// <summary>
        /// 转换为Guid
        /// </summary>
        [Fact]
        public void TestToGuid()
        {
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), Meow.Helpers.Guid.ToGuid("B9EB56E9-B720-40B4-9425-00483D311DDC"));
        }

        /// <summary>
        /// 转换为可空Guid，验证
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        public void TestToGuidOrNull_Validate(object input, Guid? result)
        {
            Assert.Equal(result, Meow.Helpers.Guid.ToGuidOrNull(input));
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        [Fact]
        public void TestToGuidOrNull()
        {
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), Meow.Helpers.Guid.ToGuidOrNull("B9EB56E9-B720-40B4-9425-00483D311DDC"));
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        [Fact]
        public void TestToGuidList()
        {
            Assert.Empty(Meow.Helpers.Guid.ToGuidList(null));
            Assert.Empty(Meow.Helpers.Guid.ToGuidList(""));

            const string guid = "83B0233C-A24F-49FD-8083-1337209EBC9A";
            Assert.Single(Meow.Helpers.Guid.ToGuidList(guid));
            Assert.Equal(new Guid(guid), Meow.Helpers.Guid.ToGuidList(guid)[0]);

            const string guid2 = "83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A";
            Assert.Equal(2, Meow.Helpers.Guid.ToGuidList(guid2).Count);
            Assert.Equal(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), Meow.Helpers.Guid.ToGuidList(guid2)[0]);
            Assert.Equal(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), Meow.Helpers.Guid.ToGuidList(guid2)[1]);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        [Fact]
        public void TestToGuidList_2()
        {
            const string guid = "83B0233C-A24F-49FD-8083-1337209EBC9A,,EAB523C6-2FE7-47BE-89D5-C6D440C3033A,";
            Assert.Equal(2, Meow.Helpers.Guid.ToGuidList(guid).Count);
            Assert.Equal(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), Meow.Helpers.Guid.ToGuidList(guid)[0]);
            Assert.Equal(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), Meow.Helpers.Guid.ToGuidList(guid)[1]);
        }
    }
}
