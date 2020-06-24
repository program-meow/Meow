using System;
using System.Collections.Generic;
using System.Linq;
using Meow.Extensions.Validates;

namespace Meow.Extensions.Helpers
{
    /// <summary>
    /// 集合类型扩展
    /// </summary>
    public static partial class Extensions
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
            return Meow.Helpers.String.Join(list, quotes, separator);
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
            return list.Select(item => (T?)item).ToList();
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static List<T> AddNoNull<T>(this IEnumerable<T> list, T data)
        {
            if (list == null)
                return new List<T>();
            var source = list.ToList();
            if (data.IsNull())
                return source;
            source.Add(data);
            return source;
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static List<string> AddNoEmpty(this IEnumerable<string> list, string data)
        {
            if (list == null)
                return new List<string>();
            var source = list.ToList();
            if (data.IsEmpty())
                return source;
            source.Add(data);
            return source;
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static List<Guid> AddNoEmpty(this IEnumerable<Guid> list, Guid data)
        {
            if (list == null)
                return new List<Guid>();
            var source = list.ToList();
            if (data.IsEmpty())
                return source;
            source.Add(data);
            return source;
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static List<Guid?> AddNoEmpty(this IEnumerable<Guid?> list, Guid? data)
        {
            if (list == null)
                return new List<Guid?>();
            var source = list.ToList();
            if (data.IsEmpty())
                return source;
            source.Add(data);
            return source;
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
    }
}
