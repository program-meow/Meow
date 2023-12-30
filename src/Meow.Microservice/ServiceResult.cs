namespace Meow.Microservice;

/// <summary>
/// 服务约定结果
/// </summary>
public class ServiceResult<TData> {
    /// <summary>
    /// 状态码
    /// </summary>
    public ResultStatusEnum Code { get; set; }
    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public TData Data { get; set; }
}