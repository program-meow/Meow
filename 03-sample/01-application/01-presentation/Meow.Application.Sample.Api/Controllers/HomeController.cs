using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Meow.Application.Presentation.Controller;
using Meow.Application.Sample.Service.Abstractions.Systems;

namespace Meow.Application.Sample.Api.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HomeController : WebApiController
    {
        /// <summary>
        /// 应用程序服务
        /// </summary>
        protected IApplicationService ApplicationService { get; set; }

        /// <summary>
        /// 测试
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await ApplicationService.DeleteAllAsync();
            await ApplicationService.AddAsync();
            var result = await ApplicationService.GetAllAsync();
            return Ok(result);
        }
    }
}