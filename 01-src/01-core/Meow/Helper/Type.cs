using System.Reflection;
using Meow.Extension.Helper;
using Meow.Extension.Parameter.Enum;
using Meow.Parameter.Enum;

namespace Meow.Helper
{
    /// <summary>
    /// 类型操作
    /// </summary>
    public static class Type
    {
        #region PropertyInfo

        /// <summary>
        /// 获取类型高精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeHighPrecision GetTypeHighPrecisionEnum(PropertyInfo value)
        {
            if (value.IsNull())
                return TypeHighPrecision.Null;
            if (Meow.Helper.Reflection.IsEnum(value))
                return TypeHighPrecision.Enum;
            if (value.PropertyType.Name.Contains("[]") || value.PropertyType.Name.StartsWith("List"))
                return TypeHighPrecision.Collection;
            switch (value.PropertyType.Name)
            {
                case "Boolean":
                    return TypeHighPrecision.Bool;
                case "Byte":
                    return TypeHighPrecision.Byte;
                case "Char":
                    return TypeHighPrecision.Char;
                case "Decimal":
                    return TypeHighPrecision.Decimal;
                case "Double":
                    return TypeHighPrecision.Double;
                case "Single":
                    return TypeHighPrecision.Float;
                case "Int32":
                    return TypeHighPrecision.Int;
                case "Int64":
                    return TypeHighPrecision.Long;
                case "SByte":
                    return TypeHighPrecision.Sbyte;
                case "Int16":
                    return TypeHighPrecision.Short;
                case "UInt32":
                    return TypeHighPrecision.Uint;
                case "UInt64":
                    return TypeHighPrecision.Ulong;
                case "UInt16":
                    return TypeHighPrecision.Ushort;
                case "DateTime":
                    return TypeHighPrecision.DateTime;
                case "String":
                    return TypeHighPrecision.String;
                default:
                    return TypeHighPrecision.Object;
            }
        }

        /// <summary>
        /// 获取类型中精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeMediumPrecision GetTypeMediumPrecisionEnum(PropertyInfo value)
        {
            if (value.IsNull())
                return TypeMediumPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToMedium();
        }

        /// <summary>
        /// 获取类型低精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeLowPrecision GetTypeLowPrecisionEnum(PropertyInfo value)
        {
            if (value.IsNull())
                return TypeLowPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToLow();
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsValueType(PropertyInfo value)
        {
            if (value.IsNull())
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Value;
        }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsReferenceType(PropertyInfo value)
        {
            if (value.IsNull())
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Reference;
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(PropertyInfo value)
        {
            if (value.IsNull())
                return false;
            return IsValueType(value) || GetTypeHighPrecisionEnum(value) == TypeHighPrecision.String;
        }

        /// <summary>
        /// 是否集合类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsCollectionType(PropertyInfo value)
        {
            if (value.IsNull())
                return false;
            return GetTypeHighPrecisionEnum(value) == TypeHighPrecision.Collection;
        }

        #endregion

        #region Object

        /// <summary>
        /// 获取类型高精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeHighPrecision GetTypeHighPrecisionEnum(object value)
        {
            if (value.IsNull())
                return TypeHighPrecision.Null;
            var type = value.GetType();
            if (Meow.Helper.Reflection.IsEnum(type))
                return TypeHighPrecision.Enum;
            if (type.Name.Contains("[]") || type.Name.StartsWith("List"))
                return TypeHighPrecision.Collection;
            switch (type.Name)
            {
                case "Boolean":
                    return TypeHighPrecision.Bool;
                case "Byte":
                    return TypeHighPrecision.Byte;
                case "Char":
                    return TypeHighPrecision.Char;
                case "Decimal":
                    return TypeHighPrecision.Decimal;
                case "Double":
                    return TypeHighPrecision.Double;
                case "Single":
                    return TypeHighPrecision.Float;
                case "Int32":
                    return TypeHighPrecision.Int;
                case "Int64":
                    return TypeHighPrecision.Long;
                case "SByte":
                    return TypeHighPrecision.Sbyte;
                case "Int16":
                    return TypeHighPrecision.Short;
                case "UInt32":
                    return TypeHighPrecision.Uint;
                case "UInt64":
                    return TypeHighPrecision.Ulong;
                case "UInt16":
                    return TypeHighPrecision.Ushort;
                case "DateTime":
                    return TypeHighPrecision.DateTime;
                case "String":
                    return TypeHighPrecision.String;
                default:
                    return TypeHighPrecision.Object;
            }
        }

        /// <summary>
        /// 获取类型中精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeMediumPrecision GetTypeMediumPrecisionEnum(object value)
        {
            if (value.IsNull())
                return TypeMediumPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToMedium();
        }

        /// <summary>
        /// 获取类型低精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeLowPrecision GetTypeLowPrecisionEnum(object value)
        {
            if (value.IsNull())
                return TypeLowPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToLow();
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsValueType(object value)
        {
            if (value.IsNull())
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Value;
        }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsReferenceType(object value)
        {
            if (value.IsNull())
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Reference;
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(object value)
        {
            if (value.IsNull())
                return false;
            return IsValueType(value) || GetTypeHighPrecisionEnum(value) == TypeHighPrecision.String;
        }

        #endregion
    }
}