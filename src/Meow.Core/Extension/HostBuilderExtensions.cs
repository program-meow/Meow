using Meow.Config;
using Meow.Infrastructure;

namespace Meow.Extension;

/// <summary>
/// 主机生成器服务扩展
/// </summary>
public static class HostBuilderExtensions {
    /// <summary>
    /// 注册Meow前置服务,用于配置具有延迟的服务
    /// </summary>
    /// <param name="hostBuilder">主机生成器</param>
    /// <param name="setupAction">服务配置操作</param>
    public static IHostBuilder AddMeowBefore( this IHostBuilder hostBuilder , Action<PreOptions> setupAction = null ) {
        hostBuilder.CheckNull( nameof( hostBuilder ) );
        hostBuilder.ConfigureServices( ( context , services ) => {
            Helper.Config.SetConfiguration( context.Configuration );
        } );
        hostBuilder.AddOptions( setupAction );
        return hostBuilder;
    }

    /// <summary>
    /// 注册Meow服务 
    /// </summary>
    /// <param name="hostBuilder">主机生成器</param>
    /// <param name="setupAction">服务配置操作</param>
    public static IHostBuilder AddMeow( this IHostBuilder hostBuilder , Action<Options> setupAction = null ) {
        hostBuilder.CheckNull( nameof( hostBuilder ) );
        Bootstrapper bootstrapper = new Bootstrapper( hostBuilder , setupAction );
        bootstrapper.Start();
        return hostBuilder;
    }

    /// <summary>
    /// 注册Meow配置操作
    /// </summary>
    /// <param name="hostBuilder">主机生成器</param>
    /// <param name="setupAction">服务配置操作</param>
    public static IHostBuilder AddOptions<TOptions>( this IHostBuilder hostBuilder , Action<TOptions> setupAction = null ) where TOptions : class, IOptions, new() {
        hostBuilder.CheckNull( nameof( hostBuilder ) );
        TOptions options = new TOptions();
        setupAction?.Invoke( options );
        hostBuilder.ConfigureServices( ( context , services ) => {
            if( setupAction != null )
                services.Configure( setupAction );
        } );
        foreach( var extension in options.Extensions ) {
            extension.Config( hostBuilder );
            hostBuilder.ConfigureAppConfiguration( extension.ConfigureAppConfiguration );
            hostBuilder.ConfigureServices( extension.ConfigureServices );
        }
        return hostBuilder;
    }
}