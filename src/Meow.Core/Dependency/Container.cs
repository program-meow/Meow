﻿namespace Meow.Dependency;

/// <summary>
/// 容器
/// </summary>
public class Container {
    /// <summary>
    /// 服务集合
    /// </summary>
    private readonly ServiceCollection _services;
    /// <summary>
    /// 服务提供器
    /// </summary>
    private IServiceProvider _provider;

    /// <summary>
    /// 容器实例
    /// </summary>
    public static readonly Container Instance = new();

    /// <summary>
    /// 初始化容器
    /// </summary>
    public Container() {
        _services = new ServiceCollection();
    }

    /// <summary>
    /// 获取服务集合
    /// </summary>
    public ServiceCollection GetServices() {
        return _services;
    }

    /// <summary>
    /// 获取服务提供器
    /// </summary>
    public IServiceProvider GetServiceProvider() {
        return _provider ??= _services.BuildServiceProvider();
    }

    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public T GetService<T>() {
        return GetService<T>( typeof( T ) );
    }

    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="T">返回对象类型</typeparam>
    /// <param name="type">对象类型</param>
    public T GetService<T>( SystemType type ) {
        object service = GetService( type );
        if( service == null )
            return default;
        return ( T ) service;
    }

    /// <summary>
    /// 获取服务
    /// </summary>
    /// <param name="type">对象类型</param>
    public object GetService( SystemType type ) {
        IServiceProvider provider = GetServiceProvider();
        return provider.GetService( type );
    }

    /// <summary>
    /// 清理
    /// </summary>
    public void Clear() {
        _services.Clear();
        _provider = null;
    }
}