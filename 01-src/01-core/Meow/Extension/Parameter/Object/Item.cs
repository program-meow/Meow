using System.Linq;
using System.Collections.Generic;
using System.Text;
using Meow.Extension.Helper;
using Meow.Parameter.Object;

namespace Meow.Extension.Parameter.Object
{
    /// <summary>
    /// 列表项扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 有效集合，value存在有效值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<T> Effective<T>(this IEnumerable<T> value) where T : Item
        {
            if (value.IsEmpty())
                return new List<T>();
            return value.Where(t => !t.Value.IsEmpty()).ToList();
        }

        /// <summary>
        /// 默认排序号
        /// </summary>
        /// <typeparam name="T">列表项类型</typeparam>
        /// <param name="value">值</param>
        public static List<T> DefaultSortId<T>(this IEnumerable<T> value) where T : Item
        {
            if (value.IsNull())
                return new List<T>();
            var count = 1;
            foreach (var item in value)
            {
                item.SortId = count;
                count += 1;
            }
            return value.ToList();
        }

        /// <summary>
        /// 连接为字符串
        /// </summary>
        /// <typeparam name="T">列表项类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="connector">连接符，默认"="</param>
        /// <param name="separator">分隔符，默认"&amp;"</param>
        public static string Connector<T>(this IEnumerable<T> value, string connector = "=", string separator = "&") where T : Item
        {
            if (value.IsEmpty())
                return string.Empty;
            var result = new StringBuilder();
            foreach (var item in value.Effective())
                result.AppendFormat("{0}{1}{2}{3}", item.Text, connector, item.Value, separator);
            return separator == ""
                ? result.ToString()
                : result.ToString().TrimEnd(separator.ToCharArray());
        }
    }
}