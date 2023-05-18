using Meow.Application.Extension;
using Meow.Exception;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Meow.Application.WebApi.Filter;

/// <summary>
/// 错误日志过滤器
/// </summary>
public class ErroLogFilterAttribute : ExceptionFilterAttribute
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public override void OnException(ExceptionContext context)
    {
        if (context == null)
            return;
        var log = context.HttpContext.RequestServices.GetService<ILogger<ErroLogFilterAttribute>>();
        var exception = context.Exception.GetRawException();
        if (exception is Warning warning)
        {
            log.LogWarning(warning, exception.Message);
            return;
        }
        log.LogError(exception, exception.Message);
    }
}