using System;
using System.Collections.Generic;
using Meow.Extensions.Helpers;
using Xunit;

namespace Meow.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("1", "2")]
        [InlineData("a", "b")]
        [InlineData("x", "z")]
        [InlineData("tt", "tt")]
        public void Test1(string a, string b)
        {
            var c = a;
            var d = b;
        }

        /// <summary>
        /// 测试循环引用序列化
        /// </summary>
        [Fact]
        public void Test()
        {
            var aa = new List<Test> { new Test { A = 1, B = 2, }, new Test { A = null, B = 4 } };
        }
    }

    public class Test
    {
        public int? A { get; set; }
        public int B { get; set; }

    }
}
