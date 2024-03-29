﻿namespace Meow.Microservice.Dapr.Events.Filters;

/// <summary>
/// 集成事件订阅处理过滤器
/// </summary>
public class SubscriptionFilterAttribute : ActionFilterAttribute {
    /// <summary>
    /// 执行
    /// </summary>
    public override async Task OnActionExecutionAsync( ActionExecutingContext context , ActionExecutionDelegate next ) {
        if( context == null )
            return;
        if( next == null )
            return;
        if( context.HttpContext.Request.ContentType != "application/cloudevents+json" )
            return;
        ILogger<SubscriptionFilterAttribute> log = context.HttpContext.RequestServices.GetService<ILogger<SubscriptionFilterAttribute>>() ?? NullLogger<SubscriptionFilterAttribute>.Instance;
        try {
            JsonElement body = await GetBodyJsonElement( context );
            string routeUrl = context.HttpContext.Request.GetDisplayUrl();
            string eventId = GetEventId( body );
            if( eventId.IsEmpty() ) {
                log.LogError( "集成事件标识为空,body:{@body},routeUrl:{routeUrl}" , body , routeUrl );
                return;
            }
            log.LogTrace( "准备处理集成事件订阅,eventId={@eventId},routeUrl={routeUrl}" , eventId , routeUrl );
            IIntegrationEventManager manager = context.HttpContext.RequestServices.GetRequiredService<IIntegrationEventManager>();
            IntegrationEventLog eventLog = await GetEventLog( manager , eventId );
            if( eventLog == null ) {
                log.LogError( "集成事件为空,eventId={@eventId},body:{@body},routeUrl:{routeUrl}" , eventId , body , routeUrl );
                return;
            }
            if( manager.CanSubscription( eventLog ) == false ) {
                log.LogDebug( "集成事件订阅无需处理,eventId={@eventId},body:{@body},routeUrl={routeUrl}" , eventId , body , routeUrl );
                if( manager.IsSubscriptionSuccess( eventLog ) )
                    context.Result = PubsubResult.Success;
                return;
            }
            IPubsubCallback callback = context.HttpContext.RequestServices.GetRequiredService<IPubsubCallback>();
            await callback.OnSubscriptionBefore( eventLog , routeUrl );
            ActionExecutedContext executedContext = await next();
            OnActionExecuted( executedContext );
            if( executedContext.Result is PubsubResult result ) {
                await callback.OnSubscriptionAfter( eventId , result == PubsubResult.Success , result.Message );
                return;
            }
            if( executedContext.Exception != null ) {
                await callback.OnSubscriptionAfter( eventId , false , Warning.GetMessage( executedContext.Exception ) );
                return;
            }
        } catch( SystemException exception ) {
            log.LogError( exception , "集成事件订阅处理过滤器执行失败." );
            throw;
        }
    }

    /// <summary>
    /// 获取请求正文Json元素
    /// </summary>
    protected async Task<JsonElement> GetBodyJsonElement( ActionExecutingContext context ) {
        byte[] body = await GetBody( context );
        DaprClient client = context.HttpContext.RequestServices.GetService<DaprClient>();
        return await Meow.Helper.Json.ToObjectAsync<JsonElement>( body , client?.JsonSerializerOptions );
    }

    /// <summary>
    /// 获取请求正文
    /// </summary>
    protected async Task<byte[]> GetBody( ActionExecutingContext context ) {
        context.HttpContext.Request.EnableBuffering();
        return await Meow.Helper.File.ReadToBytesAsync( context.HttpContext.Request.Body );
    }

    /// <summary>
    /// 获取集成事件标识
    /// </summary>
    protected string GetEventId( JsonElement body ) {
        return body.TryGetProperty( "eventId" , out JsonElement eventId ) ? eventId.Deserialize<string>() : null;
    }

    /// <summary>
    /// 获取集成事件
    /// </summary>
    protected async Task<IntegrationEventLog> GetEventLog( IIntegrationEventManager manager , string eventId ) {
        for( int i = 0 ; i < 100 ; i++ ) {
            IntegrationEventLog eventLog = await manager.GetAsync( eventId );
            if( eventLog != null )
                return eventLog;
            await Task.Delay( 100 );
        }
        return null;
    }
}