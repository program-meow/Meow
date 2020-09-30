using System;
using System.Collections.Generic;
using Xunit;

namespace Meow.Test.Mathematics
{
    /// <summary>
    /// ���Թ�����չ
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        /// ���Խ�����������
        /// </summary>
        [Fact]
        public void TestSwap()
        {
            var a = 1;
            var b = 2;
            Meow.Mathematics.Common.Swap(ref a, ref b);
            Assert.Equal(2, a);
            Assert.Equal(1, b);

            var c = "a";
            var d = "b";
            Meow.Mathematics.Common.Swap(ref c, ref d);
            Assert.Equal("b", c);
            Assert.Equal("a", d);

            var e = new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A");
            var f = new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A");
            Meow.Mathematics.Common.Swap(ref e, ref f);
            Assert.Equal(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), e);
            Assert.Equal(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), f);

            var g = new List<Guid> { new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A") };
            var h = new List<Guid> { new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A") };
            Meow.Mathematics.Common.Swap(ref g, ref h);
            Assert.Equal(new List<Guid> { new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A") }, g);
            Assert.Equal(new List<Guid> { new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A") }, h);
        }
    }
}
