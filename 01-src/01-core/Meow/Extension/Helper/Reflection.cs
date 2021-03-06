﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Meow.Parameter.Object;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 反射扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取实例上的属性值
        /// </summary>
        /// <param name="member">成员信息</param>
        /// <param name="instance">成员所在的类实例</param>
        public static object GetPropertyValue(this MemberInfo member, object instance)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            return instance.GetType().GetProperty(member.Name)?.GetValue(instance);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T">对象元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<ItemObjectTree> Analyzing<T>(this T value) where T : class
        {
            return Meow.Helper.Reflection.Analyzing(value);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<ItemObjectTree> Analyzing<T>(this IEnumerable<T> value) where T : class
        {
            return Meow.Helper.Reflection.Analyzing(value);
        }

        /// <summary>
        /// 解析对象到列表集合
        /// </summary>
        /// <typeparam name="T">对象元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<Item> AnalyzingToItems<T>(this T value) where T : class
        {
            return Meow.Helper.Reflection.AnalyzingToItems(value);
        }

        /// <summary>
        /// 解析集合对象到列表集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<Item> AnalyzingToItems<T>(this IEnumerable<T> value) where T : class
        {
            return Meow.Helper.Reflection.AnalyzingToItems(value);
        }
    }
}