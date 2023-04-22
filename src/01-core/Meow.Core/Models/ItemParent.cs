using System;
using System.Text.Json.Serialization;

namespace Meow.Models
{
    /// <summary>
    /// 列表父级项
    /// </summary>
    public class ItemParent : Item, IComparable<ItemParent>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        /// <param name="group">组</param>
        /// <param name="disabled">禁用</param>
        /// <param name="parent">父级</param>
        public ItemParent(string text, object value, int? sortId = null, string group = null, bool? disabled = null, ItemSubset parent = null)
        : base(text, value, sortId, group, disabled)
        {
            Parent = parent;
        }

        /// <summary>
        /// 父级
        /// </summary>
        [JsonPropertyName("parent")]
        public ItemSubset Parent { get; }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other">其它列表项</param>
        public int CompareTo(ItemParent other)
        {
            return base.CompareTo(other);
        }
    }
}
