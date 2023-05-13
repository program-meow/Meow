using System.ComponentModel;

namespace Meow.Type
{
    /// <summary>
    /// 类型枚举
    /// </summary>
    public enum TypeEnum
    {
        /// <summary>
        /// 类型：sbyte；
        /// 范围：-128 到 127；
        /// 大小：8 位带符号整数；
        /// </summary>
        [Description("sbyte")]
        Sbyte = 1,
        /// <summary>
        /// 类型：byte；
        /// 范围：0 到 255；
        /// 大小：无符号的 8 位整数；
        /// </summary>
        [Description("byte")]
        Byte = 2,
        /// <summary>
        /// 类型：short；
        /// 范围：-32,768 到 32,767；
        /// 大小：有符号 16 位整数；
        /// </summary>
        [Description("short")]
        Short = 3,
        /// <summary>
        /// 类型：ushort；
        /// 范围：0 到 65,535；
        /// 大小：无符号 16 位整数；
        /// </summary>
        [Description("ushort")]
        Ushort = 4,
        /// <summary>
        /// 类型：int；
        /// 范围：-2,147,483,648 到 2,147,483,647；
        /// 大小：带符号的 32 位整数；
        /// </summary>
        [Description("int")]
        Int = 5,
        /// <summary>
        /// 类型：uint；
        /// 范围：0 到 4,294,967,295；
        /// 大小：无符号的 32 位整数；
        /// </summary>
        [Description("uint")]
        Uint = 6,
        /// <summary>
        /// 类型：long；
        /// 范围：-9,223,372,036,854,775,808 到 9,223,372,036,854,775,807；
        /// 大小：64 位带符号整数；
        /// </summary>
        [Description("long")]
        Long = 7,
        /// <summary>
        /// 类型：ulong；
        /// 范围：0 到 18,446,744,073,709,551,615；
        /// 大小：无符号 64 位整数；
        /// </summary>
        [Description("ulong")]
        Ulong = 8,
        /// <summary>
        /// 类型：nint；
        /// 范围：取决于（在运行时计算的）平台；
        /// 大小：带符号的 32 位或 64 位整数；
        /// </summary>
        [Description("nint")]
        Nint = 9,
        /// <summary>
        /// 类型：nuint；
        /// 范围：取决于（在运行时计算的）平台；
        /// 大小：无符号的 32 位或 64 位整数；
        /// </summary>
        [Description("nuint")]
        Nuint = 10,
        /// <summary>
        /// 类型：float；
        /// 范围：±1.5 x 10−45 至 ±3.4 x 1038；
        /// 精度：大约 6-9 位数字；
        /// 大小：4 个字节；
        /// </summary>
        [Description("float")]
        Float = 11,
        /// <summary>
        /// 类型：double；
        /// 范围：±5.0 × 10−324 到 ±1.7 × 10308；
        /// 精度：大约 15-17 位数字；
        /// 大小：8 个字节；
        /// </summary>
        [Description("double")]
        Double = 12,
        /// <summary>
        /// 类型：decimal；
        /// 范围：±1.0 x 10-28 至 ±7.9228 x 1028；
        /// 精度：28-29 位；
        /// 大小：16 个字节；
        /// </summary>
        [Description("decimal")]
        Decimal = 13,
        /// <summary>
        /// 类型：bool；
        /// 范围：true 或 false；
        /// </summary>
        [Description("bool")]
        Bool = 14,
        /// <summary>
        /// 类型：char ；
        /// 范围：U+0000 到 U+FFFF；
        /// 大小：16 位；
        /// </summary>
        [Description("char")]
        Char = 15,
        /// <summary>
        /// string
        /// </summary>
        [Description("string")]
        String = 16,
        /// <summary>
        /// guid
        /// </summary>
        [Description("guid")]
        Guid = 17,
        /// <summary>
        /// DateTime
        /// </summary>
        [Description("DateTime")]
        DateTime = 18,
        /// <summary>
        /// enum
        /// </summary>
        [Description("enum")]
        Enum = 19,
        /// <summary>
        /// objects
        /// </summary>
        [Description("objects")]
        Objects = 20,
        /// <summary>
        /// array
        /// </summary>
        [Description("array")]
        Array = 21,
        /// <summary>
        /// dictionary
        /// </summary>
        [Description("dictionary")]
        Dictionary = 22,
        /// <summary>
        /// list
        /// </summary>
        [Description("list")]
        List = 23,
    }
}
