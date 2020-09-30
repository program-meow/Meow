using Microsoft.AspNetCore.Mvc;

namespace Meow.Application.Presentation.Controller
{
    /// <summary>
    /// WebApi控制器
    /// </summary>
    [Route( "api/[controller]" )]
    public abstract class WebApiController : Core.ControllerBase
    {
    }
}
