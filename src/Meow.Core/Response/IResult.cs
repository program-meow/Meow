namespace Meow.Response;

/// <summary>
/// 结果
/// </summary>
public interface IResult<out TResult> : IResponse {
    /// <summary>
    /// 业务状态码
    /// </summary>
    string Code { get; }
    /// <summary>
    /// 消息
    /// </summary>
    string Message { get; }
    /// <summary>
    /// 数据
    /// </summary>
    TResult Data { get; }
}