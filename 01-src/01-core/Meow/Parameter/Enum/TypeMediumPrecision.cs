using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 类型中精度枚举
    /// </summary>
    public enum TypeMediumPrecision
    {
        /// <summary>
        /// 布尔值类型
        /// </summary>        
        [Description("布尔值类型")]
        Bool = 1,
        /// <summary>
        /// 数值类型
        /// </summary>        
        [Description("数值类型")]
        Number = 2,
        /// <summary>
        /// 枚举类型
        /// </summary>        
        [Description("枚举类型")]
        Enum = 3,
        /// <summary>
        /// 日期类型
        /// </summary>        
        [Description("日期类型")]
        DateTime = 4,
        /// <summary>
        /// 集合类型
        /// </summary>        
        [Description("集合类型")]
        Collection = 5,
        /// <summary>
        /// 对象类型
        /// </summary>        
        [Description("对象类型")]
        Object = 6,
        /// <summary>
        /// 动态类型
        /// </summary>        
        [Description("动态类型")]
        Dynamic = 7,
        /// <summary>
        /// 字符串类型
        /// </summary>        
        [Description("字符串类型")]
        String = 8,
        /// <summary>
        /// 指针类型
        /// </summary>        
        [Description("指针类型")]
        Pointer = 9,
    }
}