using System.Collections.Generic;

namespace Meow.Config;

/// <summary>
/// 配置项
/// </summary>
public interface IOption
{
    /// <summary>
    /// 配置项扩展列表
    /// </summary>
    List<IOptionExtension> Extensions { get; }
    /// <summary>
    /// 添加配置项扩展
    /// </summary>
    /// <param name="extension">配置项扩展</param>
    void AddExtension(IOptionExtension extension);
}