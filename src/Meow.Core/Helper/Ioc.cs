using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemType = System.Type;

namespace Meow.Helper;

/// <summary>
/// 容器操作
/// </summary>
public static class Ioc
{
    /// <summary>
    /// 容器
    /// </summary>
    private static readonly Meow.Dependency.Container _container = Meow.Dependency.Container.Instance;
    /// <summary>
    /// 获取服务提供器操作
    /// </summary>
    private static Func<IServiceProvider> _getServiceProviderAction;

    /// <summary>
    /// 服务范围工厂
    /// </summary>
    public static IServiceScopeFactory ServiceScopeFactory { get; set; }

    /// <summary>
    /// 获取服务集合
    /// </summary>
    public static IServiceCollection GetServices()
    {
        return _container.GetServices();
    }

    /// <summary>
    /// 设置获取服务提供器操作
    /// </summary>
    /// <param name="action">获取服务提供器操作</param>
    public static void SetServiceProviderAction(Func<IServiceProvider> action)
    {
        _getServiceProviderAction = action;
    }

    /// <summary>
    /// 获取
    /// </summary>
    private static IServiceProvider GetServiceProvider()
    {
        IServiceProvider provider = _getServiceProviderAction?.Invoke();
        if (provider != null)
            return provider;
        return _container.GetServiceProvider();
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public static T Create<T>()
    {
        return Create<T>(typeof(T));
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <typeparam name="T">返回对象类型</typeparam>
    /// <param name="type">对象类型</param>
    public static T Create<T>(SystemType type)
    {
        object service = Create(type);
        if (service == null)
            return default;
        return (T)service;
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <param name="type">对象类型</param>
    public static object Create(SystemType type)
    {
        if (type == null)
            return null;
        IServiceProvider provider = GetServiceProvider();
        return provider.GetService(type);
    }

    /// <summary>
    /// 创建对象集合
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    public static List<T> CreateList<T>()
    {
        return CreateList<T>(typeof(T));
    }

    /// <summary>
    /// 创建对象集合
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="type">对象类型</param>
    public static List<T> CreateList<T>(SystemType type)
    {
        SystemType serviceType = typeof(IEnumerable<>).MakeGenericType(type);
        object result = Create(serviceType);
        if (result == null)
            return new List<T>();
        return ((IEnumerable<T>)result).ToList();
    }

    /// <summary>
    /// 创建服务范围
    /// </summary>
    public static IServiceScope CreateScope()
    {
        IServiceProvider provider = GetServiceProvider();
        return provider.CreateScope();
    }
}