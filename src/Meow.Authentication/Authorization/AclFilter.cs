using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meow.Extension;
using Meow.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Meow.Authentication.Authorization;

/// <summary>
/// 访问控制过滤器
/// </summary>
public class AclFilter : AuthorizeFilter
{
    /// <summary>
    /// 初始化访问控制过滤器
    /// </summary>
    public AclFilter()
        : base(new AclPolicyProvider(), new[] { new AclAttribute() })
    {
    }

    /// <summary>
    /// 授权
    /// </summary>
    /// <param name="context">授权过滤器上下文</param>
    public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        context.CheckNull(nameof(context));
        if (context.IsEffectivePolicy(this) == false)
            return;
        AuthorizationPolicy effectivePolicy = await GetEffectivePolicyAsync(context);
        if (effectivePolicy == null)
            return;
        IPolicyEvaluator policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
        AuthenticateResult authenticateResult = await policyEvaluator.AuthenticateAsync(effectivePolicy, context.HttpContext);
        if (HasAllowAnonymous(context))
            return;
        PolicyAuthorizationResult authorizationResult = await policyEvaluator.AuthorizeAsync(effectivePolicy, authenticateResult, context.HttpContext, context);
        SetContextResult(context, authorizationResult);
    }

    /// <summary>
    /// 获取有效授权策略
    /// </summary>
    protected async Task<AuthorizationPolicy> GetEffectivePolicyAsync(AuthorizationFilterContext context)
    {
        AuthorizationPolicyBuilder builder = new AuthorizationPolicyBuilder(await ComputePolicyAsync());
        foreach (IFilterMetadata item in context.Filters)
        {
            if (ReferenceEquals(this, item))
                continue;
            if (item is AclFilter authorizeFilter)
                builder.Combine(await authorizeFilter.ComputePolicyAsync());
        }
        Endpoint endpoint = context.HttpContext.GetEndpoint();
        if (endpoint != null)
        {
            IAuthorizationPolicyProvider policyProvider = PolicyProvider ?? context.HttpContext.RequestServices.GetRequiredService<IAuthorizationPolicyProvider>();
            IReadOnlyList<IAuthorizeData> endpointAuthorizeData = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? Meow.Helper.Array.Empty<IAuthorizeData>();
            AuthorizationPolicy endpointPolicy = await AuthorizationPolicy.CombineAsync(policyProvider, endpointAuthorizeData);
            if (endpointPolicy != null && endpointPolicy.Requirements.Any(t => t is AclRequirement))
            {
                builder.Requirements.Remove(builder.Requirements.FirstOrDefault(t => t is AclRequirement));
                builder.Combine(endpointPolicy);
            }
        }
        return builder.Build();
    }

    /// <summary>
    /// 合并授权策略
    /// </summary>
    protected async ValueTask<AuthorizationPolicy> ComputePolicyAsync()
    {
        return await AuthorizationPolicy.CombineAsync(PolicyProvider, AuthorizeData);
    }

    /// <summary>
    /// 是否允许匿名访问
    /// </summary>
    protected bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        IList<IFilterMetadata> filters = context.Filters;
        foreach (IFilterMetadata item in filters)
        {
            if (item is IAllowAnonymousFilter)
                return true;
        }
        Endpoint endpoint = context.HttpContext.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            return true;
        return false;
    }

    /// <summary>
    /// 设置授权过滤器上下文结果
    /// </summary>
    /// <param name="context">授权过滤器上下文</param>
    /// <param name="authorizationResult">授权结果</param>
    protected virtual void SetContextResult(AuthorizationFilterContext context, PolicyAuthorizationResult authorizationResult)
    {
        if (authorizationResult.Succeeded)
            return;
        context.Result = new JsonResult(new { Code = ResultStatusCodeEnum.Unauthorized }) { StatusCode = 200 };
    }
}