using System.ComponentModel;
using System.Net;
using Meow.Parameter.Enum;

namespace Meow.Parameter.Response
{
    /// <summary>
    /// HTTP请求响应对象
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// HTTP状态码
        /// </summary>
        [DisplayName("HTTP状态码")]
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// HTTP内容类型
        /// </summary>
        [DisplayName("内容类型")]
        public HttpContentType ContentType { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        public string Data { get; set; }

        /// <summary>
        /// 初始化HTTP请求响应对象
        /// </summary>
        /// <param name="statusCode">HTTP状态码</param>
        /// <param name="contentType">HTTP内容类型</param>
        /// <param name="data">数据</param>
        public HttpResponse(HttpStatusCode statusCode, HttpContentType contentType, string data)
        {
            StatusCode = statusCode;
            ContentType = contentType;
            Data = data;
        }
    }
}