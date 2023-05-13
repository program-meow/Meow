using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Meow.Model;

/// <summary>
/// 列表父级项
/// </summary>
public class ItemParent : Item<object, ItemParent>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="value">值</param>
    /// <param name="sortId">排序号</param>
    /// <param name="group">组</param>
    /// <param name="disabled">禁用</param>
    /// <param name="parent">父级项</param>
    public ItemParent(string text, object value, int? sortId = null, string group = null, bool? disabled = null, ItemParent parent = default(ItemParent))
        : base(text, value, sortId, group, disabled, parent)
    {
    }
}

/// <summary>
/// 列表子集项
/// </summary>
public class ItemSubset : Item<object, List<ItemSubset>>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="value">值</param>
    /// <param name="sortId">排序号</param>
    /// <param name="group">组</param>
    /// <param name="disabled">禁用</param>
    /// <param name="subsets">子集项</param>
    public ItemSubset(string text, object value, int? sortId = null, string group = null, bool? disabled = null, List<ItemSubset> subsets = default(List<ItemSubset>))
        : base(text, value, sortId, group, disabled, subsets)
    {
    }
}

/// <summary>
/// 列表项
/// </summary>
public class Item : Item<object, object>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="value">值</param>
    /// <param name="sortId">排序号</param>
    /// <param name="group">组</param>
    /// <param name="disabled">禁用</param>
    /// <param name="data">数据</param>
    public Item(string text, object value, int? sortId = null, string group = null, bool? disabled = null, object data = default(object))
        : base(text, value, sortId, group, disabled, data)
    {
    }
}

/// <summary>
/// 列表项
/// </summary>
public class Item<TValue, TData> : IComparable<Item<TValue, TData>>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="value">值</param>
    /// <param name="sortId">排序号</param>
    /// <param name="group">组</param>
    /// <param name="disabled">禁用</param>
    /// <param name="data">数据</param>
    public Item(string text, TValue value, int? sortId = null, string group = null, bool? disabled = null, TData data = default(TData))
    {
        Text = text;
        Value = value;
        SortId = sortId;
        Group = group;
        Disabled = disabled;
        Data = data;
    }

    /// <summary>
    /// 文本
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; }

    /// <summary>
    /// 值
    /// </summary>
    [JsonPropertyName("value")]
    public object Value { get; }

    /// <summary>
    /// 排序号
    /// </summary>
    [JsonPropertyName("sortId")]
    public int? SortId { get; }

    /// <summary>
    /// 组
    /// </summary>
    [JsonPropertyName("group")]
    public string Group { get; }

    /// <summary>
    /// 禁用
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; }

    /// <summary>
    /// 数据
    /// </summary>
    [JsonPropertyName("data")]
    public TData Data { get; }

    /// <summary>
    /// 比较
    /// </summary>
    /// <param name="other">其它列表项</param>
    public int CompareTo(Item<TValue, TData> other)
    {
        return string.Compare(Text, other?.Text, StringComparison.CurrentCulture);
    }
}