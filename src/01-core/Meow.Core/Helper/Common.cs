using System;
using System.Collections.Generic;
using System.Linq;

namespace Meow.Helper
{
    /// <summary>
    /// 公共操作
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="value">值</param>
        public static string SafeString(object value)
        {
            return value?.ToString()?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        public static string SafeValue(string value)
        {
            return value ?? string.Empty;
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="array">集合</param>
        public static List<string> SafeValue(IEnumerable<string> array)
        {
            if (array == null)
                return new List<string>();
            return array.Select(SafeValue).ToList();
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(T? value) where T : struct
        {
            return value ?? default;
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="array">集合</param>
        public static List<T> SafeValue<T>(IEnumerable<T?> array) where T : struct
        {
            if (array == null)
                return new List<T>();
            return array.Select(SafeValue).ToList();
        }

        /// <summary>
        /// 转换可空集合
        /// </summary>
        /// <param name="array">集合</param>
        public static List<T?> ToOrNull<T>(IEnumerable<T> array) where T : struct
        {
            if (array == null)
                return new List<T?>();
            return array.Select(t => (T?)t).ToList();
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(T value) where T : new()
        {
            return value ?? new T();
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static System.Type GetType<T>()
        {
            return GetType(typeof(T));
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        public static System.Type GetType(System.Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

    }
}
