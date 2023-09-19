using Meow.Data.EntityFrameworkCore.Filter;
using Meow.Domain.Operation;
using Meow.Infrastructure;
using Meow.Tenant;
using SystemAction = System.Action;

namespace Meow.Data.EntityFrameworkCore.Infrastructure;

/// <summary>
/// EntityFrameworkCore服务注册器
/// </summary>
public class EntityFrameworkServiceRegistrar : IServiceRegistrar {
    /// <summary>
    /// 获取服务名
    /// </summary>
    public static string ServiceName => "Meow.Data.EntityFrameworkCore.Infrastructure.EntityFrameworkServiceRegistrar";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderId => 710;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled => ServiceRegistrarConfig.IsEnabled( ServiceName );

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceContext">服务上下文</param>
    public SystemAction Register( ServiceContext serviceContext ) {
        FilterManager.AddFilterType<IDelete>();
        FilterManager.AddFilterType<ITenant>();
        return null;
    }
}