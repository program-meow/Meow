using Meow.Dependency;
using Meow.Reflection;

namespace Meow.Infrastructure;

/// <summary>
/// 依赖服务注册器 - 用于扫描注册ISingletonDependency,IScopeDependency,ITransientDependency
/// </summary>
public class DependencyServiceRegistrar : IServiceRegistrar {
    /// <summary>
    /// 获取服务名
    /// </summary>
    public static string ServiceName => "Meow.Infrastructure.DependencyServiceRegistrar";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderId => 100;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled => ServiceRegistrarConfig.IsEnabled( ServiceName );

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceContext">服务上下文</param>
    public System.Action Register( ServiceContext serviceContext ) {
        return () => {
            serviceContext.HostBuilder.ConfigureServices( ( context , services ) => {
                RegisterDependency<ISingletonDependency>( services , serviceContext.TypeFinder , ServiceLifetime.Singleton );
                RegisterDependency<IScopeDependency>( services , serviceContext.TypeFinder , ServiceLifetime.Scoped );
                RegisterDependency<ITransientDependency>( services , serviceContext.TypeFinder , ServiceLifetime.Transient );
            } );
        };
    }

    /// <summary>
    /// 注册依赖
    /// </summary>
    private void RegisterDependency<TDependencyInterface>( IServiceCollection services , ITypeFinder finder , ServiceLifetime lifetime ) {
        List<(SystemType, SystemType)> types = GetTypes<TDependencyInterface>( finder );
        List<(SystemType, SystemType)> result = FilterTypes( types );
        foreach( var item in result )
            RegisterType( services , item.Item1 , item.Item2 , lifetime );
    }

    /// <summary>
    /// 获取接口类型和实现类型列表
    /// </summary>
    private List<(SystemType, SystemType)> GetTypes<TDependencyInterface>( ITypeFinder finder ) {
        List<(SystemType, SystemType)> result = new List<(SystemType, SystemType)>();
        List<SystemType> classTypes = finder.Find<TDependencyInterface>();
        foreach( SystemType classType in classTypes ) {
            List<SystemType> interfaceTypes = Meow.Helper.Reflection.GetDirectInterfaceTypes( classType , typeof( TDependencyInterface ) );
            interfaceTypes.ForEach( interfaceType => result.Add( (interfaceType, classType) ) );
        }
        return result;
    }

    /// <summary>
    /// 过滤类型
    /// </summary>
    private List<(SystemType, SystemType)> FilterTypes( List<(SystemType, SystemType)> types ) {
        List<(SystemType, SystemType)> result = new List<(SystemType, SystemType)>();
        foreach( IGrouping<SystemType , (SystemType, SystemType)> group in types.GroupBy( t => t.Item1 ) ) {
            if( group.Count() == 1 ) {
                result.Add( group.First() );
                continue;
            }
            result.Add( GetTypesByPriority( group ) );
        }
        return result;
    }

    /// <summary>
    /// 获取优先级类型
    /// </summary>
    private (SystemType, SystemType) GetTypesByPriority( IGrouping<SystemType , (SystemType, SystemType)> group ) {
        int? currentPriority = null;
        SystemType classType = null;
        foreach( (SystemType, SystemType) item in group ) {
            int priority = GetPriority( item.Item2 );
            if( currentPriority == null || priority > currentPriority ) {
                currentPriority = priority;
                classType = item.Item2;
            }
        }
        return (group.Key, classType);
    }

    /// <summary>
    /// 获取优先级
    /// </summary>
    private int GetPriority( SystemType type ) {
        var attribute = type.GetCustomAttribute<IocAttribute>();
        if( attribute == null )
            return 0;
        return attribute.Priority;
    }

    /// <summary>
    /// 注册类型
    /// </summary>
    private void RegisterType( IServiceCollection services , SystemType interfaceType , SystemType classType , ServiceLifetime lifetime ) {
        services.TryAdd( new ServiceDescriptor( interfaceType , classType , lifetime ) );
    }
}