using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityModel.Client;
using Meow.Extension.Helper;
using Meow.Extension.Parameter.Enum;
using Meow.Helper;
using Meow.Parameter.Response;
using HttpContentTypeEnum = Meow.Parameter.Enum.HttpContentType;
using HttpContentTypeHelper = Meow.Helper.HttpContentType;

namespace Meow.Http.Core
{
    /// <summary>
    /// HttpClient请求实现
    /// </summary>
    /// <typeparam name="TResult">请求结果类型</typeparam>
    public class HttpClientRequestImp<TResult> : HttpRequestBase<IHttpClientRequest<TResult>, TResult>, IHttpClientRequest<TResult>
    {
        #region 基础字段

        /// <summary>
        /// Http动词
        /// </summary>
        private readonly HttpMethod _httpMethod;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化Http请求
        /// </summary>
        /// <param name="httpMethod">Http动词</param>
        /// <param name="url">地址</param>
        public HttpClientRequestImp(HttpMethod httpMethod, string url) : base(url)
        {
            _httpMethod = httpMethod;
        }

        #endregion

        #region 发送请求

        /// <summary>
        /// 发送请求
        /// </summary>
        protected override async Task<HttpResponse> SendAsync()
        {
            var client = CreateHttpClient();
            InitHttpClient(client);
            var response = await client.SendAsync(CreateRequestMessage());
            var result = await response.Content.ReadAsStringAsync();
            return new HttpResponse(response.StatusCode, HttpContentTypeHelper.ToEnum(GetContentType(response)), result);
        }

        /// <summary>
        /// 获取内容类型
        /// </summary>
        private string GetContentType(HttpResponseMessage response)
        {
            return response?.Content?.Headers?.ContentType == null ? string.Empty : response.Content.Headers.ContentType.MediaType;
        }

        /// <summary>
        /// 创建Http客户端
        /// </summary>
        private HttpClient CreateHttpClient()
        {
            return new HttpClient(CreateHttpClientHandler());
        }

        /// <summary>
        /// 创建Http客户端处理器
        /// </summary>
        private HttpClientHandler CreateHttpClientHandler()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                ServerCertificateCustomValidationCallback = _serverCertificateCustomValidationCallback
            };
            if (_certificatePath.IsEmpty())
                return handler;
            var certificate = new X509Certificate2(_certificatePath, _certificatePassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            handler.ClientCertificates.Add(certificate);
            return handler;
        }

        /// <summary>
        /// 初始化Http客户端
        /// </summary>
        /// <param name="client">Http客户端</param>
        private void InitHttpClient(HttpClient client)
        {
            client.Timeout = _timeout;
            if (_token.IsEmpty())
                return;
            client.SetBearerToken(_token);
        }

        /// <summary>
        /// 创建请求消息
        /// </summary>
        private HttpRequestMessage CreateRequestMessage()
        {
            var message = new HttpRequestMessage
            {
                Method = _httpMethod,
                RequestUri = new Uri(_url),
                Content = CreateHttpContent()
            };
            foreach (var header in _headers)
                message.Headers.Add(header.Key, header.Value);
            return message;
        }

        /// <summary>
        /// 创建请求内容
        /// </summary>
        private HttpContent CreateHttpContent()
        {
            return _contentType switch
            {
                HttpContentTypeEnum.FormData => new FormUrlEncodedContent(_params.ToDictionary(t => t.Text, t => t.Value.SafeString())),
                HttpContentTypeEnum.FormFile => throw new NotImplementedException("未实现该ContentType"),
                HttpContentTypeEnum.Json => CreateJsonContent(),
                HttpContentTypeEnum.Xml => CreateXmlContent(),
                _ => throw new NotImplementedException("未实现该ContentType")
            };
        }

        /// <summary>
        /// 创建json内容
        /// </summary>
        private HttpContent CreateJsonContent()
        {
            if (_data.IsEmpty())
                _data = Json.ToJson(_params);
            return new StringContent(_data, _encoding, HttpContentTypeEnum.Json.Label());
        }

        /// <summary>
        /// 创建xml内容
        /// </summary>
        private HttpContent CreateXmlContent()
        {
            return new StringContent(_data, _encoding, HttpContentTypeEnum.Xml.Label());
        }

        #endregion
    }
}