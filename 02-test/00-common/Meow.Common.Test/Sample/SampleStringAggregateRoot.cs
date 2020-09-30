using System;
using Meow.Application.Domain.Model;

namespace Meow.Common.Test.Sample
{
    /// <summary>
    /// string聚合根测试样例
    /// </summary>
    public class SampleStringAggregateRoot : AggregateRoot<SampleStringAggregateRoot, string>
    {
        /// <summary>
        /// 初始化string聚合根测试样例
        /// </summary>
        public SampleStringAggregateRoot()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// 初始化string聚合根测试样例
        /// </summary>
        /// <param name="id">标识</param>
        public SampleStringAggregateRoot(string id)
            : base(id)
        {
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}