using System.ComponentModel;

namespace Meow.Common.Test.Sample
{
    /// <summary>
    /// 枚举测试样例
    /// </summary>
    public enum SampleEnum
    {
        /// <summary>
        /// A1
        /// </summary>
        A = 1,
        /// <summary>
        /// B2
        /// </summary>
        [Description("B2")]
        B = 2,
        /// <summary>
        /// C3
        /// </summary>
        [Description("C3")]
        C = 3,
        /// <summary>
        /// D4
        /// </summary>
        [Description("D4")]
        D = 4,
        /// <summary>
        /// E5
        /// </summary>
        [Description("E5")]
        E = 5
    }
}
