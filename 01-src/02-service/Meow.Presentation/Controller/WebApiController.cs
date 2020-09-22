using Microsoft.AspNetCore.Mvc;
using ControllerBase = Meow.Presentation.Controller.Core.ControllerBase;

namespace Meow.Presentation.Controller
{
    /// <summary>
    /// WebApi控制器
    /// </summary>
    [Route( "api/[controller]" )]
    public abstract class WebApiController : ControllerBase
    {
    }
}
