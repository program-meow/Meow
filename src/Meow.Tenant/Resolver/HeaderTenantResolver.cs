﻿namespace Meow.Tenant.Resolver;

/// <summary>
/// 基于请求头的租户解析器
/// </summary>
public class HeaderTenantResolver : TenantResolverBase {
    /// <summary>
    /// 解析租户标识
    /// </summary>
    protected override Task<string> Resolve( HttpContext context ) {
        string key = GetTenantKey( context );
        context.Request.Headers.TryGetValue( key , out StringValues result );
        string tenantId = result.FirstOrDefault();
        GetLog( context ).LogTrace( $"执行请求头租户解析器,{key}={tenantId}" );
        return Task.FromResult( tenantId );
    }
}