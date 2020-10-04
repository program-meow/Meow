using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 经济区
    /// </summary>
    public enum EconomicRegion
    {
        /// <summary>
        /// 东北地区
        /// </summary>
        [Description("东北地区")]
        NortheastRegion = 1,
        /// <summary>
        /// 东部地区
        /// </summary>
        [Description("东部地区")]
        EasternRegion = 2,
        /// <summary>
        /// 中部地区
        /// </summary>
        [Description("中部地区")]
        CentralRegion = 3,
        /// <summary>
        /// 西部地区
        /// </summary>
        [Description("西部地区")]
        WesternRegion = 4,
    }
}