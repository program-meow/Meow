using System.Net;
using Meow.Extension;

namespace Meow.Response
{
    /// <summary>
    /// 结果 - 规则：业务状态码 "200" 为成功，"500"为错误
    /// </summary>
    public class Result : Result<object>
    {
        /// <summary>
        /// 初始化结果
        /// </summary>
        /// <param name="code">结果状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(ResultStatusCode code, string message, object data = default(object))
            : this(code.GetValue().SafeString(), message, data)
        {
        }

        /// <summary>
        /// 初始化结果
        /// </summary>
        /// <param name="code">HTTP状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(HttpStatusCode code, string message, object data = default(object))
            : this(code.GetValue().SafeString(), message, data)
        {
        }

        /// <summary>
        /// 初始化结果
        /// </summary>
        /// <param name="code">业务状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(string code, string message, object data = default(object)) : base(code, message, data)
        {
        }
    }

    /// <summary>
    /// 结果 - 规则：业务状态码 "200" 为成功，"500"为错误
    /// </summary>
    public class Result<TResult> : IResult<TResult>
    {
        /// <summary>
        /// 业务状态码
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// 数据
        /// </summary>
        public TResult Data { get; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsOk => Code.SafeString() == ResultStatusCode.Ok.GetValue().SafeString();

        /// <summary>
        /// 初始化结果
        /// </summary>
        /// <param name="code">结果状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(ResultStatusCode code, string message, TResult data = default(TResult))
            : this(code.GetValue().SafeString(), message, data)
        {
        }

        /// <summary>
        /// 初始化结果
        /// </summary>
        /// <param name="code">HTTP状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(HttpStatusCode code, string message, TResult data = default(TResult))
            : this(code.GetValue().SafeString(), message, data)
        {
        }

        /// <summary>
        /// 初始化结果
        /// </summary>
        /// <param name="code">业务状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(string code, string message, TResult data = default(TResult))
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
