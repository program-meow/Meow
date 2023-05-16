using System;
using System.Collections.Generic;
using Meow.Data.Filter;
using SystemType = System.Type;

namespace Meow.Data.EntityFramework.Filter;

/// <summary>
/// 数据过滤器管理器
/// </summary>
public class FilterManager : IFilterManager
{
    /// <summary>
    /// 同步锁
    /// </summary>
    private static readonly object Sync = new();
    /// <summary>
    /// 过滤器类型列表
    /// </summary>
    private static readonly List<SystemType> _filterTypes = new();
    /// <summary>
    /// 过滤器字典
    /// </summary>
    private readonly Dictionary<SystemType, IFilter> _filters = new();
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 初始化数据过滤器管理器
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public FilterManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    /// 添加过滤器类型
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public static void AddFilterType<TFilterType>()
    {
        SystemType type = typeof(TFilterType);
        lock (Sync)
        {
            if (_filterTypes.Contains(type))
                return;
            _filterTypes.Add(type);
        }
    }

    /// <summary>
    /// 移除过滤器类型
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public static void RemoveFilterType<TFilterType>()
    {
        SystemType type = typeof(TFilterType);
        lock (Sync)
        {
            if (_filterTypes.Contains(type))
                _filterTypes.Remove(type);
        }
    }

    /// <summary>
    /// 启用过滤器
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public void EnableFilter<TFilterType>() where TFilterType : class
    {
        IFilter filter = GetFilter<TFilterType>();
        filter?.Enable();
    }

    /// <summary>
    /// 禁用过滤器
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public IDisposable DisableFilter<TFilterType>() where TFilterType : class
    {
        IFilter filter = GetFilter<TFilterType>();
        return filter?.Disable();
    }

    /// <summary>
    /// 获取过滤器
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public IFilter GetFilter<TFilterType>() where TFilterType : class
    {
        return GetFilter(typeof(TFilterType));
    }

    /// <summary>
    /// 获取过滤器
    /// </summary>
    /// <param name="filterType">过滤器类型</param>
    public IFilter GetFilter(SystemType filterType)
    {
        if (_filters.ContainsKey(filterType) == false)
        {
            SystemType serviceType = typeof(IFilter<>).MakeGenericType(filterType);
            object filter = _serviceProvider.GetService(serviceType);
            _filters.Add(filterType, (IFilter)filter);
        }
        return _filters[filterType];
    }

    /// <summary>
    /// 实体是否启用过滤器
    /// </summary>
    public bool IsEntityEnabled<TEntity>()
    {
        foreach (SystemType type in _filterTypes)
        {
            IFilter filter = GetFilter(type);
            if (filter.IsEntityEnabled<TEntity>())
                return true;
        }
        return false;
    }

    /// <summary>
    /// 过滤器是否启用
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public bool IsEnabled<TFilterType>() where TFilterType : class
    {
        IFilter filter = GetFilter<TFilterType>();
        if (filter == null)
            return false;
        return filter.IsEnabled;
    }
}