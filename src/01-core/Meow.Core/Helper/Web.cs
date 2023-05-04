using System.Net.Http;
using Meow.Http;

namespace Meow.Helper
{
    /// <summary>
    /// Web操作
    /// </summary>
    public static class Web
    {
        #region Client(Http客户端)

        /// <summary>
        /// Http客户端服务
        /// </summary>
        public static IHttpClient HttpClientService => Ioc.Create<IHttpClient>();
        /// <summary>
        /// Http客户端
        /// </summary>
        public static IHttpClient HttpClient => new HttpClientService().SetHttpClientHandler(new HttpClientHandler());

        #endregion
    }
}
