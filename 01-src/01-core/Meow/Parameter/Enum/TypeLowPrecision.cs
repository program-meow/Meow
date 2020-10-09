using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 类型低精度枚举
    /// </summary>
    public enum TypeLowPrecision
    {
        /// <summary>
        /// Null类型
        /// </summary>        
        [Description("Null类型")]
        Null = 1,
        /// <summary>
        /// 值类型
        /// </summary>        
        [Description("值类型")]
        Value = 2,
        /// <summary>
        /// 引用类型
        /// </summary>        
        [Description("引用类型")]
        Reference = 3,
    }
}