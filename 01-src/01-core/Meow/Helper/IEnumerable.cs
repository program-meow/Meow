﻿using System.Collections.Generic;
using System.Text;

namespace Meow.Helper
{
    /// <summary>
    /// 集合操作
    /// </summary>
    public static class IEnumerable
    {
        /// <summary>
        /// 将集合连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>(IEnumerable<T> value, string quotes = "", string separator = ",")
        {
            if (value == null)
                return string.Empty;
            var result = new StringBuilder();
            foreach (var each in value)
                result.AppendFormat("{0}{1}{0}{2}", quotes, each, separator);
            return separator == ""
                ? result.ToString()
                : result.ToString().TrimEnd(separator.ToCharArray());
        }
    }
}