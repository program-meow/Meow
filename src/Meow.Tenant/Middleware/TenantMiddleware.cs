﻿namespace Meow.Tenant.Middleware;

/// <summary>
/// 租户处理中间件
/// </summary>
public class TenantMiddleware {
    /// <summary>
    /// 中间件管道
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 初始化租户处理中间件
    /// </summary>
    /// <param name="next">中间件管道</param>
    public TenantMiddleware( RequestDelegate next ) {
        _next = next;
    }

    /// <summary>
    /// 执行管道
    /// </summary>
    /// <param name="httpContext">Http上下文</param>
    public async Task InvokeAsync( HttpContext httpContext ) {
        if( _next == null )
            return;
        if( httpContext == null ) {
            await _next( httpContext );
            return;
        }
        ILogger<TenantMiddleware> log = httpContext.RequestServices.GetService<ILogger<TenantMiddleware>>() ?? NullLogger<TenantMiddleware>.Instance;
        ITenantResolver resolver = httpContext.RequestServices.GetRequiredService<ITenantResolver>();
        try {
            string tenantId = await resolver.ResolveAsync( httpContext );
            TenantManager.CurrentTenantId = tenantId;
            await _next( httpContext );
        } catch( System.Exception exception ) {
            log.LogError( exception , "租户处理中间件发生异常" );
            throw;
        }
    }
}