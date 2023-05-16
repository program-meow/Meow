using System;
using Meow.Data.Extension;
using Meow.Data.Filter;
using Meow.Model;

namespace Meow.Data.EntityFramework.Extension;

/// <summary>
/// 过滤器操作扩展
/// </summary>
public static class FilterExtension
{
    /// <summary>
    /// 启用逻辑删除过滤器
    /// </summary>
    /// <param name="source">源</param>
    public static void EnableDeleteFilter(this IFilterOperation source)
    {
        source.EnableFilter<IDelete>();
    }

    /// <summary>
    /// 禁用逻辑删除过滤器
    /// </summary>
    /// <param name="source">源</param>
    public static IDisposable DisableDeleteFilter(this IFilterOperation source)
    {
        return source.DisableFilter<IDelete>();
    }
}