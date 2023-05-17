using Meow.Authentication.Authorization;
using Microsoft.AspNetCore.Authorization;
using MeowOption = Meow.Config.Option;

namespace Meow.Authentication.Extension;

/// <summary>
/// AspNetCore操作扩展
/// </summary>
public static class OptionsExtension
{
    /// <summary>
    /// 配置授权访问控制
    /// </summary>
    /// <typeparam name="TPermissionManager">权限管理器类型</typeparam>
    /// <param name="options">配置项</param>
    public static MeowOption UseAcl<TPermissionManager>(this MeowOption options) where TPermissionManager : class, IPermissionManager
    {
        options.AddExtension(new AspNetCoreOptionsExtension<TPermissionManager, AclMiddlewareResultHandler>());
        return options;
    }

    /// <summary>
    /// 配置授权访问控制
    /// </summary>
    /// <typeparam name="TPermissionManager">权限管理器类型</typeparam>
    /// <typeparam name="TAuthorizationMiddlewareResultHandler">授权中间件结果处理器类型</typeparam>
    /// <param name="options">配置项</param>
    public static MeowOption UseAcl<TPermissionManager, TAuthorizationMiddlewareResultHandler>(this MeowOption options)
        where TPermissionManager : class, IPermissionManager
        where TAuthorizationMiddlewareResultHandler : class, IAuthorizationMiddlewareResultHandler
    {
        options.AddExtension(new AspNetCoreOptionsExtension<TPermissionManager, TAuthorizationMiddlewareResultHandler>());
        return options;
    }
}