using Meow.Config;
using Meow.Infrastructure;

namespace Meow.Extension;

/// <summary>
/// 主机生成器服务扩展
/// </summary>
public static class IHostBuilderExtensions {
    /// <summary>
    /// 注册Meow服务 
    /// </summary>
    /// <param name="hostBuilder">主机生成器</param>
    public static IHostBuilder AddMeow( this IHostBuilder hostBuilder) {
        hostBuilder.CheckNull( nameof( hostBuilder ) );
        Bootstrapper bootstrapper = new Bootstrapper( hostBuilder);
        bootstrapper.Start();
        return hostBuilder;
    }

    /// <summary>
    /// 转换为Meow应用生成器
    /// </summary>
    /// <param name="hostBuilder">主机生成器</param>
    public static IAppBuilder AsBuild( this IHostBuilder hostBuilder ) {
        hostBuilder.CheckNull( nameof( hostBuilder ) );
        return new AppBuilder( hostBuilder );
    }

    /// <summary>
    /// 启动Meow服务 
    /// </summary>
    /// <param name="appBuilder">应用生成器</param>
    public static IAppBuilder AddMeow( this IAppBuilder appBuilder ) {
        appBuilder.CheckNull( nameof( appBuilder ) );
        Bootstrapper bootstrapper = new Bootstrapper( appBuilder.Host );
        bootstrapper.Start();
        return appBuilder;
    }
}