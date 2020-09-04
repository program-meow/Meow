using System.ComponentModel;

namespace Meow.Mathematics.Enum
{
    /// <summary>
    /// 查询边界
    /// </summary>
    public enum Boundary
    {
        /// <summary>
        /// 包含左边
        /// </summary>
        [Description("包含左边")]
        Left,
        /// <summary>
        /// 包含右边
        /// </summary>
        [Description("包含右边")]
        Right,
        /// <summary>
        /// 包含两边
        /// </summary>
        [Description("包含两边")]
        Both,
        /// <summary>
        /// 不包含
        /// </summary>
        [Description("不包含")]
        Neither
    }
}