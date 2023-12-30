namespace Meow.Microservice;

/// <summary>
/// 服务状态码
/// </summary>
public enum ServiceStateEnum {
    /// <summary>
    /// 失败
    /// </summary>
    Fail,
    /// <summary>
    /// 成功
    /// </summary>
    Ok,
    /// <summary>
    /// 未授权
    /// </summary>
    Unauthorized
}