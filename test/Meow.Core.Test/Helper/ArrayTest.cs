using Meow.Enum;
using Meow.Extension;
using Xunit;

namespace Meow.Core.Test.Helper {
    /// <summary>
    /// 数组操作 - 测试
    /// </summary>
    public class ArrayTest {
        /// <summary>
        /// 空数组
        /// </summary>
        [Fact]
        public void EmptyTest() {
            string[] stringArray = Meow.Helper.Array.Empty<string>();
            object[] objectArray = Meow.Helper.Array.Empty<object>();
            Assert.Empty( stringArray );
            Assert.Empty( objectArray );
        }

        /// <summary>
        /// 空数组
        /// </summary>
        [Fact]
        public void Test() {
            decimal bb = -2000200020002010000.223M;
            string aa = bb.ToMoneyFromNum( MoneyEnum.USD );
        }
    }
}
