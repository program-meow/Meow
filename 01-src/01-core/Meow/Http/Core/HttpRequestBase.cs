using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Meow.Extension.Helper;
using Meow.Helper;
using Meow.Parameter.Object;
using DateTime = System.DateTime;
using HttpContentType = Meow.Parameter.Enum.HttpContent;

namespace Meow.Http.Core
{
    /// <summary>
    /// Http请求
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    public abstract class HttpRequestBase<TRequest> where TRequest : IRequest<TRequest>
    {
        #region 基础字段

        /// <summary>
        /// 地址
        /// </summary>
        private readonly string _url;
        /// <summary>
        /// Http动词
        /// </summary>
        private readonly HttpMethod _httpMethod;
        /// <summary>
        /// 字符编码
        /// </summary>
        private Encoding _encoding;
        /// <summary>
        /// 内容类型
        /// </summary>
        private string _contentType;
        /// <summary>
        /// Cookie容器
        /// </summary>
        private readonly CookieContainer _cookieContainer;
        /// <summary>
        /// 超时时间
        /// </summary>
        private TimeSpan _timeout;
        /// <summary>
        /// 请求头集合
        /// </summary>
        private readonly Dictionary<string, string> _headers;
        /// <summary>
        /// 执行失败的回调函数
        /// </summary>
        private Action<string> _failAction;
        /// <summary>
        /// 执行失败的回调函数
        /// </summary>
        private Action<string, HttpStatusCode> _failStatusCodeAction;
        /// <summary>
        /// ssl证书验证委托
        /// </summary>
        private Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> _serverCertificateCustomValidationCallback;
        /// <summary>
        /// 令牌
        /// </summary>
        private string _token;
        /// <summary>
        /// 证书路径
        /// </summary>
        private string _certificatePath;
        /// <summary>
        /// 证书密码
        /// </summary>
        private string _certificatePassword;

        #endregion

        #region 数据字段

        /// <summary>
        /// Url参数
        /// </summary>
        private List<Item> _urlData;
        /// <summary>
        /// 正文参数
        /// </summary>
        private List<Item> _bodyData;
        /// <summary>
        /// 特殊数据
        /// </summary>
        private string _specialData;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化Http请求
        /// </summary>
        /// <param name="httpMethod">Http动词</param>
        /// <param name="url">地址</param>
        protected HttpRequestBase(HttpMethod httpMethod, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _url = url;
            _httpMethod = httpMethod;
            _contentType = HttpContentType.FormUrlEncoded.Description();
            _cookieContainer = new CookieContainer();
            _timeout = new TimeSpan(0, 0, 30);
            _headers = new Dictionary<string, string>();
            _encoding = System.Text.Encoding.UTF8;
            _urlData = new List<Item>();
            _bodyData = new List<Item>();
        }

        #endregion

        #region 基础配置

        /// <summary>
        /// 返回自身
        /// </summary>
        private TRequest This()
        {
            return (TRequest)(object)this;
        }

        /// <summary>
        /// 设置字符编码
        /// </summary>
        /// <param name="encoding">字符编码</param>
        public TRequest Encoding(Encoding encoding)
        {
            if (encoding.IsNull())
                return This();
            _encoding = encoding;
            return This();
        }

        /// <summary>
        /// 设置字符编码
        /// </summary>
        /// <param name="encoding">字符编码</param>
        public TRequest Encoding(string encoding)
        {
            if (encoding.IsEmpty())
                return This();
            return Encoding(System.Text.Encoding.GetEncoding(encoding));
        }

        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        public TRequest ContentType(HttpContentType contentType)
        {
            if (contentType.IsNull())
                return This();
            return ContentType(contentType.Description());
        }

        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        public TRequest ContentType(string contentType)
        {
            if (contentType.IsEmpty())
                return This();
            _contentType = contentType;
            return This();
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">有效时间，单位：天</param>
        public TRequest Cookie(string name, string value, double expiresDate)
        {
            if (name.IsEmpty() || value.IsEmpty())
                return This();
            return Cookie(name, value, null, null, DateTime.Now.AddDays(expiresDate));
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">到期时间</param>
        public TRequest Cookie(string name, string value, DateTime expiresDate)
        {
            if (name.IsEmpty() || value.IsEmpty())
                return This();
            return Cookie(name, value, null, null, expiresDate);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="path">源服务器URL子集</param>
        /// <param name="domain">所属域</param>
        /// <param name="expiresDate">到期时间</param>
        public TRequest Cookie(string name, string value, string path = "/", string domain = null, DateTime? expiresDate = null)
        {
            if (name.IsEmpty() || value.IsEmpty())
                return This();
            return Cookie(new Cookie(name, value, path, domain)
            {
                Expires = expiresDate ?? DateTime.Now.AddYears(1)
            });
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookie">cookie</param>
        public TRequest Cookie(Cookie cookie)
        {
            if (cookie.IsNull())
                return This();
            _cookieContainer.Add(new Uri(_url), cookie);
            return This();
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        /// <param name="timeout">超时时间</param>
        public TRequest Timeout(int timeout)
        {
            if (timeout.IsNull())
                return This();
            _timeout = new TimeSpan(0, 0, timeout);
            return This();
        }

        /// <summary>
        /// 请求头
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public TRequest Header<T>(string key, T value)
        {
            if (key.IsEmpty() || value.IsEmpty())
                return This();
            _headers.Add(key, value.SafeString());
            return This();
        }

        /// <summary>
        /// 请求失败回调函数
        /// </summary>
        /// <param name="action">执行失败的回调函数,参数为响应结果</param>
        public TRequest OnFail(Action<string> action)
        {
            if (action.IsNull())
                return This();
            _failAction = action;
            return This();
        }

        /// <summary>
        /// 请求失败回调函数
        /// </summary>
        /// <param name="action">执行失败的回调函数,第一个参数为响应结果，第二个参数为状态码</param>
        public TRequest OnFail(Action<string, HttpStatusCode> action)
        {
            if (action.IsNull())
                return This();
            _failStatusCodeAction = action;
            return This();
        }

        /// <summary>
        /// 忽略Ssl
        /// </summary>
        public TRequest IgnoreSsl()
        {
            _serverCertificateCustomValidationCallback = (a, b, c, d) => true;
            return This();
        }

        /// <summary>
        /// 设置Bearer令牌
        /// </summary>
        /// <param name="token">令牌</param>
        public TRequest BearerToken(string token)
        {
            if (token.IsEmpty())
                return This();
            _token = token;
            return This();
        }

        /// <summary>
        /// 设置证书
        /// </summary>
        /// <param name="path">证书路径</param>
        /// <param name="password">证书密码</param>
        public TRequest Certificate(string path, string password)
        {
            if (path.IsEmpty() || password.IsEmpty())
                return This();
            _certificatePath = path;
            _certificatePassword = password;
            return This();
        }

        #endregion

        #region 数据配置

        /// <summary>
        /// Url参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public TRequest UrlData(string key, object value)
        {
            if (key.IsEmpty() || value.IsEmpty())
                return This();
            _urlData.Add(new Item(key, value, _urlData.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        /// <summary>
        /// Url参数字典
        /// </summary>
        /// <param name="parameters">参数字典</param>
        public TRequest UrlData(IDictionary<string, object> parameters)
        {
            if (parameters.IsEmpty())
                return This();
            foreach (var item in parameters.Where(item => !item.Value.IsEmpty()))
                _urlData.Add(new Item(item.Key, item.Value, _urlData.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        /// <summary>
        /// Url参数对象
        /// </summary>
        /// <param name="value">参数字典</param>
        public TRequest UrlData<T>(T value) where T : class
        {
            if (value.IsNull())
                return This();
            var items = value.AnalyzingToItems();
            foreach (var item in items.Where(item => !item.Value.IsEmpty()))
                _urlData.Add(new Item(item.Text, item.Value, _urlData.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        /// <summary>
        /// Url参数集合对象
        /// </summary>
        /// <param name="value">参数字典</param>
        public TRequest UrlData<T>(IEnumerable<T> value) where T : class
        {
            if (value.IsEmpty())
                return This();
            var items = value.AnalyzingToItems();
            foreach (var item in items.Where(item => !item.Value.IsEmpty()))
                _urlData.Add(new Item(item.Text, item.Value, _urlData.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        #endregion


        #region SendAsync(发送请求)

        /// <summary>
        /// 发送请求
        /// </summary>
        protected async Task<HttpResponseMessage> SendAsync()
        {
            var client = CreateHttpClient();
            InitHttpClient(client);
            return await client.SendAsync(CreateRequestMessage());
        }

        /// <summary>
        /// 创建Http客户端
        /// </summary>
        protected virtual HttpClient CreateHttpClient()
        {
            return new HttpClient(CreateHttpClientHandler()) { Timeout = _timeout };
        }

        /// <summary>
        /// 创建Http客户端处理器
        /// </summary>
        protected HttpClientHandler CreateHttpClientHandler()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                ServerCertificateCustomValidationCallback = _serverCertificateCustomValidationCallback
            };
            if (string.IsNullOrWhiteSpace(_certificatePath))
                return handler;
            var certificate = new X509Certificate2(_certificatePath, _certificatePassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            handler.ClientCertificates.Add(certificate);
            return handler;
        }

        /// <summary>
        /// 初始化Http客户端
        /// </summary>
        /// <param name="client">Http客户端</param>
        protected virtual void InitHttpClient(HttpClient client)
        {
            InitToken();
            if (string.IsNullOrWhiteSpace(_token))
                return;
            client.SetBearerToken(_token);
        }

        /// <summary>
        /// 初始化访问令牌
        /// </summary>
        protected virtual void InitToken()
        {
            if (string.IsNullOrWhiteSpace(_token) == false)
                return;
            _token = Web.AccessToken;
        }

        /// <summary>
        /// 创建请求消息
        /// </summary>
        protected virtual HttpRequestMessage CreateRequestMessage()
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
            var contentType = _contentType.SafeString().ToLower();
            switch (contentType)
            {
                case "application/x-www-form-urlencoded":
                    return new FormUrlEncodedContent(_bodyData.ToDictionary(t => t.Text, t => t.Value.SafeString()));
                case "application/json":
                    return CreateJsonContent();
                case "text/xml":
                    return CreateXmlContent();
            }
            throw new NotImplementedException("未实现该ContentType");
        }

        /// <summary>
        /// 创建json内容
        /// </summary>
        private HttpContent CreateJsonContent()
        {
            if (_specialData.IsEmpty())
                _specialData = Json.ToJson(_bodyData);
            return new StringContent(_specialData, _encoding, "application/json");
        }

        /// <summary>
        /// 创建xml内容
        /// </summary>
        private HttpContent CreateXmlContent()
        {
            return new StringContent(_specialData, _encoding, "text/xml");
        }

        #endregion

    }
}