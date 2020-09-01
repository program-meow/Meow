using System;

namespace Meow.Helper
{
    /// <summary>
    /// 公共操作
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return GetType(typeof(T));
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        public static Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">值</param>
        public static T To<T>(object value)
        {
            if (value == null)
                return default(T);
            if (value is string && string.IsNullOrWhiteSpace(value.ToString()))
                return default(T);
            var type = GetType<T>();
            var typeName = type.Name.ToLower();
            try
            {
                if (typeName == "string")
                    return (T)(object)value.ToString();
                if (typeName == "guid")
                    return (T)(object)new System.Guid(value.ToString());
                if (type.IsEnum)
                    return Enum.Parse<T>(value);
                if (value is IConvertible)
                    return (T)System.Convert.ChangeType(value, type);
                return (T)value;
            }
            catch
            {
                return default(T);
            }
        }
    }
}