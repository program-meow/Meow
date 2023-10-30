using Meow.Json.Converter;
using Meow.Helper;
using Meow.Http;

namespace Meow.Infrastructure;

/// <summary>
/// Http服务注册器
/// </summary>
public class HttpServiceRegistrar : IServiceRegistrar {
    /// <summary>
    /// 获取服务名
    /// </summary>
    public static string ServiceName => "Meow.Infrastructure.HttpServiceRegistrar";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderId => 200;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled => ServiceRegistrarConfig.IsEnabled( ServiceName );

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceContext">服务上下文</param>
    public SystemAction Register( ServiceContext serviceContext ) {
        serviceContext.HostBuilder.ConfigureServices( ( context , services ) => {
            RegisterHttpContextAccessor( services );
            services.AddHttpClient();
            RegisterServiceLocator();
            RegisterHttpClient( services );
            ConfigJsonOptions( services );
        } );
        return null;
    }

    /// <summary>
    /// 注册Http上下文访问器
    /// </summary>
    private void RegisterHttpContextAccessor( IServiceCollection services ) {
        HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        services.TryAddSingleton<IHttpContextAccessor>( httpContextAccessor );
        Meow.Helper.Web.HttpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 注册服务定位器
    /// </summary>
    private void RegisterServiceLocator() {
        Ioc.SetServiceProviderAction( () => Meow.Helper.Web.ServiceProvider );
    }

    /// <summary>
    /// 注册Http客户端
    /// </summary>
    private void RegisterHttpClient( IServiceCollection services ) {
        services.TryAddSingleton<IHttpClient , HttpClientService>();
    }

    /// <summary>
    /// 配置Json
    /// </summary>
    private void ConfigJsonOptions( IServiceCollection services ) {
        services.Configure( ( Microsoft.AspNetCore.Mvc.JsonOptions options ) => {
            options.JsonSerializerOptions.Converters.Add( new DateTimeJsonConverter() );
            options.JsonSerializerOptions.Converters.Add( new NullableDateTimeJsonConverter() );
        } );
    }
}