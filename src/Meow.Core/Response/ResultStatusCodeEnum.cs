using System.ComponentModel;

namespace Meow.Response;

/// <summary>
/// 结果状态码
/// </summary>
public enum ResultStatusCodeEnum
{
    /// <summary>
    /// 成功
    /// </summary>
    [Description("success")]
    Ok = 200,
    /// <summary>
    /// 错误
    /// </summary>
    [Description("error")]
    Error = 500,
    /// <summary>
    /// 未授权
    /// </summary>
    [Description("meow.unauthorized")]
    Unauthorized = 401,
}