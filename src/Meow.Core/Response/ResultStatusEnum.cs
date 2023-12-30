namespace Meow.Response;

/// <summary>
/// 结果状态
/// </summary>
public enum ResultStatusEnum {
    /// <summary>
    /// 成功
    /// </summary>
    [Description( "success" )]
    Success = 200,
    /// <summary>
    /// 错误
    /// </summary>
    [Description( "error" )]
    Error = 500,
    /// <summary>
    /// 未授权
    /// </summary>
    [Description( "meow.unauthorized" )]
    Unauthorized = 401,
}