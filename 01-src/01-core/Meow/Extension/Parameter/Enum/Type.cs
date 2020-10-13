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
            return value switch
            {
                TypeHighPrecision.Null => TypeMediumPrecision.Null,
                TypeHighPrecision.Bool => TypeMediumPrecision.Bool,
                TypeHighPrecision.Byte => TypeMediumPrecision.Number,
                TypeHighPrecision.Char => TypeMediumPrecision.Number,
                TypeHighPrecision.Decimal => TypeMediumPrecision.Number,
                TypeHighPrecision.Double => TypeMediumPrecision.Number,
                TypeHighPrecision.Float => TypeMediumPrecision.Number,
                TypeHighPrecision.Int => TypeMediumPrecision.Number,
                TypeHighPrecision.Long => TypeMediumPrecision.Number,
                TypeHighPrecision.Sbyte => TypeMediumPrecision.Number,
                TypeHighPrecision.Short => TypeMediumPrecision.Number,
                TypeHighPrecision.Uint => TypeMediumPrecision.Number,
                TypeHighPrecision.Ulong => TypeMediumPrecision.Number,
                TypeHighPrecision.Ushort => TypeMediumPrecision.Number,
                TypeHighPrecision.Enum => TypeMediumPrecision.Enum,
                TypeHighPrecision.DateTime => TypeMediumPrecision.DateTime,
                TypeHighPrecision.String => TypeMediumPrecision.String,
                TypeHighPrecision.Object => TypeMediumPrecision.Object,
                TypeHighPrecision.Array => TypeMediumPrecision.Collection,
                TypeHighPrecision.Dictionary => TypeMediumPrecision.Collection,
                TypeHighPrecision.List => TypeMediumPrecision.Collection,
                _ => throw new Warning("超出范围")
            };
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
            return value switch
            {
                TypeHighPrecision.Null => TypeLowPrecision.Null,
                TypeHighPrecision.Bool => TypeLowPrecision.Value,
                TypeHighPrecision.Byte => TypeLowPrecision.Value,
                TypeHighPrecision.Char => TypeLowPrecision.Value,
                TypeHighPrecision.Decimal => TypeLowPrecision.Value,
                TypeHighPrecision.Double => TypeLowPrecision.Value,
                TypeHighPrecision.Float => TypeLowPrecision.Value,
                TypeHighPrecision.Int => TypeLowPrecision.Value,
                TypeHighPrecision.Long => TypeLowPrecision.Value,
                TypeHighPrecision.Sbyte => TypeLowPrecision.Value,
                TypeHighPrecision.Short => TypeLowPrecision.Value,
                TypeHighPrecision.Uint => TypeLowPrecision.Value,
                TypeHighPrecision.Ulong => TypeLowPrecision.Value,
                TypeHighPrecision.Ushort => TypeLowPrecision.Value,
                TypeHighPrecision.Enum => TypeLowPrecision.Value,
                TypeHighPrecision.DateTime => TypeLowPrecision.Value,
                TypeHighPrecision.String => TypeLowPrecision.Reference,
                TypeHighPrecision.Object => TypeLowPrecision.Reference,
                TypeHighPrecision.Array => TypeLowPrecision.Reference,
                TypeHighPrecision.Dictionary => TypeLowPrecision.Reference,
                TypeHighPrecision.List => TypeLowPrecision.Reference,
                _ => throw new Warning("超出范围")
            };
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
            return value switch
            {
                TypeMediumPrecision.Null => TypeLowPrecision.Null,
                TypeMediumPrecision.Bool => TypeLowPrecision.Value,
                TypeMediumPrecision.Number => TypeLowPrecision.Value,
                TypeMediumPrecision.Enum => TypeLowPrecision.Value,
                TypeMediumPrecision.DateTime => TypeLowPrecision.Value,
                TypeMediumPrecision.String => TypeLowPrecision.Reference,
                TypeMediumPrecision.Object => TypeLowPrecision.Reference,
                TypeMediumPrecision.Collection => TypeLowPrecision.Reference,
                _ => throw new Warning("超出范围")
            };
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