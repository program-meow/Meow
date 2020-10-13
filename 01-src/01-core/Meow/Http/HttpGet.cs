using System.Net.Http;
using Meow.Http.Core;

namespace Meow.Http
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpGet:HttpClientRequest<HttpGet>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public HttpGet(string url) : base(HttpMethod.Get, url)
        {
        }
    }
}
