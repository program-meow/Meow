using SystemType = System.Type;

namespace Meow.Event;

/// <summary>
/// 基于内存的本地事件总线
/// </summary>
public class LocalEventBus : ILocalEventBus {
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 初始化本地事件总线
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public LocalEventBus( IServiceProvider serviceProvider ) {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException( nameof( serviceProvider ) );
    }

    /// <inheritdoc />
    public async Task PublishAsync<TEvent>( TEvent @event , CancellationToken cancellationToken = default ) where TEvent : IEvent {
        cancellationToken.ThrowIfCancellationRequested();
        if( @event == null )
            return;
        await PublishLocalEventAsync( @event , cancellationToken );
    }

    /// <summary>
    /// 发布本地事件
    /// </summary>
    private async Task PublishLocalEventAsync<TEvent>( TEvent @event , CancellationToken cancellationToken ) where TEvent : IEvent {
        SystemType eventType = @event.GetType();
        IEnumerable<ILocalEventHandler> handlers = GetEventHandlers( eventType );
        if( handlers == null )
            return;
        foreach( ILocalEventHandler handler in handlers.Where( t => t is { Enabled: true } ).OrderBy( t => t.Order ) ) {
            MethodInfo method = typeof( IEventHandler<> ).MakeGenericType( eventType ).GetMethod( "HandleAsync" , new[] { eventType , typeof( CancellationToken ) } );
            if( method == null )
                return;
            object result = method.Invoke( handler , new object[] { @event , cancellationToken } );
            if( result == null )
                return;
            await ( Task ) result;
        }
    }

    /// <summary>
    /// 获取事件处理器列表
    /// </summary>
    private IEnumerable<ILocalEventHandler> GetEventHandlers( SystemType eventType ) {
        SystemType handlerType = typeof( IEventHandler<> ).MakeGenericType( eventType );
        SystemType serviceType = typeof( IEnumerable<> ).MakeGenericType( handlerType );
        object handlers = _serviceProvider.GetService( serviceType );
        if( handlers is not IEnumerable<IEventHandler> eventHandlers )
            yield break;
        foreach( var handler in eventHandlers )
            yield return handler as ILocalEventHandler;
    }
}