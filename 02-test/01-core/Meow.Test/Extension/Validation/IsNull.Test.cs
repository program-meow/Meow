using System;
using System.Collections.Generic;
using Meow.Extension.Validation;
using Xunit;

namespace Meow.Test.Extension.Validation
{
    /// <summary>
    /// �����Ƿ�ΪNull��չ
    /// </summary>
    public class IsNullTest
    {
        /// <summary>
        /// �����Ƿ�Null - �ַ���
        /// </summary>
        [Theory]
        [InlineData(null, true)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("a", false)]
        public void TestIsNull_String(string value, bool result)
        {
            Assert.Equal(value.IsNull(), result);
        }

        /// <summary>
        /// �����Ƿ�Null - Guid
        /// </summary>
        [Fact]
        public void TestIsNull_Guid()
        {
            Assert.False(Guid.Empty.IsNull());
            Assert.False(Guid.NewGuid().IsNull());
        }

        /// <summary>
        /// �����Ƿ�Null - �ɿ�Guid
        /// </summary>
        [Fact]
        public void TestIsNull_Guid_Nullable()
        {
            Guid? value = null;
            Assert.True(value.IsNull());
            value = Guid.Empty;
            Assert.False(value.IsNull());
            value = Guid.NewGuid();
            Assert.False(value.IsNull());
        }

        /// <summary>
        /// �����Ƿ�Null - ����
        /// </summary>
        [Fact]
        public void TestIsNull_List()
        {
            List<int> list = null;
            Assert.True(list.IsNull());
            list = new List<int>();
            Assert.False(list.IsNull());
            list.Add(1);
            Assert.False(list.IsNull());
        }
    }
}
