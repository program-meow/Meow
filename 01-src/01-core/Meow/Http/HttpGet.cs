using System.Net.Http;
using Meow.Http.Core;

namespace Meow.Http
{
    /// <summary>
    /// HttpGet请求
    /// </summary>
    public class HttpGet : HttpRequestBase<HttpGet>, IRequest<HttpGet>
    {
        /// <summary>
        /// 初始化HttpGet请求
        /// </summary>
        /// <param name="url">地址</param>
        public HttpGet(string url) : base(HttpMethod.Get, url)
        {
        }
    }
}