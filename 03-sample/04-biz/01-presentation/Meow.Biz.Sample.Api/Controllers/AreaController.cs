using Microsoft.AspNetCore.Mvc;
using Meow.Biz.Area.Service;

namespace Meow.Biz.Sample.Api.Controllers
{
    /// <summary>
    /// 地区控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AreaController : Area.Controller.AreaController
    {
        /// <summary>
        /// 初始化地区控制器
        /// </summary>
        /// <param name="areaService">地区服务</param>
        public AreaController(IAreaService areaService) : base(areaService)
        {
        }
    }
}