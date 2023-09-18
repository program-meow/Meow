using Meow.Config;
using Meow.Dependency;
using Meow.Extension;
using Meow.Reflection;
using SystemAction = System.Action;
using SystemType = System.Type;

namespace Meow.Infrastructure;

/// <summary>
/// 启动器
/// </summary>
public class Bootstrapper {
    /// <summary>
    /// 主机生成器
    /// </summary>
    private readonly IHostBuilder _hostBuilder;
    /// <summary>
    /// 服务配置
    /// </summary>
    private readonly Action<Options> _setupAction;
    /// <summary>
    /// 程序集查找器
    /// </summary>
    private readonly IAssemblyFinder _assemblyFinder;
    /// <summary>
    /// 类型查找器
    /// </summary>
    private readonly ITypeFinder _typeFinder;
    /// <summary>
    /// 服务配置操作列表
    /// </summary>
    private readonly List<SystemAction> _serviceActions;

    /// <summary>
    /// 初始化启动器
    /// </summary>
    /// <param name="hostBuilder">主机生成器</param>
    /// <param name="setupAction">服务配置操作</param>
    public Bootstrapper( IHostBuilder hostBuilder , Action<Options> setupAction = null ) {
        _hostBuilder = hostBuilder ?? throw new ArgumentNullException( nameof( hostBuilder ) );
        _setupAction = setupAction;
        _assemblyFinder = new AppDomainAssemblyFinder { AssemblySkipPattern = BootstrapperConfig.AssemblySkipPattern };
        _typeFinder = new AppDomainTypeFinder( _assemblyFinder );
        _serviceActions = new List<SystemAction>();
    }

    /// <summary>
    /// 启动
    /// </summary>
    public virtual void Start() {
        SetConfiguration();
        ResolveServiceRegistrar();
        ConfigOptions();
        ResolveDependencyRegistrar();
        ExecuteServiceActions();
    }

    /// <summary>
    /// 设置配置实例
    /// </summary>
    protected virtual void SetConfiguration() {
        _hostBuilder.ConfigureServices( ( context , services ) => {
            Meow.Helper.Config.SetConfiguration( context.Configuration );
            services.TryAddSingleton( _assemblyFinder );
            services.TryAddSingleton( _typeFinder );
        } );
    }

    /// <summary>
    /// 解析服务注册器
    /// </summary>
    protected virtual void ResolveServiceRegistrar() {
        List<SystemType> types = _typeFinder.Find<IServiceRegistrar>();
        List<IServiceRegistrar> instances = types.Select( type => Meow.Helper.Reflection.CreateInstance<IServiceRegistrar>( type ) ).Where( t => t.Enabled ).OrderBy( t => t.OrderId ).ToList();
        ServiceContext context = new ServiceContext( _hostBuilder , _assemblyFinder , _typeFinder );
        instances.ForEach( t => _serviceActions.Add( t.Register( context ) ) );
    }

    /// <summary>
    /// 注册配置项
    /// </summary>
    protected virtual void ConfigOptions() {
        _hostBuilder.AddOptions( _setupAction );
    }

    /// <summary>
    /// 解析依赖注册器
    /// </summary>
    protected virtual void ResolveDependencyRegistrar() {
        List<SystemType> types = _typeFinder.Find<IDependencyRegistrar>();
        List<IDependencyRegistrar> instances = types.Select( type => Meow.Helper.Reflection.CreateInstance<IDependencyRegistrar>( type ) ).OrderBy( t => t.Order ).ToList();
        _hostBuilder.ConfigureServices( ( context , services ) => {
            instances.ForEach( t => t.Register( services ) );
        } );
    }

    /// <summary>
    /// 执行延迟服务注册操作
    /// </summary>
    protected virtual void ExecuteServiceActions() {
        _serviceActions.ForEach( action => action?.Invoke() );
    }
}