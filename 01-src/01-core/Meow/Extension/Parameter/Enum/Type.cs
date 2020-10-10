using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;

namespace Meow.Extension.Parameter.Enum
{
    /// <summary>
    /// 类型枚举扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换类型中精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeMediumPrecision ToMedium(this TypeHighPrecision value)
        {
            switch (value)
            {
                case TypeHighPrecision.Null:
                    return TypeMediumPrecision.Null;
                case TypeHighPrecision.Bool:
                    return TypeMediumPrecision.Bool;
                case TypeHighPrecision.Byte:
                case TypeHighPrecision.Char:
                case TypeHighPrecision.Decimal:
                case TypeHighPrecision.Double:
                case TypeHighPrecision.Float:
                case TypeHighPrecision.Int:
                case TypeHighPrecision.Long:
                case TypeHighPrecision.Sbyte:
                case TypeHighPrecision.Short:
                case TypeHighPrecision.Uint:
                case TypeHighPrecision.Ulong:
                case TypeHighPrecision.Ushort:
                    return TypeMediumPrecision.Number;
                case TypeHighPrecision.Enum:
                    return TypeMediumPrecision.Enum;
                case TypeHighPrecision.DateTime:
                    return TypeMediumPrecision.DateTime;
                case TypeHighPrecision.String:
                    return TypeMediumPrecision.String;
                case TypeHighPrecision.Object:
                    return TypeMediumPrecision.Object;
                case TypeHighPrecision.Array:
                case TypeHighPrecision.Dictionary:
                case TypeHighPrecision.List:
                    return TypeMediumPrecision.Collection;
                default:
                    throw new Warning("超出范围");
            }
        }

        /// <summary>
        /// 转换类型中精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeMediumPrecision? ToMedium(this TypeHighPrecision? value)
        {
            if (value.IsNull())
                return null;
            return value.SafeValue().ToMedium();
        }

        /// <summary>
        /// 转换类型低精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeLowPrecision ToLow(this TypeHighPrecision value)
        {
            switch (value)
            {
                case TypeHighPrecision.Null:
                    return TypeLowPrecision.Null;
                case TypeHighPrecision.Bool:
                case TypeHighPrecision.Byte:
                case TypeHighPrecision.Char:
                case TypeHighPrecision.Decimal:
                case TypeHighPrecision.Double:
                case TypeHighPrecision.Float:
                case TypeHighPrecision.Int:
                case TypeHighPrecision.Long:
                case TypeHighPrecision.Sbyte:
                case TypeHighPrecision.Short:
                case TypeHighPrecision.Uint:
                case TypeHighPrecision.Ulong:
                case TypeHighPrecision.Ushort:
                case TypeHighPrecision.Enum:
                case TypeHighPrecision.DateTime:
                    return TypeLowPrecision.Value;
                case TypeHighPrecision.String:
                case TypeHighPrecision.Object:
                case TypeHighPrecision.Array:
                case TypeHighPrecision.Dictionary:
                case TypeHighPrecision.List:
                    return TypeLowPrecision.Reference;
                default:
                    throw new Warning("超出范围");
            }
        }

        /// <summary>
        /// 转换类型低精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeLowPrecision? ToLow(this TypeHighPrecision? value)
        {
            if (value.IsNull())
                return null;
            return value.SafeValue().ToLow();
        }

        /// <summary>
        /// 转换类型低精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeLowPrecision ToLow(this TypeMediumPrecision value)
        {
            switch (value)
            {
                case TypeMediumPrecision.Null:
                    return TypeLowPrecision.Null;
                case TypeMediumPrecision.Bool:
                case TypeMediumPrecision.Number:
                case TypeMediumPrecision.Enum:
                case TypeMediumPrecision.DateTime:
                    return TypeLowPrecision.Value;
                case TypeMediumPrecision.String:
                case TypeMediumPrecision.Object:
                case TypeMediumPrecision.Collection:
                    return TypeLowPrecision.Reference;
                default:
                    throw new Warning("超出范围");
            }
        }

        /// <summary>
        /// 转换类型低精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeLowPrecision? ToLow(this TypeMediumPrecision? value)
        {
            if (value.IsNull())
                return null;
            return value.SafeValue().ToLow();
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(this TypeHighPrecision value)
        {
            if (value.IsNull())
                return false;
            return value.ToLow() == TypeLowPrecision.Value || value == TypeHighPrecision.String;
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(this TypeHighPrecision? value)
        {
            if (value.IsNull())
                return false;
            return value.SafeValue().IsSingleType();
        }
    }
}