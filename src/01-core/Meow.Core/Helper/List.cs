using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meow.Helper
{
    /// <summary>
    /// 集合操作
    /// </summary>
    public static class List
    {
        /// <summary>
        /// 驼峰式命
        /// </summary>
        /// <param name="array">集合</param>
        /// <returns>驼峰形式字符串</returns>
        public static string CamelCase(IEnumerable<string> array)
        {
            if (array == null)
                return string.Empty;
            StringBuilder result = new StringBuilder();
            foreach (string each in array)
                result.Append(String.FirstUpperCase(each.ToLower()));
            return result.ToString();
        }

        /// <summary>
        /// 将集合连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>(IEnumerable<T> array, string quotes = "", string separator = ",")
        {
            if (array == null)
                return string.Empty;
            StringBuilder result = new StringBuilder();
            foreach (T each in array)
                result.AppendFormat("{0}{1}{0}{2}", quotes, each, separator);
            return String.RemoveEnd(result.ToString(), separator);
        }

        /// <summary>
        /// 将集合连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<TKey, TValue>(Dictionary<TKey, TValue> array, string quotes = "", string separator = ",")
        {
            if (array == null)
                return string.Empty;
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<TKey, TValue> each in array)
                result.AppendFormat("{0}{1}{0}{2}", quotes, each.Value, separator);
            return String.RemoveEnd(result.ToString(), separator);
        }

        /// <summary>
        /// 转换为小写字符串集合
        /// </summary>
        /// <param name="array">集合</param>
        /// <returns>小写字符串集合</returns>
        public static List<string> ToLower(IEnumerable<string> array)
        {
            if (array == null)
                return new List<string>();
            return array.Select(t => t.ToLower()).ToList();
        }

        /// <summary>
        /// 转换为大写字符串集合
        /// </summary>
        /// <param name="array">集合</param>
        /// <returns>大写字符串集合</returns>
        public static List<string> ToUpper(IEnumerable<string> array)
        {
            if (array == null)
                return new List<string>();
            return array.Select(t => t.ToUpper()).ToList();
        }

        #region Add 和 AddRange 扩展

        /// <summary>
        /// 添加不为null的值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<T> AddNotNull<T>(List<T> array, T value)
        {
            array ??= new List<T>();
            if (value == null)
                return array;
            array.Add(value);
            return array;
        }

        /// <summary>
        /// 添加不为null的值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<T> AddRangeNotNull<T>(List<T> array, List<T> value)
        {
            array ??= new List<T>();
            if (value == null)
                return array;
            array.AddRange(value.Where(t => t != null));
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<string> AddNotEmpty(List<string> array, string value)
        {
            array ??= new List<string>();
            if (Validation.IsEmpty(value))
                return array;
            array.Add(value);
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<string> AddRangeNotEmpty(List<string> array, List<string> value)
        {
            array ??= new List<string>();
            if (value == null)
                return array;
            array.AddRange(value.Where(t => !Validation.IsEmpty(t)));
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<Guid> AddNotEmpty(List<Guid> array, Guid value)
        {
            array ??= new List<Guid>();
            if (Validation.IsEmpty(value))
                return array;
            array.Add(value);
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<Guid> AddRangeNotEmpty(List<Guid> array, List<Guid> value)
        {
            array ??= new List<Guid>();
            if (value == null)
                return array;
            array.AddRange(value.Where(t => !Validation.IsEmpty(t)));
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<Guid?> AddNotEmpty(List<Guid?> array, Guid? value)
        {
            array ??= new List<Guid?>();
            if (Validation.IsEmpty(value))
                return array;
            array.Add(value);
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<Guid?> AddRangeNotEmpty(List<Guid?> array, List<Guid?> value)
        {
            array ??= new List<Guid?>();
            if (value == null)
                return array;
            array.AddRange(value.Where(t => !Validation.IsEmpty(t)));
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<DateTime> AddNotEmpty(List<DateTime> array, DateTime value)
        {
            array ??= new List<DateTime>();
            if (Validation.IsEmpty(value))
                return array;
            array.Add(value);
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<DateTime> AddRangeNotEmpty(List<DateTime> array, List<DateTime> value)
        {
            array ??= new List<DateTime>();
            if (value == null)
                return array;
            array.AddRange(value.Where(t => !Validation.IsEmpty(t)));
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<DateTime?> AddNotEmpty(List<DateTime?> array, DateTime? value)
        {
            array ??= new List<DateTime?>();
            if (Validation.IsEmpty(value))
                return array;
            array.Add(value);
            return array;
        }

        /// <summary>
        /// 添加有效值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static List<DateTime?> AddRangeNotEmpty(List<DateTime?> array, List<DateTime?> value)
        {
            array ??= new List<DateTime?>();
            if (value == null)
                return array;
            array.AddRange(value.Where(t => !Validation.IsEmpty(t)));
            return array;
        }

        #endregion

        /// <summary>
        /// 是否不间断连续 判断类型是Int
        /// </summary>
        /// <typeparam name="TSource">集合元素类型</typeparam>
        /// <typeparam name="TKey">键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        /// <param name="startNo">起始数</param>
        public static bool IsSequenceBy<TSource, TKey>(IEnumerable<TSource> array, Func<TSource, TKey> keySelector, int startNo = 1)
        {
            if (Validation.IsEmpty(array))
                return false;
            var compare = new List<int?>();
            for (var i = 0; i < array.Count(); i++)
                compare.Add(startNo + i);
            foreach (TSource element in array)
            {
                var value = Convert.ToIntOrNull(keySelector(element));
                if (value == null)
                    return false;
                var compareValue = compare.FirstOrDefault(t => t == value);
                if (compareValue == null)
                    return false;
                compare.Remove(compareValue);
            }
            return true;
        }

        /// <summary>
        /// 是否不间断连续
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="startNo">起始数</param>
        public static bool IsSequence(IEnumerable<int?> array, int startNo = 1)
        {
            return IsSequence(Common.SafeValue(array), startNo);
        }

        /// <summary>
        /// 是否不间断连续
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="startNo">起始数</param>
        public static bool IsSequence(IEnumerable<int> array, int startNo = 1)
        {
            if (Validation.IsEmpty(array))
                return false;
            var compare = new List<int?>();
            for (var i = 0; i < array.Count(); i++)
                compare.Add(startNo + i);
            foreach (int each in array)
            {
                var compareValue = compare.FirstOrDefault(t => t == each);
                if (compareValue == null)
                    return false;
                compare.Remove(compareValue);
            }
            return true;
        }

        #region Remove  扩展

        /// <summary>
        /// 移除null值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        public static List<T> RemoveNull<T>(List<T> array)
        {
            array ??= new List<T>();
            array.RemoveAll(t => t == null);
            return array;
        }

        /// <summary>
        /// 根据条件移除null值
        /// </summary>
        /// <typeparam name="TSource">集合元素类型</typeparam>
        /// <typeparam name="TKey">键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        public static List<TSource> RemoveNullBy<TSource, TKey>(List<TSource> array, Func<TSource, TKey> keySelector)
        {
            array ??= new List<TSource>();
            array = array.Where(t => keySelector(t) == null).ToList();
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <param name="array">集合</param>
        public static List<string> RemoveEmpty(List<string> array)
        {
            array ??= new List<string>();
            array.RemoveAll(Validation.IsEmpty);
            return array;
        }

        /// <summary>
        /// 根据条件移除空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        public static List<T> RemoveEmptyBy<T>(List<T> array, Func<T, string> keySelector)
        {
            array ??= new List<T>();
            array = array.Where(t => !Validation.IsEmpty(keySelector(t))).ToList();
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <param name="array">集合</param>
        public static List<Guid> RemoveEmpty(List<Guid> array)
        {
            array ??= new List<Guid>();
            array.RemoveAll(Validation.IsEmpty);
            return array;
        }

        /// <summary>
        /// 根据条件移除空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        public static List<T> RemoveEmptyBy<T>(List<T> array, Func<T, Guid> keySelector)
        {
            array ??= new List<T>();
            array = array.Where(t => !Validation.IsEmpty(keySelector(t))).ToList();
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <param name="array">集合</param>
        public static List<Guid?> RemoveEmpty(List<Guid?> array)
        {
            array ??= new List<Guid?>();
            array.RemoveAll(Validation.IsEmpty);
            return array;
        }

        /// <summary>
        /// 根据条件移除空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        public static List<T> RemoveEmptyBy<T>(List<T> array, Func<T, Guid?> keySelector)
        {
            array ??= new List<T>();
            array = array.Where(t => !Validation.IsEmpty(keySelector(t))).ToList();
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <param name="array">集合</param>
        public static List<DateTime> RemoveEmpty(List<DateTime> array)
        {
            array ??= new List<DateTime>();
            array.RemoveAll(Validation.IsEmpty);
            return array;
        }

        /// <summary>
        /// 根据条件移除空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        public static List<T> RemoveEmptyBy<T>(List<T> array, Func<T, DateTime> keySelector)
        {
            array ??= new List<T>();
            array = array.Where(t => !Validation.IsEmpty(keySelector(t))).ToList();
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <param name="array">集合</param>
        public static List<DateTime?> RemoveEmpty(List<DateTime?> array)
        {
            array ??= new List<DateTime?>();
            array.RemoveAll(Validation.IsEmpty);
            return array;
        }

        /// <summary>
        /// 根据条件移除空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        public static List<T> RemoveEmptyBy<T>(List<T> array, Func<T, DateTime?> keySelector)
        {
            array ??= new List<T>();
            array = array.Where(t => !Validation.IsEmpty(keySelector(t))).ToList();
            return array;
        }

        #endregion


    }
}
