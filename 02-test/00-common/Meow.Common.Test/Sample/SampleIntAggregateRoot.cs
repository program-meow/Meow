using Meow.Domain.Model;

namespace Meow.Common.Test.Sample
{
    /// <summary>
    /// int聚合根测试样例
    /// </summary>
    public class SampleIntAggregateRoot : AggregateRoot<SampleIntAggregateRoot, int>
    {
        /// <summary>
        /// 初始化int聚合根测试样例
        /// </summary>
        public SampleIntAggregateRoot()
            : this(0)
        {
        }

        /// <summary>
        /// 初始化int聚合根测试样例
        /// </summary>
        /// <param name="id">标识</param>
        public SampleIntAggregateRoot(int id)
            : base(id)
        {
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}