using System.ComponentModel;

namespace Meow.Enum
{
    /// <summary>
    /// 货币
    /// </summary>
    public enum MoneyEnum
    {
        /// <summary>
        /// 人民币
        /// </summary>
        [Description("￥")]
        RMB = 1,
        /// <summary>
        /// 美元
        /// </summary>
        [Description("$")]
        USD = 2,
    }
}
