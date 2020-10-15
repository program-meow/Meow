using System.ComponentModel;
using System.Net;
using Meow.Parameter.Enum;

namespace Meow.Parameter.Response
{
    /// <summary>
    /// HTTP请求响应对象
    /// </summary>
    public class HttpResponse : HttpResponse<string>
    {
        /// <summary>
        /// 初始化HTTP请求响应对象
        /// </summary>
        /// <param name="code">HTTP状态码</param>
        /// <param name="contentType">HTTP内容类型</param>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        public HttpResponse(HttpStatusCode? code = null, HttpContentType? contentType = null, string data = null, string message = "") : base(code, contentType, data, message)
        {
        }
    }

    /// <summary>
    /// HTTP请求响应对象
    /// </summary>
    public class HttpResponse<TResult>
    {
        /// <summary>
        /// HTTP状态码
        /// </summary>
        [DisplayName("HTTP状态码")]
        public HttpStatusCode? Code { get; }
        /// <summary>
        /// HTTP内容类型
        /// </summary>
        [DisplayName("内容类型")]
        public HttpContentType? ContentType { get; }
        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        public TResult Data { get; }
        /// <summary>
        /// 消息
        /// </summary>
        [DisplayName("消息")]
        public string Message { get; }

        /// <summary>
        /// 初始化HTTP请求响应对象
        /// </summary>
        /// <param name="code">HTTP状态码</param>
        /// <param name="contentType">HTTP内容类型</param>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        public HttpResponse(HttpStatusCode? code = null, HttpContentType? contentType = null, TResult data = default(TResult), string message = "")
        {
            Code = code;
            ContentType = contentType;
            Data = data;
            Message = message;
        }
    }
}