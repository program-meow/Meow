using System.Threading.Tasks;
using Meow.Sample.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Meow.Sample.Service.Abstractions.Systems;

namespace Meow.Sample.Api.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 应用程序服务
        /// </summary>
        protected IApplicationService ApplicationService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HomeController(IApplicationService applicationService)
        {
            ApplicationService = applicationService;
        }

        /// <summary>
        /// 测试
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Application>> Get()
        {
            await ApplicationService.DeleteAllAsync();
            await ApplicationService.AddAsync();
            var result = await ApplicationService.GetAllAsync();
            return result;
        }
    }
}
