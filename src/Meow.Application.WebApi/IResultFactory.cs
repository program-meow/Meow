﻿namespace Meow.Application;

/// <summary>
/// 返回结果工厂
/// </summary>
public interface IResultFactory : ISingletonDependency {
    /// <summary>
    /// 创建返回结果
    /// </summary>
    /// <param name="code">业务状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    /// <param name="httpStatusCode">Http状态码</param>
    /// <param name="options">Json序列化配置</param>
    IActionResult CreateResult( string code , string message , dynamic data , int? httpStatusCode , JsonSerializerOptions options );
}