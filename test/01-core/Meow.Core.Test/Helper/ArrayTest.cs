using Xunit;

namespace Meow.Core.Test.Helper
{
    /// <summary>
    /// 数组操作 - 测试
    /// </summary>
    public class ArrayTest
    {
        /// <summary>
        /// 空数组
        /// </summary>
        [Fact]
        public void EmptyTest()
        {
            var stringArray = Meow.Helper.Array.Empty<string>();
            var objectArray = Meow.Helper.Array.Empty<object>();
            Assert.Empty(stringArray);
            Assert.Empty(objectArray);
        }
    }
}
