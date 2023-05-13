namespace Meow.Type
{
    /// <summary>
    /// 类型名
    /// </summary>
    public static class TypeName
    {
        /// <summary>
        /// 类型：sbyte；
        /// 范围：-128 到 127；
        /// 大小：8 位带符号整数；
        /// </summary>
        public const string Sbyte = "SByte";
        /// <summary>
        /// 类型：byte；
        /// 范围：0 到 255；
        /// 大小：无符号的 8 位整数；
        /// </summary>
        public const string Byte = "Byte";
        /// <summary>
        /// 类型：short；
        /// 范围：-32,768 到 32,767；
        /// 大小：有符号 16 位整数；
        /// </summary>
        public const string Short = "Int16";
        /// <summary>
        /// 类型：ushort；
        /// 范围：0 到 65,535；
        /// 大小：无符号 16 位整数；
        /// </summary>
        public const string Ushort = "UInt16";
        /// <summary>
        /// 类型：int；
        /// 范围：-2,147,483,648 到 2,147,483,647；
        /// 大小：带符号的 32 位整数；
        /// </summary>
        public const string Int = "Int32";
        /// <summary>
        /// 类型：uint；
        /// 范围：0 到 4,294,967,295；
        /// 大小：无符号的 32 位整数；
        /// </summary>
        public const string Uint = "UInt32";
        /// <summary>
        /// 类型：long；
        /// 范围：-9,223,372,036,854,775,808 到 9,223,372,036,854,775,807；
        /// 大小：64 位带符号整数；
        /// </summary>
        public const string Long = "Int64";
        /// <summary>
        /// 类型：ulong；
        /// 范围：0 到 18,446,744,073,709,551,615；
        /// 大小：无符号 64 位整数；
        /// </summary>
        public const string Ulong = "UInt64";
        /// <summary>
        /// 类型：nint；
        /// 范围：取决于（在运行时计算的）平台；
        /// 大小：带符号的 32 位或 64 位整数；
        /// </summary>
        public const string Nint = "IntPtr";
        /// <summary>
        /// 类型：nuint；
        /// 范围：取决于（在运行时计算的）平台；
        /// 大小：无符号的 32 位或 64 位整数；
        /// </summary>
        public const string Nuint = "UIntPtr";
        /// <summary>
        /// 类型：float；
        /// 范围：±1.5 x 10−45 至 ±3.4 x 1038；
        /// 精度：大约 6-9 位数字；
        /// 大小：4 个字节；
        /// </summary>
        public const string Float = "Single";
        /// <summary>
        /// 类型：double；
        /// 范围：±5.0 × 10−324 到 ±1.7 × 10308；
        /// 精度：大约 15-17 位数字；
        /// 大小：8 个字节；
        /// </summary>
        public const string Double = "Double";
        /// <summary>
        /// 类型：decimal；
        /// 范围：±1.0 x 10-28 至 ±7.9228 x 1028；
        /// 精度：28-29 位；
        /// 大小：16 个字节；
        /// </summary>
        public const string Decimal = "Decimal";
        /// <summary>
        /// 类型：bool；
        /// 范围：true 或 false；
        /// </summary>
        public const string Bool = "Boolean";
        /// <summary>
        /// 类型：char ；
        /// 范围：U+0000 到 U+FFFF；
        /// 大小：16 位；
        /// </summary>
        public const string Char = "Char";
        /// <summary>
        /// string
        /// </summary>
        public const string String = "String";
        /// <summary>
        /// guid
        /// </summary>
        public const string Guid = "Guid";
        /// <summary>
        /// DateTime
        /// </summary>
        public const string DateTime = "DateTime";
    }
}
