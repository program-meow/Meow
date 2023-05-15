﻿using System;
using Meow.Action;
using Meow.Data.Filter;
using Meow.Extension;

namespace Meow.Data.Extension;

/// <summary>
/// 过滤器操作扩展
/// </summary>
public static class FilterExtension
{
    /// <summary>
    /// 启用过滤器
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型,范例: IDelete</typeparam>
    /// <param name="source">源</param>
    public static void EnableFilter<TFilterType>(this IFilterOperation source) where TFilterType : class
    {
        source.CheckNull(nameof(source));
        if (source is IFilterSwitch filterSwitch)
            filterSwitch.EnableFilter<TFilterType>();
    }

    /// <summary>
    /// 禁用过滤器
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型,范例: IDelete</typeparam>
    /// <param name="source">源</param>
    public static IDisposable DisableFilter<TFilterType>(this IFilterOperation source) where TFilterType : class
    {
        source.CheckNull(nameof(source));
        if (source is IFilterSwitch filterSwitch)
            return filterSwitch.DisableFilter<TFilterType>();
        return DisposeAction.Null;
    }
}