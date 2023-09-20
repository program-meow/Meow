namespace Meow.Application.Extension;

/// <summary>
/// 业务锁操作扩展
/// </summary>
public static class AppBuilderExtensions {
    /// <summary>
    /// 配置业务锁
    /// </summary>
    /// <param name="builder">应用生成器</param>
    public static IAppBuilder AddLock( this IAppBuilder builder ) {
        builder.CheckNull( nameof( builder ) );
        builder.Host.ConfigureServices( ( context , services ) => {
            services.TryAddTransient<ILock , DefaultLock>();
        } );
        return builder;
    }
}