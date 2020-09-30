using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// 列表项
    /// </summary>
    public class Item : IComparable<Item>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        public Item(string text, object value, int? sortId = null)
        {
            Text = text;
            Value = value;
            SortId = sortId;
        }

        /// <summary>
        /// 文本
        /// </summary>
        [DisplayName("文本")]
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; }
        /// <summary>
        /// 值
        /// </summary>
        [DisplayName("值")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; }
        /// <summary>
        /// 排序号
        /// </summary>
        [DisplayName("排序号")]
        [JsonProperty("sortId", NullValueHandling = NullValueHandling.Ignore)]
        public int? SortId { get; }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other">其它列表项</param>
        public int CompareTo(Item other)
        {
            return string.Compare(Text, other.Text, StringComparison.CurrentCulture);
        }
    }
}
