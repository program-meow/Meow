﻿namespace Meow.Application.Logging;

/// <summary>
/// 日志上下文中间件
/// </summary>
public class LogContextMiddleware {
    /// <summary>
    /// 下个中间件
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 初始化日志上下文中间件
    /// </summary>
    /// <param name="next">下个中间件</param>
    public LogContextMiddleware( RequestDelegate next ) {
        _next = next;
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">Http上下文</param>
    public async Task Invoke( HttpContext context ) {
        var traceId = context.Request.Headers[ "x-correlation-id" ].SafeString();
        if( traceId.IsEmpty() )
            traceId = context.TraceIdentifier;
        var session = context.RequestServices.GetService<Meow.Authentication.Session.ISession>();
        var environment = context.RequestServices.GetService<IWebHostEnvironment>();
        var logContext = new LogContext {
            Stopwatch = Stopwatch.StartNew() ,
            TraceId = traceId ,
            UserId = session?.UserId ,
            Application = environment?.ApplicationName ,
            Environment = environment?.EnvironmentName
        };
        context.Items[ LogContextAccessor.LogContextKey ] = logContext;
        await _next( context );
    }
}