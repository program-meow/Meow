using System.Net;
using Meow.Extension.Exception;
using Meow.Presentation.Parameter;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meow.Presentation.Attribute
{
    /// <summary>
    /// 异常处理过滤器
    /// </summary>
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new Result(HttpStatusCode.InternalServerError, context.Exception.GetPrompt());
        }
    }
}