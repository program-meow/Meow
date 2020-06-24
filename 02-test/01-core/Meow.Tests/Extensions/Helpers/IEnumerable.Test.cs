using System;
using System.Collections.Generic;
using Meow.Extensions.Helpers;
using Xunit;

namespace Meow.Tests.Extensions.Helpers
{
    /// <summary>
    /// ���Լ���������չ
    /// </summary>
    public class IEnumerableTest
    {
        /// <summary>
        /// ���Խ���������Ϊ���ָ������ַ���
        /// </summary>
        [Fact]
        public void TestJoin()
        {
            Assert.Equal("1,2,3", new List<int> { 1, 2, 3 }.Join());
            Assert.Equal("'1','2','3'", new List<int> { 1, 2, 3 }.Join("'"));
            Assert.Equal("123", new List<int> { 1, 2, 3 }.Join("", ""));
            Assert.Equal("\"1\",\"2\",\"3\"", new List<int> { 1, 2, 3 }.Join("\""));
            Assert.Equal("1 2 3", new List<int> { 1, 2, 3 }.Join("", " "));
            Assert.Equal("1;2;3", new List<int> { 1, 2, 3 }.Join("", ";"));
            Assert.Equal("1,2,3", new List<string> { "1", "2", "3" }.Join());
            Assert.Equal("'1','2','3'", new List<string> { "1", "2", "3" }.Join("'"));

            var list = new List<Guid> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" )
            };
            Assert.Equal("83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A".ToLower(), list.Join());
            Assert.Equal("'83B0233C-A24F-49FD-8083-1337209EBC9A','EAB523C6-2FE7-47BE-89D5-C6D440C3033A'".ToLower(), list.Join("'"));
        }

        /// <summary>
        /// ���԰�ȫ��ȡֵ
        /// </summary>
        [Fact]
        public void TestSafeValue()
        {
            var list = new List<Guid> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" )
            };
            Assert.Equal(2, list.SafeValue().Count);
            list = null;
            Assert.True(list.SafeValue().Count == 0);
        }

    }
}
