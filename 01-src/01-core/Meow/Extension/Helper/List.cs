using System;
using System.Collections.Generic;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 集合类型扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="data">添加数据</param>
        public static void AddNoNull<T>(this List<T> value, T data)
        {
            if (value.IsNull())
                value = new List<T>();
            if (data.IsNull())
                return;
            value.Add(data);
        }

        /// <summary>
        /// 添加不为空数据集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="list">添加数据集合</param>
        public static void AddNoNull<T>(this List<T> value, IEnumerable<T> list)
        {
            if (value.IsNull())
                value = new List<T>();
            if (list.IsNull())
                return;
            foreach (var item in list)
                value.AddNoNull(item);
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="data">添加数据</param>
        public static void AddNoEmpty(this List<string> value, string data)
        {
            if (value.IsNull())
                value = new List<string>();
            if (data.IsEmpty())
                return;
            value.Add(data);
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="data">添加数据</param>
        public static void AddNoEmpty(this List<Guid> value, Guid? data)
        {
            if (value.IsNull())
                value = new List<Guid>();
            if (data.IsEmpty())
                return;
            value.Add(data.SafeValue());
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="data">添加数据</param>
        public static void AddNoEmpty(this List<Guid?> value, Guid? data)
        {
            if (value.IsNull())
                value = new List<Guid?>();
            if (data.IsEmpty())
                return;
            value.Add(data);
        }
    }
}