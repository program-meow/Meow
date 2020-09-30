using System;
using System.Collections.Generic;
using Meow.Extension.Validation;
using Xunit;

namespace Meow.Test.Extension.Validation
{
    /// <summary>
    /// �����Ƿ�Ϊ����չ
    /// </summary>
    public class IsEmptyTest
    {
        /// <summary>
        /// �����Ƿ��ֵ - �ַ���
        /// </summary>
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("a", false)]
        public void TestIsEmpty_String(string value, bool result)
        {
            Assert.Equal(value.IsEmpty(), result);
        }

        /// <summary>
        /// �����Ƿ��ֵ - Guid
        /// </summary>
        [Fact]
        public void TestIsEmpty_Guid()
        {
            Assert.True(Guid.Empty.IsEmpty());
            Assert.False(Guid.NewGuid().IsEmpty());
        }

        /// <summary>
        /// �����Ƿ��ֵ - �ɿ�Guid
        /// </summary>
        [Fact]
        public void TestIsEmpty_Guid_Nullable()
        {
            Guid? value = null;
            Assert.True(value.IsEmpty());
            value = Guid.Empty;
            Assert.True(value.IsEmpty());
            value = Guid.NewGuid();
            Assert.False(value.IsEmpty());
        }

        /// <summary>
        /// �����Ƿ��ֵ - ����
        /// </summary>
        [Fact]
        public void TestIsEmpty_List()
        {
            List<int> list = null;
            Assert.True(list.IsEmpty());
            list = new List<int>();
            Assert.True(list.IsEmpty());
            list.Add(1);
            Assert.False(list.IsEmpty());
        }
    }
}
