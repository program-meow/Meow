﻿namespace Meow.Aop;

/// <summary>
/// Aop配置扩展
/// </summary>
public static class AppBuilderExtensions {
    /// <summary>
    /// 启用AspectCore拦截器
    /// </summary>
    /// <param name="builder">应用生成器</param>
    public static IAppBuilder AddAop( this IAppBuilder builder ) {
        return builder.AddAop( false );
    }

    /// <summary>
    /// 启用AspectCore拦截器
    /// </summary>
    /// <param name="builder">应用生成器</param>
    /// <param name="isEnableIAopProxy">是否启用IAopProxy接口标记</param>
    public static IAppBuilder AddAop( this IAppBuilder builder , bool isEnableIAopProxy ) {
        return builder.AddAop( null , isEnableIAopProxy );
    }

    /// <summary>
    /// 启用AspectCore拦截器
    /// </summary>
    /// <param name="builder">应用生成器</param>
    /// <param name="setupAction">AspectCore拦截器配置操作</param>
    /// <param name="isEnableIAopProxy">是否启用IAopProxy接口标记</param>
    public static IAppBuilder AddAop( this IAppBuilder builder , Action<IAspectConfiguration> setupAction , bool isEnableIAopProxy ) {
        builder.CheckNull( nameof( builder ) );
        builder.Host.UseServiceProviderFactory( new DynamicProxyServiceProviderFactory() );
        builder.Host.ConfigureServices( ( context , services ) => {
            ConfigureDynamicProxy( services , setupAction , isEnableIAopProxy );
            RegisterAspectScoped( services );
        } );
        return builder;
    }

    /// <summary>
    /// 配置拦截器
    /// </summary>
    private static void ConfigureDynamicProxy( IServiceCollection services , Action<IAspectConfiguration> setupAction , bool isEnableIAopProxy ) {
        services.ConfigureDynamicProxy( config => {
            if( setupAction == null ) {
                config.NonAspectPredicates.Add( t => !IsProxy( t.DeclaringType , isEnableIAopProxy ) );
                config.EnableParameterAspect();
                return;
            }
            setupAction.Invoke( config );
        } );
    }

    /// <summary>
    /// 是否创建代理
    /// </summary>
    private static bool IsProxy( SystemType type , bool isEnableIAopProxy ) {
        if( type == null )
            return false;
        if( isEnableIAopProxy == false ) {
            if( Meow.Helper.Reflection.GetTopBaseType( type ).SafeString() == "Microsoft.EntityFrameworkCore.DbContext" )
                return false;
            if( type.SafeString().Contains( "Xunit.DependencyInjection.ITestOutputHelperAccessor" ) )
                return false;
            return true;
        }
        SystemType[] interfaces = type.GetInterfaces();
        if( interfaces == null || interfaces.Length == 0 )
            return false;
        foreach( SystemType item in interfaces ) {
            if( item == typeof( IAopProxy ) )
                return true;
        }
        return false;
    }

    /// <summary>
    /// 注册拦截作用域
    /// </summary>
    private static void RegisterAspectScoped( IServiceCollection services ) {
        services.AddScoped<IAspectScheduler , ScopeAspectScheduler>();
        services.AddScoped<IAspectBuilderFactory , ScopeAspectBuilderFactory>();
        services.AddScoped<IAspectContextFactory , ScopeAspectContextFactory>();
    }
}