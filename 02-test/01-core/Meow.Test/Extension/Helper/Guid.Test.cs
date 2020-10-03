using System;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// Guid��չ
    /// </summary>
    public class GuidTest
    {

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
    }
}
