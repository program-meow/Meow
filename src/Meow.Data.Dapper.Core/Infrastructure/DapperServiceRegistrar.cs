using Meow.Data.Dapper.TypeHandler;
using Meow.Infrastructure;

namespace Meow.Data.Dapper.Infrastructure;

/// <summary>
/// Dapper服务注册器
/// </summary>
public class DapperServiceRegistrar : IServiceRegistrar {
    /// <summary>
    /// 获取服务名
    /// </summary>
    public static string ServiceName => "Meow.Data.Dapper.Infrastructure.DapperServiceRegistrar";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderId => 810;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled => ServiceRegistrarConfig.IsEnabled( ServiceName );

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceContext">服务上下文</param>
    public System.Action Register( ServiceContext serviceContext ) {
        SqlMapper.AddTypeHandler( new ExtraPropertiesTypeHandler() );
        return null;
    }
}