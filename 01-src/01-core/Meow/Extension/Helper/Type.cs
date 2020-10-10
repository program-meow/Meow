using System.Reflection;
using Meow.Helper;
using Meow.Parameter.Enum;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static partial class Extension
    {

        #region PropertyInfo扩展

        /// <summary>
        /// 获取类型高精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeHighPrecision GetTypeHighPrecisionEnum(this PropertyInfo value)
        {
            return Type.GetTypeHighPrecisionEnum(value);
        }

        /// <summary>
        /// 获取类型中精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeMediumPrecision GetTypeMediumPrecisionEnum(this PropertyInfo value)
        {
            return Type.GetTypeMediumPrecisionEnum(value);
        }

        /// <summary>
        /// 获取类型低精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeLowPrecision GetTypeLowPrecisionEnum(this PropertyInfo value)
        {
            return Type.GetTypeLowPrecisionEnum(value);
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsValueType(this PropertyInfo value)
        {
            return Type.IsValueType(value);
        }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsReferenceType(this PropertyInfo value)
        {
            return Type.IsReferenceType(value);
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(this PropertyInfo value)
        {
            return Type.IsSingleType(value);
        }

        /// <summary>
        /// 是否字典类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDictionaryType(this PropertyInfo value)
        {
            return Type.IsDictionaryType(value);

        }

        /// <summary>
        /// 是否集合类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsCollectionType(this PropertyInfo value)
        {
            return Type.IsCollectionType(value);
        }

        /// <summary>
        /// 是否嵌套集合
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNestingCollectionType(this PropertyInfo value)
        {
            return Type.IsNestingCollectionType(value);
        }

        /// <summary>
        /// 是否为NULL
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNull(this PropertyInfo value)
        {
            return Type.IsNull(value);
        }

        #endregion

        #region Object扩展

        /// <summary>
        /// 获取类型高精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeHighPrecision GetTypeHighPrecisionEnum(this object value)
        {
            return Type.GetTypeHighPrecisionEnum(value);
        }

        /// <summary>
        /// 获取类型中精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeMediumPrecision GetTypeMediumPrecisionEnum(this object value)
        {
            return Type.GetTypeMediumPrecisionEnum(value);
        }

        /// <summary>
        /// 获取类型低精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeLowPrecision GetTypeLowPrecisionEnum(this object value)
        {
            return Type.GetTypeLowPrecisionEnum(value);
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsValueType(this object value)
        {
            return Type.IsValueType(value);
        }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsReferenceType(this object value)
        {
            return Type.IsReferenceType(value);
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(this object value)
        {
            return Type.IsSingleType(value);
        }

        #endregion
    }
}