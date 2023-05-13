using System.Net.Http;
using Meow.Http;

namespace Meow.Helper
{
    /// <summary>
    /// Http操作
    /// </summary>
    public static class Http
    {
        #region Client(Http客户端)

        /// <summary>
        /// Http客户端
        /// </summary>
        public static IHttpClient Client
        {
            get
            {
                try
                {
                    return Ioc.Create<IHttpClient>();
                }
                catch
                {
                    return new HttpClientService().SetHttpClientHandler(new HttpClientHandler());
                }
            }
        }

        #endregion
    }
}
