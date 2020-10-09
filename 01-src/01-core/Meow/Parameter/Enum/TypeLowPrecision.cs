using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 类型低精度枚举
    /// </summary>
    public enum TypeLowPrecision
    {
        /// <summary>
        /// 值类型
        /// </summary>        
        [Description("值类型")]
        ValueTypes = 1,
        /// <summary>
        /// 引用类型
        /// </summary>        
        [Description("引用类型")]
        ReferenceTypes = 2,
        /// <summary>
        /// 指针类型
        /// </summary>        
        [Description("指针类型")]
        PointerTypes = 3,
    }
}