using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 操作系统类型
    /// </summary>
    public enum OperatingSystem
    {
        /// <summary>
        /// Windows操作系统
        /// </summary>
        [Description("Windows操作系统")]
        Windows = 1,
        /// <summary>
        /// Linux操作系统
        /// </summary>
        [Description("Linux操作系统")]
        Linux = 2,
        /// <summary>
        /// 苹果操作系统
        /// </summary>
        [Description("苹果操作系统")]
        OSX = 3,
    }
}