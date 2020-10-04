using Meow.Application.Presentation.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Meow.NuGet.Sample.Api.Controllers
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HomeController : WebApiController
    {
        /// <summary>
        /// 初始化首页控制器
        /// </summary>
        public HomeController()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("成功");
        }
    }
}
