using System;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// Guid¿©’π
    /// </summary>
    public class GuidTest
    {

        /// <summary>
        /// ≤‚ ‘ «∑Òø’÷µ - Guid
        /// </summary>
        [Fact]
        public void TestIsEmpty_Guid()
        {
            Assert.True(Guid.Empty.IsEmpty());
            Assert.False(Guid.NewGuid().IsEmpty());
        }

        /// <summary>
        /// ≤‚ ‘ «∑Òø’÷µ - ø…ø’Guid
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
