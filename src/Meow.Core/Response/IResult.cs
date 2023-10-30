namespace Meow.Response;

/// <summary>
/// 结果
/// </summary>
public interface IResult<out TResultData> : IResponse {
    /// <summary>
    /// 业务状态码
    /// </summary>
    ResultStatusCodeEnum Code { get; }
    /// <summary>
    /// 消息
    /// </summary>
    string Message { get; }
    /// <summary>
    /// 数据
    /// </summary>
    TResultData Data { get; }
}