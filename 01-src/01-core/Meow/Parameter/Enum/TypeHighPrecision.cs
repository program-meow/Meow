using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 类型高精度枚举
    /// </summary>
    public enum TypeHighPrecision
    {
        /// <summary>
        /// 布尔值类型
        /// </summary>        
        [Description("布尔值类型")]
        Bool = 1,
        /// <summary>
        /// 8位无符号整数类型
        /// </summary>        
        [Description("8位无符号整数类型")]
        Byte = 2,
        /// <summary>
        /// 16位Unicode字符类型
        /// </summary>        
        [Description("16位Unicode字符类型")]
        Char = 3,
        /// <summary>
        /// 128位精确的十进制值，28-29有效位数类型
        /// </summary>        
        [Description("128位精确的十进制值，28-29有效位数类型")]
        Decimal = 4,
        /// <summary>
        /// 64位双精度浮点类型
        /// </summary>        
        [Description("64位双精度浮点类型")]
        Double = 5,
        /// <summary>
        /// 32位单精度浮点类型
        /// </summary>        
        [Description("32位单精度浮点类型")]
        Float = 6,
        /// <summary>
        /// 32位有符号整数类型
        /// </summary>        
        [Description("32位有符号整数类型")]
        Int = 7,
        /// <summary>
        /// 64位有符号整数类型
        /// </summary>        
        [Description("64位有符号整数类型")]
        Long = 8,
        /// <summary>
        /// 8位有符号整数类型
        /// </summary>        
        [Description("8位有符号整数类型")]
        Sbyte = 9,
        /// <summary>
        /// 16位有符号整数类型
        /// </summary>        
        [Description("16位有符号整数类型")]
        Short = 10,
        /// <summary>
        /// 32位无符号整数类型
        /// </summary>        
        [Description("32位无符号整数类型")]
        Uint = 11,
        /// <summary>
        /// 64位无符号整数类型
        /// </summary>        
        [Description("64位无符号整数类型")]
        Ulong = 12,
        /// <summary>
        /// 16位无符号整数类型
        /// </summary>        
        [Description("16位无符号整数类型")]
        Ushort = 13,
        /// <summary>
        /// 枚举类型
        /// </summary>        
        [Description("枚举类型")]
        Enum = 14,
        /// <summary>
        /// 日期类型
        /// </summary>        
        [Description("日期类型")]
        DateTime = 15,
        /// <summary>
        /// 集合类型
        /// </summary>        
        [Description("集合类型")]
        Collection = 16,
        /// <summary>
        /// 对象类型
        /// </summary>        
        [Description("对象类型")]
        Object = 17,
        /// <summary>
        /// 动态类型
        /// </summary>        
        [Description("动态类型")]
        Dynamic = 18,
        /// <summary>
        /// 字符串类型
        /// </summary>        
        [Description("字符串类型")]
        String = 19,
        /// <summary>
        /// 指针类型
        /// </summary>        
        [Description("指针类型")]
        Pointer = 20,
    }
}