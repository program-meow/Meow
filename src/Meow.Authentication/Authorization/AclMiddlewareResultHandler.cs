﻿using System.Threading.Tasks;
using Meow.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Meow.Authentication.Authorization;

/// <summary>
/// 授权中间件结果处理器
/// </summary>
public class AclMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    /// <summary>
    /// 默认处理器
    /// </summary>
    private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

    /// <summary>
    /// 处理授权结果
    /// </summary>
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Succeeded == false)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsJsonAsync(new { Code = ResultStatusCodeEnum.Unauthorized });
            return;
        }
        await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}