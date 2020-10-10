using System;
using System.Collections.Generic;
using Meow.Parameter.Enum;

namespace Meow.Test
{
    /// <summary>
    /// 测试对象
    /// </summary>
    public class TestObjct
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 枚举
        /// </summary>
        public Database? Enum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public TestObjct Value { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<TestObjct> List { get; set; }

    }
}
