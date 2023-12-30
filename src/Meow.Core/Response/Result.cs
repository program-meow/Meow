namespace Meow.Response;

/// <summary>
/// 结果 - 规则：业务状态码 "200" 为成功，"500"为错误
/// </summary>
public class Result : Result<dynamic> {
    /// <summary>
    /// 初始化结果
    /// </summary>
    /// <param name="code">HTTP状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    public Result( HttpStatusCode code , string message , object data = default )
        : this( code.ToResultStatusCode() , message , data ) {
    }

    /// <summary>
    /// 初始化结果
    /// </summary>
    /// <param name="code">结果状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    public Result( ResultStatusEnum code , string message , object data = default )
        : base( code , message , data ) {
    }
}

/// <summary>
/// 结果 - 规则：业务状态码 "200" 为成功，"500"为错误
/// </summary>
public class Result<TResultData> : IResult<TResultData> {
    /// <inheritdoc />
    public ResultStatusEnum Code { get; }
    /// <inheritdoc />
    public string Message { get; }
    /// <inheritdoc />
    public TResultData Data { get; }

    /// <summary>
    /// 初始化结果
    /// </summary>
    /// <param name="code">HTTP状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    public Result( HttpStatusCode code , string message , TResultData data = default )
        : this( code.ToResultStatusCode() , message , data ) {
    }

    /// <summary>
    /// 初始化结果
    /// </summary>
    /// <param name="code">结果状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    public Result( ResultStatusEnum code , string message , TResultData data = default ) {
        Code = code;
        Message = message;
        Data = data;
    }
}