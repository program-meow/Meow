using Meow.Extension;
using MeowISession = Meow.Authentication.Session.ISession;

namespace Meow.Tenant.Resolver;

/// <summary>
/// 默认租户解析器
/// </summary>
public class DefaultTenantResolver : ITenantResolver {
    /// <summary>
    /// 租户配置
    /// </summary>
    private readonly TenantOptions _options;
    /// <inheritdoc />
    public int Priority { get; set; }

    /// <summary>
    /// 初始化默认租户解析器
    /// </summary>
    /// <param name="options">租户配置</param>
    public DefaultTenantResolver( IOptions<TenantOptions> options ) {
        _options = options?.Value ?? TenantOptions.Null;
    }

    /// <inheritdoc />
    public async Task<string> ResolveAsync( HttpContext context ) {
        if( context == null )
            return null;
        if( _options.IsEnabled == false )
            return null;
        MeowISession session = context.RequestServices.GetService<MeowISession>();
        if( session is { IsAuthenticated: true } )
            return session.TenantId;
        if( _options.Resolvers == null )
            return null;
        foreach( ITenantResolver resolver in _options.Resolvers.OrderByDescending( t => t.Priority ) ) {
            string result = await resolver.ResolveAsync( context );
            if( result.IsEmpty() == false )
                return result;
        }
        return null;
    }
}