﻿using Meow.Json;

namespace Meow.Application.Controller;

/// <summary>
/// WebApi控制器基类
/// </summary>
[ApiController]
[Route( "api/[controller]" )]
[ExceptionHandler( Order = 1 )]
[ErrorLogFilter( Order = 2 )]
public abstract class WebApiControllerBase : ControllerBase {

    /// <summary>
    /// 会话
    /// </summary>
    protected virtual IMeowSession Session => UserSession.Instance;

    /// <summary>
    /// 获取日志操作
    /// </summary>
    protected virtual ILog GetLog() {
        try {
            ILogFactory logFactory = Ioc.Create<ILogFactory>();
            return logFactory.CreateLog( GetType() );
        } catch {
            return NullLog.Instance;
        }
    }

    /// <summary>
    /// 返回成功消息
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="message">消息</param>
    /// <param name="statusCode">Http状态码</param>
    protected virtual IActionResult Success( dynamic data = null , string message = null , int? statusCode = 200 ) {
        message ??= ResultStatusCodeEnum.Success.GetDescription();
        return GetResult( ResultStatusCodeEnum.Success.GetValue().SafeString() , message , data , statusCode );
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    private IActionResult GetResult( string code , string message , dynamic data , int? httpStatusCode ) {
        JsonSerializerOptions options = GetJsonSerializerOptions();
        IResultFactory resultFactory = HttpContext.RequestServices.GetService<IResultFactory>();
        if( resultFactory == null )
            return new Result( code , message , data , httpStatusCode , options );
        return resultFactory.CreateResult( code , message , data , httpStatusCode , options );
    }

    /// <summary>
    /// 获取Json序列化配置
    /// </summary>
    private JsonSerializerOptions GetJsonSerializerOptions() {
        IJsonSerializerOptionsFactory factory = HttpContext.RequestServices.GetService<IJsonSerializerOptionsFactory>();
        factory.CheckNull( nameof( factory ) );
        return factory.CreateOptions();
    }

    /// <summary>
    /// 返回失败消息
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="statusCode">Http状态码</param>
    protected virtual IActionResult Fail( string message , int? statusCode = 200 ) {
        return GetResult( ResultStatusCodeEnum.Error.GetValue().SafeString() , message , null , statusCode );
    }

    /// <summary>
    /// 获取文件流结果,内容类型设置为 application/octet-stream
    /// </summary>
    /// <param name="stream">文件流</param>
    protected IActionResult GetStreamResult( SystemStream stream ) {
        return new FileStreamResult( stream , "application/octet-stream" );
    }

    /// <summary>
    /// 获取文件流结果,内容类型设置为 application/octet-stream
    /// </summary>
    /// <param name="stream">文件流</param>
    protected IActionResult GetStreamResult( byte[] stream ) {
        return GetStreamResult( new MemoryStream( stream ) );
    }
}