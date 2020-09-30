using System;
using System.Collections.Generic;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// 测试集合操作
    /// </summary>
    public class IEnumerableTest
    {
        /// <summary>
        /// 测试将集合连接为带分隔符的字符串
        /// </summary>
        [Fact]
        public void TestJoin()
        {
            Assert.Equal("1,2,3", Meow.Helper.IEnumerable.Join(new List<int> { 1, 2, 3 }));
            Assert.Equal("'1','2','3'", Meow.Helper.IEnumerable.Join(new List<int> { 1, 2, 3 }, "'"));
            Assert.Equal("123", Meow.Helper.IEnumerable.Join(new List<int> { 1, 2, 3 }, "", ""));
            Assert.Equal("\"1\",\"2\",\"3\"", Meow.Helper.IEnumerable.Join(new List<int> { 1, 2, 3 }, "\""));
            Assert.Equal("1 2 3", Meow.Helper.IEnumerable.Join(new List<int> { 1, 2, 3 }, "", " "));
            Assert.Equal("1;2;3", Meow.Helper.IEnumerable.Join(new List<int> { 1, 2, 3 }, "", ";"));
            Assert.Equal("1,2,3", Meow.Helper.IEnumerable.Join(new List<string> { "1", "2", "3" }));
            Assert.Equal("'1','2','3'", Meow.Helper.IEnumerable.Join(new List<string> { "1", "2", "3" }, "'"));

            var list = new List<Guid> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" )
            };
            Assert.Equal("83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A".ToLower(), Meow.Helper.IEnumerable.Join(list));
            Assert.Equal("'83B0233C-A24F-49FD-8083-1337209EBC9A','EAB523C6-2FE7-47BE-89D5-C6D440C3033A'".ToLower(), Meow.Helper.IEnumerable.Join(list, "'"));
        }
    }
}