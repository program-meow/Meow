using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Meow.Models
{
    /// <summary>
    /// 列表子集项
    /// </summary>
    public class ItemSubset : Item, IComparable<ItemSubset>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        /// <param name="group">组</param>
        /// <param name="disabled">禁用</param>
        /// <param name="subsets">子集</param>
        public ItemSubset(string text, object value, int? sortId = null, string group = null, bool? disabled = null, List<ItemSubset> subsets = null)
        : base(text, value, sortId, group, disabled)
        {
            Subsets = subsets ?? new List<ItemSubset>();
        }

        /// <summary>
        /// 子集
        /// </summary>
        [JsonPropertyName("subsets")]
        public List<ItemSubset> Subsets { get; }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other">其它列表项</param>
        public int CompareTo(ItemSubset other)
        {
            return base.CompareTo(other);
        }
    }
}
