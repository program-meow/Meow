using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 类型中精度枚举
    /// </summary>
    public enum TypeMediumPrecision
    {
        /// <summary>
        /// Null类型
        /// </summary>        
        [Description("Null类型")]
        Null = 1,
        /// <summary>
        /// 布尔值类型
        /// </summary>        
        [Description("布尔值类型")]
        Bool = 2,
        /// <summary>
        /// 数值类型
        /// </summary>        
        [Description("数值类型")]
        Number = 3,
        /// <summary>
        /// 枚举类型
        /// </summary>        
        [Description("枚举类型")]
        Enum = 4,
        /// <summary>
        /// 日期类型
        /// </summary>        
        [Description("日期类型")]
        DateTime = 5,
        /// <summary>
        /// 字符串类型
        /// </summary>        
        [Description("字符串类型")]
        String = 6,
        /// <summary>
        /// 对象类型
        /// </summary>        
        [Description("对象类型")]
        Object = 7,
        /// <summary>
        /// 集合类型
        /// </summary>        
        [Description("集合类型")]
        Collection = 8,
    }
}