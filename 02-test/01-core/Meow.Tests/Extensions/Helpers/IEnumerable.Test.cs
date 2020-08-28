using System;
using System.Collections.Generic;
using System.Linq;
using Meow.Extensions.Helpers;
using Meow.Tests.Samples;
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

        /// <summary>
        /// ����ת��Ϊ���ɿ�ֵ
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
        /// ����ת��Ϊ�ɿ�ֵ
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
        /// ���Թ��˿�ֵ
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
        /// ���Թ��˿�ֵ - �ַ���
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
        /// ���Թ��˿�ֵ
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
        /// ���Թ����ظ�ֵ
        /// </summary>
        [Fact]
        public void TestDistinct()
        {
            var id1 = new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A");
            var id2 = new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A");
            var name1 = "С��";
            var name2 = "С��";
            var name3 = "С��";
            var age1 = 1;
            var age2 = 2;
            var age3 = 3;
            var age4 = 4;
            int? age5 = null;
            var credit = EnumSample.A;
            var list = new List<SampleA>
            {
                new SampleA
                {
                    Id = id1,
                    Name = name1,
                    Age = age1,
                    Credit = credit,
                },
                new SampleA
                {
                    Id = id2,
                    Name = name2,
                    Age = age2,
                    Credit = credit,
                },
                new SampleA
                {
                    Id = id1,
                    Name = name3,
                    Age = age3,
                    Credit = credit,
                },
                new SampleA
                {
                    Id = id2,
                    Name = name1,
                    Age = age4,
                    Credit = credit,
                },
                new SampleA
                {
                    Id = id1,
                    Name = name2,
                    Age = age5,
                    Credit = credit,
                },
            };

            Assert.Equal(2, list.Distinct(t => t.Id).Count());
            Assert.Equal(3, list.Distinct(t => t.Name).Count());
            Assert.Equal(5, list.Distinct(t => t.Age).Count());
            Assert.True(list.Distinct(t => t.Credit).Count() == 1);
        }
    }
}
