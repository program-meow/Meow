using Meow.Aop;

namespace Meow.Application.Filter;

/// <summary>
/// 错误日志过滤器
/// </summary>
public class ErrorLogFilterAttribute : ExceptionFilterAttribute {
    /// <summary>
    /// 异常处理
    /// </summary>
    public override void OnException( ExceptionContext context ) {
        if( context == null )
            return;
        ILogger<ErrorLogFilterAttribute> log = context.HttpContext.RequestServices.GetService<ILogger<ErrorLogFilterAttribute>>();
        SystemException exception = context.Exception.GetRawException();
        if( exception is Warning warning ) {
            log.LogWarning( warning , exception.Message );
            return;
        }
        log.LogError( exception , exception.Message );
    }
}