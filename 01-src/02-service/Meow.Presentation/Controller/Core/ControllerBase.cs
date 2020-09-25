using System.Net;
using Meow.Auth.Session;
using Meow.Extension.Helper;
using Meow.Presentation.Attribute;
using Meow.Presentation.Parameter;
using Microsoft.AspNetCore.Mvc;

namespace Meow.Presentation.Controller.Core
{
    /// <summary>
    /// 控制器
    /// </summary>
    [ExceptionHandler]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.Controller
    {
        /// <summary>
        /// 会话
        /// </summary>
        public virtual ISession Session => Meow.Auth.Session.Session.Instance;

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        protected virtual IActionResult Ok(dynamic data = null, string message = null)
        {
            if (message == null)
                message = HttpStatusCode.OK.Description();
            return new Result(HttpStatusCode.OK, message, data);
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        protected virtual IActionResult Error(string message)
        {
            return new Result(HttpStatusCode.InternalServerError, message);
        }
    }
}