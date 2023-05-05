using System.ComponentModel;

namespace Meow.Enum
{
    /// <summary>
    /// 主体
    /// </summary>
    public enum MainBodyEnum
    {
        /// <summary>
        /// 个人
        /// </summary>        
        [Description("个人")]
        Personal = 1,
        /// <summary>
        /// 个体工商户
        /// </summary>
        [Description("个体工商户")]
        IndividualBusiness = 2,
        /// <summary>
        /// 企业
        /// </summary>
        [Description("企业")]
        Enterprise = 3,
        /// <summary>
        /// 媒体
        /// </summary>
        [Description("媒体")]
        Media = 4,
        /// <summary>
        /// 政府
        /// </summary>
        [Description("政府")]
        Government = 5,
        /// <summary>
        /// 其他组织
        /// </summary>
        [Description("其他组织")]
        OtherOrganizations = 6,
    }
}