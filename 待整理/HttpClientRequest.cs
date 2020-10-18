using System.Net.Http;
using Meow.Http.Core;

namespace Meow.Http
{
    /// <summary>
    /// HttpClient请求
    /// </summary>
    /// <typeparam name="TResult">请求结果类型</typeparam>
    public class HttpClientRequest<TResult>
    {
        /// <summary>
        /// 初始化HttpClient请求
        /// </summary>
        public HttpClientRequest() { }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">地址</param>
        public IHttpClientRequest<TResult> Get(string url)
        {
            return new HttpClientRequestImp<TResult>(HttpMethod.Get, url);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">地址</param>
        public IHttpClientRequest<TResult> Post(string url)
        {
            return new HttpClientRequestImp<TResult>(HttpMethod.Post, url);
        }

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="url">地址</param>
        public IHttpClientRequest<TResult> Put(string url)
        {
            return new HttpClientRequestImp<TResult>(HttpMethod.Put, url);
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="url">地址</param>
        public IHttpClientRequest<TResult> Delete(string url)
        {
            return new HttpClientRequestImp<TResult>(HttpMethod.Delete, url);
        }
    }
}