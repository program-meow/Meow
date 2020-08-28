using System;
using System.Collections.Generic;
using System.Linq;
using Meow.Extension.Validate;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 集合类型扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换为用分隔符连接的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>(this IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            return Meow.Helper.String.Join(list, quotes, separator);
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        public static List<T> SafeValue<T>(this IEnumerable<T> list)
        {
            return list == null
                 ? new List<T>()
                 : list.ToList();
        }

        /// <summary>
        /// 转换为不可空值，当项值为null时，则排除
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        public static List<T> ToNotNull<T>(this IEnumerable<T?> list) where T : struct
        {
            if (list == null)
                return new List<T>();
            return list
                  .Where(t => t != null)
                  .Select(t => t.SafeValue())
                  .ToList();
        }

        /// <summary>
        /// 转换为可空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        public static List<T?> ToOrNull<T>(this IEnumerable<T> list) where T : struct
        {
            if (list == null)
                return new List<T?>();
            return list.Select(item => (T?)item).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        public static List<T> FilterNull<T>(this IEnumerable<T> list)
        {
            return list == null
                 ? new List<T>()
                 : list.Where(t => t != null).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <param name="list">集合</param>
        public static List<string> FilterEmpty(this IEnumerable<string> list)
        {
            return list == null
                 ? new List<string>()
                 : list.Where(t => !t.IsEmpty()).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <param name="list">集合</param>
        public static List<Guid> FilterEmpty(this IEnumerable<Guid> list)
        {
            return list == null
                 ? new List<Guid>()
                 : list.Where(t => !t.IsEmpty()).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <param name="list">集合</param>
        public static List<Guid?> FilterEmpty(this IEnumerable<Guid?> list)
        {
            return list == null
                 ? new List<Guid?>()
                 : list.Where(t => !t.IsEmpty()).ToList();
        }

        /// <summary>
        /// 过滤重复值
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <typeparam name="TKey">标识类型</typeparam>
        /// <param name="source">源数据</param>
        /// <param name="keySelector">委托方法</param>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
