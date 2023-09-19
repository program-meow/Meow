using Meow.Data.Filter;
using Meow.Extension;
using SystemType = System.Type;

namespace Meow.Data.EntityFrameworkCore.Filter;

/// <summary>
/// 数据过滤器管理器
/// </summary>
public class FilterManager : IFilterManager {
    /// <summary>
    /// 同步锁
    /// </summary>
    private static readonly object Sync = new();
    /// <summary>
    /// 过滤器类型列表
    /// </summary>
    private static readonly List<SystemType> _filterTypes = new();
    /// <summary>
    /// 过滤器字典
    /// </summary>
    private readonly Dictionary<SystemType , IFilter> _filters = new();
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 初始化数据过滤器管理器
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public FilterManager( IServiceProvider serviceProvider ) {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException( nameof( serviceProvider ) );
    }

    /// <summary>
    /// 添加过滤器类型
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public static void AddFilterType<TFilterType>() {
        SystemType type = typeof( TFilterType );
        lock( Sync ) {
            if( _filterTypes.Contains( type ) )
                return;
            _filterTypes.Add( type );
        }
    }

    /// <summary>
    /// 移除过滤器类型
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public static void RemoveFilterType<TFilterType>() {
        SystemType type = typeof( TFilterType );
        lock( Sync ) {
            if( _filterTypes.Contains( type ) )
                _filterTypes.Remove( type );
        }
    }

    /// <summary>
    /// 清空过滤器类型
    /// </summary>
    public static void ClearFilterTypes() {
        _filterTypes.Clear();
    }

    /// <inheritdoc />
    public void EnableFilter<TFilterType>() where TFilterType : class {
        IFilter filter = GetFilter<TFilterType>();
        filter?.Enable();
    }

    /// <inheritdoc />
    public IDisposable DisableFilter<TFilterType>() where TFilterType : class {
        IFilter filter = GetFilter<TFilterType>();
        return filter?.Disable();
    }

    /// <inheritdoc />
    public IFilter GetFilter<TFilterType>() where TFilterType : class {
        return GetFilter( typeof( TFilterType ) );
    }

    /// <inheritdoc />
    public IFilter GetFilter( SystemType filterType ) {
        if( _filters.ContainsKey( filterType ) == false ) {
            SystemType serviceType = typeof( IFilter<> ).MakeGenericType( filterType );
            object filter = _serviceProvider.GetService( serviceType );
            _filters.Add( filterType , ( IFilter ) filter );
        }
        return _filters[ filterType ];
    }

    /// <inheritdoc />
    public bool IsEntityEnabled<TEntity>() {
        foreach( SystemType type in _filterTypes ) {
            IFilter filter = GetFilter( type );
            if( filter.IsEntityEnabled<TEntity>() )
                return true;
        }
        return false;
    }

    /// <inheritdoc />
    public bool IsEnabled<TFilterType>() where TFilterType : class {
        IFilter filter = GetFilter<TFilterType>();
        if( filter == null )
            return false;
        return filter.IsEnabled;
    }

    /// <inheritdoc />
    public Expression<Func<TEntity , bool>> GetExpression<TEntity>( object state ) where TEntity : class {
        Expression<Func<TEntity , bool>> expression = null;
        foreach( SystemType type in _filterTypes ) {
            IFilter filter = GetFilter( type );
            if( filter.IsEntityEnabled<TEntity>() )
                expression = expression.And( filter.GetExpression<TEntity>( state ) );
        }
        return expression;
    }
}