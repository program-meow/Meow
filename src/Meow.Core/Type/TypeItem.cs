using System.Collections.Generic;
using Meow.Model;

namespace Meow.Type;

/// <summary>
/// 类型项
/// </summary>
public class TypeItem : Item<object, List<TypeItem>>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="type">类型</param>
    /// <param name="value">值</param>
    /// <param name="subsets">子集项</param>
    public TypeItem(string name, TypeEnum? type, object value, List<TypeItem> subsets = default(List<TypeItem>))
        : base(name, value, null, null, null, subsets)
    {
        Type = type;
    }

    /// <summary>
    /// 类型
    /// </summary>
    public TypeEnum? Type { get; set; }

    /// <summary>
    /// 默认
    /// </summary>
    public static TypeItem Default = new TypeItem(null, null, null);
}