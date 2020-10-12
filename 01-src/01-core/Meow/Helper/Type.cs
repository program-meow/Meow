using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Meow.Extension.Helper;
using Meow.Extension.Parameter.Enum;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;

namespace Meow.Helper
{
    /// <summary>
    /// 类型操作
    /// </summary>
    public static class Type
    {
        #region 基础

        /// <summary>
        /// 类型高精度枚举字典
        /// </summary>
        private static readonly List<Item> _typeHighPrecisionDictionary = new List<Item>
        {
            new Item("Nullable", TypeHighPrecision.Null),
            new Item("Boolean", TypeHighPrecision.Bool),
            new Item("Byte", TypeHighPrecision.Byte),
            new Item("Char", TypeHighPrecision.Char),
            new Item("Decimal", TypeHighPrecision.Decimal),
            new Item("Double", TypeHighPrecision.Double),
            new Item("Single", TypeHighPrecision.Float),
            new Item("Int32", TypeHighPrecision.Int),
            new Item("Int64", TypeHighPrecision.Long),
            new Item("SByte", TypeHighPrecision.Sbyte),
            new Item("Int16", TypeHighPrecision.Short),
            new Item("UInt32", TypeHighPrecision.Uint),
            new Item("UInt64", TypeHighPrecision.Ulong),
            new Item("UInt16", TypeHighPrecision.Ushort),
            new Item("Enum", TypeHighPrecision.Enum),
            new Item("DateTime", TypeHighPrecision.DateTime),
            new Item("String", TypeHighPrecision.String),
            new Item("Object", TypeHighPrecision.Object),
            new Item("Array", TypeHighPrecision.Array),
            new Item("Dictionary", TypeHighPrecision.Dictionary),
            new Item("List", TypeHighPrecision.List),
        };

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name">名称</param>
        private static string SetTypeFullName(string name)
        {
            if (name.IsEmpty() || name.StartsWith("Nullable"))
                return "Nullable";
            if (name.Contains("[]"))
                return "Array";
            if (name.StartsWith("Dictionary"))
                return "Dictionary";
            if (name.StartsWith("List"))
                return "List";
            return name;
        }

        /// <summary>
        /// 根据类型名称获取类型高精度枚举
        /// </summary>
        /// <param name="name">名称</param>
        private static TypeHighPrecision GetHighPrecisionEnumByTypeName(string name)
        {
            name = SetTypeFullName(name);
            var value = _typeHighPrecisionDictionary.FirstOrDefault(t => t.Text == name);
            return (TypeHighPrecision?)value?.Value ?? TypeHighPrecision.Object;
        }

        #endregion

        #region PropertyInfo

        /// <summary>
        /// 获取类型高精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeHighPrecision GetTypeHighPrecisionEnum(PropertyInfo value)
        {
            if (value == null)
                return TypeHighPrecision.Null;
            if (Meow.Helper.Reflection.IsEnum(value))
                return TypeHighPrecision.Enum;
            return GetHighPrecisionEnumByTypeName(value.PropertyType.Name);
        }

        /// <summary>
        /// 获取类型中精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeMediumPrecision GetTypeMediumPrecisionEnum(PropertyInfo value)
        {
            if (value == null)
                return TypeMediumPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToMedium();
        }

        /// <summary>
        /// 获取类型低精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeLowPrecision GetTypeLowPrecisionEnum(PropertyInfo value)
        {
            if (value == null)
                return TypeLowPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToLow();
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsValueType(PropertyInfo value)
        {
            if (value == null)
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Value;
        }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsReferenceType(PropertyInfo value)
        {
            if (value == null)
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Reference;
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(PropertyInfo value)
        {
            if (value == null)
                return false;
            return IsValueType(value) || GetTypeHighPrecisionEnum(value) == TypeHighPrecision.String;
        }

        /// <summary>
        /// 是否字典类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDictionaryType(PropertyInfo value)
        {
            if (value == null)
                return false;
            return GetTypeHighPrecisionEnum(value) == TypeHighPrecision.Dictionary;
        }

        /// <summary>
        /// 是否集合类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsCollectionType(PropertyInfo value)
        {
            if (value == null)
                return false;
            return GetTypeHighPrecisionEnum(value).ToMedium() == TypeMediumPrecision.Collection;
        }

        /// <summary>
        /// 是否嵌套集合
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNestingCollectionType(PropertyInfo value)
        {
            if (value == null)
                return false;
            if (value.PropertyType.FullName.Repeat("List") > 1)
                return true;
            if (value.PropertyType.FullName.Repeat("[]") > 1)
                return true;
            return false;
        }

        /// <summary>
        /// 是否为NULL
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNull(PropertyInfo value)
        {
            if (value == null)
                return true;
            return GetTypeHighPrecisionEnum(value) == TypeHighPrecision.Null;
        }

        #endregion

        #region Object

        /// <summary>
        /// 获取类型高精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeHighPrecision GetTypeHighPrecisionEnum(object value)
        {
            if (value == null)
                return TypeHighPrecision.Null;
            var type = value.GetType();
            if (Meow.Helper.Reflection.IsEnum(type))
                return TypeHighPrecision.Enum;
            return GetHighPrecisionEnumByTypeName(type.Name);
        }

        /// <summary>
        /// 获取类型中精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeMediumPrecision GetTypeMediumPrecisionEnum(object value)
        {
            if (value == null)
                return TypeMediumPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToMedium();
        }

        /// <summary>
        /// 获取类型低精度枚举
        /// </summary>
        /// <param name="value">值</param>
        public static TypeLowPrecision GetTypeLowPrecisionEnum(object value)
        {
            if (value == null)
                return TypeLowPrecision.Null;
            return GetTypeHighPrecisionEnum(value).ToLow();
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsValueType(object value)
        {
            if (value == null)
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Value;
        }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsReferenceType(object value)
        {
            if (value == null)
                return false;
            return GetTypeLowPrecisionEnum(value) == TypeLowPrecision.Reference;
        }

        /// <summary>
        /// 是否单类型：包含值类型和String类型
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsSingleType(object value)
        {
            if (value == null)
                return false;
            return IsValueType(value) || GetTypeHighPrecisionEnum(value) == TypeHighPrecision.String;
        }

        #endregion
    }
}