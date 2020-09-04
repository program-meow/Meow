using System;
using System.Collections.Generic;
using System.Linq;
using Meow.Common.Test.Sample;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// 测试集合类型扩展
    /// </summary>
    public class IEnumerableTest
    {
        /// <summary>
        /// 测试将集合连接为带分隔符的字符串
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
        /// 测试安全获取值
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

        /// <summary>
        /// 测试转换为不可空值
        /// </summary>
        [Fact]
        public void TestToNotNull()
        {
            var list = new List<Guid?> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" ),
                null
            };
            List<Guid> result = list.ToNotNull();
            Assert.Equal(2, result.Count);
            list = null;
            Assert.True(list.ToNotNull().Count == 0);
        }

        /// <summary>
        /// 测试转换为可空值
        /// </summary>
        [Fact]
        public void TestToOrNull()
        {
            var list = new List<Guid> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" )
            };
            List<Guid?> result = list.ToOrNull();
            Assert.Equal(2, result.Count);
            list = null;
            Assert.True(list.ToOrNull().Count == 0);
        }

        /// <summary>
        /// 测试过滤空值
        /// </summary>
        [Fact]
        public void TestFilterNull()
        {
            var list = new List<Guid?> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                null,
                Guid.Empty,
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" )
            };
            List<Guid?> result = list.FilterNull();
            Assert.Equal(3, result.Count);
            list = null;
            Assert.True(list.FilterNull().Count == 0);
        }

        /// <summary>
        /// 测试过滤空值 - 字符串
        /// </summary>
        [Fact]
        public void TestFilterEmpty_String()
        {
            var list = new List<string> {
                "83B0233C-A24F-49FD-8083-1337209EBC9A",
                null,
                string.Empty,
                "EAB523C6-2FE7-47BE-89D5-C6D440C3033A"
            };
            List<string> result = list.FilterEmpty();
            Assert.Equal(2, result.Count);
            list = null;
            Assert.True(list.FilterNull().Count == 0);
        }

        /// <summary>
        /// 测试过滤空值
        /// </summary>
        [Fact]
        public void TestFilterEmpty()
        {
            var list1 = new List<Guid> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                Guid.Empty,
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" )
            };
            List<Guid> result1 = list1.FilterEmpty();
            Assert.Equal(2, result1.Count);
            result1 = null;
            Assert.True(result1.FilterNull().Count == 0);

            var list2 = new List<Guid?> {
                new Guid( "83B0233C-A24F-49FD-8083-1337209EBC9A" ),
                Guid.Empty,
                null,
                new Guid( "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" )
            };
            List<Guid?> result2 = list2.FilterEmpty();
            Assert.Equal(2, result2.Count);
            list2 = null;
            Assert.True(list2.FilterNull().Count == 0);
        }

        /// <summary>
        /// 测试过滤重复值
        /// </summary>
        [Fact]
        public void TestDistinct()
        {
            var guidValue1 = new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A");
            var guidValue2 = new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A");
            var testValue1 = "小明";
            var testValue2 = "小王";
            var testValue3 = "小李";
            var intValue1 = 1;
            var intValue2 = 2;
            var intValue3 = 3;
            var intValue4 = 4;
            int? intValue5 = null;
            var enumValue = SampleEnum.A;
            var list = new List<Sample>
            {
                new Sample
                {
                    GuidValue = guidValue1,
                    TestValue = testValue1,
                    NullableIntValue = intValue1,
                    EnumValue = enumValue,
                },
                new Sample
                {
                    GuidValue = guidValue2,
                    TestValue = testValue2,
                    NullableIntValue = intValue2,
                    EnumValue = enumValue,
                },
                new Sample
                {
                    GuidValue = guidValue1,
                    TestValue = testValue3,
                    NullableIntValue = intValue3,
                    EnumValue = enumValue,
                },
                new Sample
                {
                    GuidValue = guidValue2,
                    TestValue = testValue1,
                    NullableIntValue = intValue4,
                    EnumValue = enumValue,
                },
                new Sample
                {
                    GuidValue = guidValue1,
                    TestValue = testValue2,
                    NullableIntValue = intValue5,
                    EnumValue = enumValue,
                },
            };

            Assert.Equal(2, list.Distinct(t => t.GuidValue).Count());
            Assert.Equal(3, list.Distinct(t => t.TestValue).Count());
            Assert.Equal(5, list.Distinct(t => t.NullableIntValue).Count());
            Assert.True(list.Distinct(t => t.EnumValue).Count() == 1);
        }
    }
}
