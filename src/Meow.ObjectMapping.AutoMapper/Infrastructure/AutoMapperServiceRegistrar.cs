using Meow.Extension;

namespace Meow.ObjectMapping.Infrastructure;

/// <summary>
/// AutoMapper服务注册器
/// </summary>
public class AutoMapperServiceRegistrar : IServiceRegistrar {
    /// <summary>
    /// 获取服务名
    /// </summary>
    public static string ServiceName => "Meow.ObjectMapping.Infrastructure.AutoMapperServiceRegistrar";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderId => 300;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled => ServiceRegistrarConfig.IsEnabled( ServiceName );

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceContext">服务上下文</param>
    public SystemAction Register( ServiceContext serviceContext ) {
        List<SystemType> types = serviceContext.TypeFinder.Find<IAutoMapperConfig>();
        List<IAutoMapperConfig> instances = types.Select( type => Meow.Helper.Reflection.CreateInstance<IAutoMapperConfig>( type ) ).ToList();
        MapperConfigurationExpression expression = new MapperConfigurationExpression();
        instances.ForEach( t => t.Config( expression ) );
        ObjectMapper mapper = new ObjectMapper( expression );
        ObjectMapperExtensions.SetMapper( mapper );
        serviceContext.HostBuilder.ConfigureServices( ( context , services ) => {
            services.AddSingleton<IObjectMapper>( mapper );
        } );
        return null;
    }
}