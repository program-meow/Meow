using System;
using System.Collections.Generic;
using System.Linq;
using Meow.Common.Test;
using Meow.Common.Test.Sample;
using Meow.Extension.Helper;
using Meow.Query.Pager;
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
            var list = TestSampleCreate.GetList();
            Assert.Equal(2, list.Distinct(t => t.GuidValue).Count());
            Assert.Equal(3, list.Distinct(t => t.TestValue).Count());
            Assert.Equal(5, list.Distinct(t => t.NullableIntValue).Count());
            Assert.True(list.Distinct(t => t.EnumValue).Count() == 1);
        }

        /// <summary>
        /// 测试添加查询条件
        /// </summary>
        [Fact]
        public void TestWhereIf()
        {
            var list = TestSampleCreate.GetList();
            Assert.Equal(2, list.WhereIf(t => t.TestValue.Contains("小明"), true).Count());
            Assert.Equal(5, list.WhereIf(t => t.TestValue.Contains("小明"), false).Count());
        }

        /// <summary>
        /// 测试添加查询条件
        /// </summary>
        [Fact]
        public void TestWhereIfNotEmpty()
        {
            var list = TestSampleCreate.GetList();
            Assert.Equal(2, list.WhereIfNotEmpty(t => t.TestValue.Contains("小明")).Count());
            Assert.Equal(5, list.WhereIfNotEmpty(t => t.TestValue.Contains("")).Count());
        }

        /// <summary>
        /// 添加范围查询条件
        /// </summary>
        [Fact]
        public void TestBetween()
        {
            var list = TestSampleCreate.GetList();
            Assert.Equal(3, list.Between(t => t.IntValue, 2, (int?)null).Count);
            Assert.Equal(4, list.Between(t => t.DoubleValue, (double)0, (double)3).Count);
            Assert.True(list.Between(t => t.DecimalValue, (decimal)6, (decimal?)null).Count == 0);
            var date = new DateTime(2002, 1, 1, 1, 1, 1);
            Assert.True(list.Between(t => t.DateValue, date, date).Count == 1);
        }

        /// <summary>
        /// 测试分页，包含排序
        /// </summary>
        [Fact]
        public void TestPage()
        {
            var list = TestSampleCreate.GetList();
            Assert.Equal(2, list.Page(new Pager(1, 2)).Count);
            Assert.True(list.Page(new Pager(3, 2)).Count == 1);
            Assert.True(list.Page(new Pager(10, 2)).Count == 0);
        }

        /// <summary>
        /// 测试转换为分页列表，包含排序分页操作
        /// </summary>
        [Fact]
        public void TestToPagerList()
        {
            var list = TestSampleCreate.GetList();
            var pag1 = list.ToPagerList(new Pager(1, 2));
            Assert.Equal(5, pag1.TotalCount);
            Assert.Equal(3, pag1.PageCount);
            Assert.Equal(2, pag1.Data.Count);

            var pag2 = list.ToPagerList(new Pager(1, 10));
            Assert.Equal(5, pag2.TotalCount);
            Assert.Equal(1, pag2.PageCount);
            Assert.Equal(5, pag2.Data.Count);

            var pag3 = list.ToPagerList(new Pager(10, 10));
            Assert.Equal(5, pag3.TotalCount);
            Assert.Equal(1, pag3.PageCount);
            Assert.True(pag3.Data.Count == 0);
        }
    }
}
