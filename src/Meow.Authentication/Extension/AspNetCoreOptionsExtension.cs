using Meow.Authentication.Authorization;
using Meow.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Meow.Authentication.Extension;

/// <summary>
/// AspNetCore操作配置扩展
/// </summary>
public class AspNetCoreOptionsExtension<TPermissionManager, TAuthorizationMiddlewareResultHandler> : OptionExtensionBase
    where TPermissionManager : class, IPermissionManager
    where TAuthorizationMiddlewareResultHandler : class, IAuthorizationMiddlewareResultHandler
{
    /// <summary>
    /// 初始化AspNetCore操作配置扩展
    /// </summary>
    public AspNetCoreOptionsExtension()
    {
    }

    /// <inheritdoc />
    public override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, AclHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, AclPolicyProvider>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, TAuthorizationMiddlewareResultHandler>();
        services.AddSingleton<IPermissionManager, TPermissionManager>();
    }
}