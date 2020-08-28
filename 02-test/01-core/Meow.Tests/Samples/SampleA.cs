using System;

namespace Meow.Tests.Samples
{
    /// <summary>
    /// 测试样例A
    /// </summary>
    public class SampleA
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 学分
        /// </summary>
        public EnumSample? Credit { get; set; }
    }
}
