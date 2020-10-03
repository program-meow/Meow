using System.Threading.Tasks;
using Meow.Addin.Sample.Service.Abstractions;
using Meow.Application.Presentation.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Meow.Addin.Sample.Api.Controllers
{
    /// <summary>
    /// 邮箱控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EmailController : WebApiController
    {
        /// <summary>
        /// 邮箱演示服务
        /// </summary>
        protected IEmailSampleService EmailSampleService { get; set; }

        /// <summary>
        /// 初始化邮箱控制器
        /// </summary>
        /// <param name="emailSampleService">邮箱演示服务</param>
        public EmailController(IEmailSampleService emailSampleService)
        {
            EmailSampleService = emailSampleService;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        [HttpGet("Send")]
        public IActionResult Send()
        {
            var result = EmailSampleService.Send();
            return Ok(result);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        [HttpGet("SendAsync")]
        public async Task<IActionResult> SendAsync()
        {
            var result = await EmailSampleService.SendAsync();
            return Ok(result);
        }
    }
}
