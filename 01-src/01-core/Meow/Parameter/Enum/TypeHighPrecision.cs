using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 类型高精度枚举
    /// </summary>
    public enum TypeHighPrecision
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
        /// 8位无符号整数类型
        /// </summary>        
        [Description("8位无符号整数类型")]
        Byte = 3,
        /// <summary>
        /// 16位Unicode字符类型
        /// </summary>        
        [Description("16位Unicode字符类型")]
        Char = 4,
        /// <summary>
        /// 128位精确的十进制值，28-29有效位数类型
        /// </summary>        
        [Description("128位精确的十进制值，28-29有效位数类型")]
        Decimal = 5,
        /// <summary>
        /// 64位双精度浮点类型
        /// </summary>        
        [Description("64位双精度浮点类型")]
        Double = 6,
        /// <summary>
        /// 32位单精度浮点类型
        /// </summary>        
        [Description("32位单精度浮点类型")]
        Float = 7,
        /// <summary>
        /// 32位有符号整数类型
        /// </summary>        
        [Description("32位有符号整数类型")]
        Int = 8,
        /// <summary>
        /// 64位有符号整数类型
        /// </summary>        
        [Description("64位有符号整数类型")]
        Long = 9,
        /// <summary>
        /// 8位有符号整数类型
        /// </summary>        
        [Description("8位有符号整数类型")]
        Sbyte = 10,
        /// <summary>
        /// 16位有符号整数类型
        /// </summary>        
        [Description("16位有符号整数类型")]
        Short = 11,
        /// <summary>
        /// 32位无符号整数类型
        /// </summary>        
        [Description("32位无符号整数类型")]
        Uint = 12,
        /// <summary>
        /// 64位无符号整数类型
        /// </summary>        
        [Description("64位无符号整数类型")]
        Ulong = 13,
        /// <summary>
        /// 16位无符号整数类型
        /// </summary>        
        [Description("16位无符号整数类型")]
        Ushort = 14,
        /// <summary>
        /// 枚举类型
        /// </summary>        
        [Description("枚举类型")]
        Enum = 15,
        /// <summary>
        /// 日期类型
        /// </summary>        
        [Description("日期类型")]
        DateTime = 16,
        /// <summary>
        /// 字符串类型
        /// </summary>        
        [Description("字符串类型")]
        String = 17,
        /// <summary>
        /// 集合类型
        /// </summary>        
        [Description("集合类型")]
        Collection = 18,
        /// <summary>
        /// 对象类型
        /// </summary>        
        [Description("对象类型")]
        Object = 19,
    }
}