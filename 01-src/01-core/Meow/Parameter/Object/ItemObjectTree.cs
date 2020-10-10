using System.Collections.Generic;
using System.ComponentModel;
using Meow.Parameter.Enum;
using Newtonsoft.Json;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// 列表项对象解析树
    /// </summary>
    public class ItemObjectTree : ItemTree<ItemObjectTree>
    {
        /// <summary>
        /// 初始化列表项树
        /// </summary>
        public ItemObjectTree() : this(null, null)
        {
        }

        /// <summary>
        /// 初始化列表项树
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="type">类型</param>
        public ItemObjectTree(string text, TypeHighPrecision? type) : this(text, type, null)
        {
        }

        /// <summary>
        /// 初始化列表项树
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        public ItemObjectTree(string text, TypeHighPrecision? type, object value, int? sortId = 1) : this(text, type, value, new List<ItemObjectTree>(), sortId)
        {
        }

        /// <summary>
        /// 初始化列表项树
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <param name="subsets">子集集合</param>
        /// <param name="sortId">排序号</param>
        public ItemObjectTree(string text, TypeHighPrecision? type, object value, IEnumerable<ItemObjectTree> subsets, int? sortId = 1) : base(text, value, subsets, sortId)
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        [JsonProperty("sortId", NullValueHandling = NullValueHandling.Ignore)]
        public TypeHighPrecision? Type { get; set; }
    }
}