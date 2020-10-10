using System.Collections.Generic;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// 测试类型操作
    /// </summary>
    public class TypeTest
    {
        /// <summary>
        /// 获取类型高精度枚举
        /// </summary>
        [Fact]
        public void TestGetTypeHighPrecisionEnum()
        {
            bool? a1 = true;
            byte a2 = 1;
            char a3 = '1';
            decimal a4 = 1;
            double a5 = 1;
            float a6 = 1;
            int a7 = 1;
            long a8 = 1;
            sbyte a9 = 1;
            short a10 = 1;
            uint a11 = 1;
            ulong a12 = 1;
            ushort a13 = 1;
            Database a14 = Database.MySql;
            System.DateTime? a15 = System.DateTime.Now;
            string a16 = "哈";
            byte[] a17 = new byte[] { 1, 2 };
            Item a18 = new Item(null, null);
            List<Item> a19 = new List<Item> { new Item(null, null), new Item(null, null) };
            int[] a20 = new int[] { 1, 2 };
            System.DateTime? a21 = System.DateTime.MaxValue;
            System.DateTime? a22 = null;
            Dictionary<string, string> a23 = new Dictionary<string, string>();
            Assert.Equal(TypeHighPrecision.Bool, a1.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Byte, a2.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Char, a3.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Decimal, a4.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Double, a5.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Float, a6.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Int, a7.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Long, a8.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Sbyte, a9.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Short, a10.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Uint, a11.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Ulong, a12.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Ushort, a13.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Enum, a14.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.DateTime, a15.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.String, a16.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Array, a17.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Object, a18.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.List, a19.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Array, a20.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.DateTime, a21.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Null, a22.GetTypeHighPrecisionEnum());
            Assert.Equal(TypeHighPrecision.Dictionary, a23.GetTypeHighPrecisionEnum());
        }
    }
}
