using System;
using System.Collections.Generic;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// 测试集合类型扩展
    /// </summary>
    public class ListTest
    {
        /// <summary>
        /// 测试添加不为空数据
        /// </summary>
        [Fact]
        public void TestAddNoNull()
        {
            var list = new List<Guid?>();
            list.AddNoNull(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"));
            list.AddNoNull(null);
            list.AddNoNull(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"));
            Assert.Equal(2, list.Count);
        }

        /// <summary>
        /// 测试添加不为空数据
        /// </summary>
        [Fact]
        public void TestAddNoEmpty_String()
        {
            var list = new List<string>();
            list.AddNoEmpty("A");
            list.AddNoEmpty(null);
            list.AddNoEmpty("");
            list.AddNoEmpty("    ");
            list.AddNoEmpty("B");
            Assert.Equal(2, list.Count);
        }

        /// <summary>
        /// 测试添加不为空数据
        /// </summary>
        [Fact]
        public void TestAddNoEmpty_Guid()
        {
            var list1 = new List<Guid>();
            list1.AddNoEmpty(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"));
            list1.AddNoEmpty(Guid.Empty);
            list1.AddNoEmpty(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"));
            Assert.Equal(2, list1.Count);

            var list2 = new List<Guid?>();
            list2.AddNoEmpty(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"));
            list2.AddNoEmpty(Guid.Empty);
            list2.AddNoEmpty(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"));
            list2.AddNoEmpty(null);
            Assert.Equal(2, list2.Count);
        }
    }
}
