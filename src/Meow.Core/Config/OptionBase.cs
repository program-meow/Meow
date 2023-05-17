using System.Collections.Generic;
using Meow.Extension;

namespace Meow.Config;

/// <summary>
/// 配置项基类
/// </summary>
public abstract class OptionBase : IOption
{
    /// <summary>
    /// 初始化配置项
    /// </summary>
    protected OptionBase()
    {
        Extensions = new List<IOptionExtension>();
    }

    /// <summary>
    /// 配置项扩展列表
    /// </summary>
    public List<IOptionExtension> Extensions { get; }

    /// <summary>
    /// 添加配置项扩展
    /// </summary>
    /// <param name="extension">配置项扩展</param>
    public void AddExtension(IOptionExtension extension)
    {
        extension.CheckNull(nameof(extension));
        Extensions.Add(extension);
    }
}