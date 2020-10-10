using System.Linq;
using System.Collections.Generic;
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
    }
}