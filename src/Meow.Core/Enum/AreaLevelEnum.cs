using System.ComponentModel;

namespace Meow.Enum
{
    /// <summary>
    /// 地区等级
    /// </summary>
    public enum AreaLevelEnum
    {
        /// <summary>
        /// 国家
        /// </summary>
        [Description("国家")]
        Country = 1,
        /// <summary>
        /// 省份
        /// </summary>
        [Description("省份")]
        Province = 2,
        /// <summary>
        /// 城市
        /// </summary>
        [Description("城市")]
        City = 3,
        /// <summary>
        /// 区县
        /// </summary>
        [Description("区县")]
        County = 4,
        /// <summary>
        /// 街道/乡镇
        /// </summary>
        [Description("街道/乡镇")]
        Town = 5,
    }
}