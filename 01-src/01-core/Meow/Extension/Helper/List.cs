using System;
using System.Collections.Generic;
using Meow.Extension.Validate;

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
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static void AddNoNull<T>(this List<T> list, T data)
        {
            if (list == null)
                return;
            if (data.IsNull())
                return;
            list.Add(data);
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static void AddNoEmpty(this List<string> list, string data)
        {
            if (list == null)
                return;
            if (data.IsEmpty())
                return;
            list.Add(data);
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static void AddNoEmpty(this List<Guid> list, Guid data)
        {
            if (list == null)
                return;
            if (data.IsEmpty())
                return;
            list.Add(data);
        }

        /// <summary>
        /// 添加不为空数据
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="data">添加数据</param>
        public static void AddNoEmpty(this List<Guid?> list, Guid? data)
        {
            if (list == null)
                return;
            if (data.IsEmpty())
                return;
            list.Add(data);
        }
    }
}
