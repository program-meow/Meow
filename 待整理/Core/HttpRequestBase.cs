﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Meow.Extension.Helper;
using Meow.Extension.Parameter.Object;
using Meow.Extension.Parameter.Response;
using Meow.Helper;
using Meow.Parameter.Object;
using Meow.Parameter.Response;
using DateTime = System.DateTime;
using HttpContentTypeEnum = Meow.Parameter.Enum.HttpContentType;

namespace Meow.Http.Core
{
    /// <summary>
    /// Http请求
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <typeparam name="TResult">请求结果类型</typeparam>
    public abstract class HttpRequestBase<TRequest, TResult> where TRequest : IHttpRequest<TRequest, TResult>
    {
        #region 基础字段

        /// <summary>
        /// 地址
        /// </summary>
        protected readonly string _url;
        /// <summary>
        /// 内容类型
        /// </summary>
        protected HttpContentTypeEnum? _contentType;
        /// <summary>
        /// 字符编码
        /// </summary>
        protected Encoding _encoding;
        /// <summary>
        /// 超时时间
        /// </summary>
        protected TimeSpan _timeout;
        /// <summary>
        /// Cookie容器
        /// </summary>
        protected readonly CookieContainer _cookieContainer;
        /// <summary>
        /// 请求头集合
        /// </summary>
        protected readonly Dictionary<string, string> _headers;
        /// <summary>
        /// 令牌
        /// </summary>
        protected string _token;
        /// <summary>
        /// ssl证书验证委托
        /// </summary>
        protected Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> _serverCertificateCustomValidationCallback;
        /// <summary>
        /// 证书路径
        /// </summary>
        protected string _certificatePath;
        /// <summary>
        /// 证书密码
        /// </summary>
        protected string _certificatePassword;

        #endregion

        #region 数据字段

        /// <summary>
        /// 参数
        /// </summary>
        protected List<Item> _params;
        /// <summary>
        /// 数据
        /// </summary>
        protected string _data;

        #endregion

        #region 回调函数

        /// <summary>
        /// 执行成功的回调函数
        /// </summary>
        protected Action<HttpResponse<TResult>> _okAction;
        /// <summary>
        /// 执行失败的回调函数
        /// </summary>
        protected Action<HttpResponse<TResult>> _errorAction;
        /// <summary>
        /// 执行成功的转换函数
        /// </summary>
        private Func<string, TResult> _convertAction;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化Http请求
        /// </summary>
        /// <param name="url">地址</param>
        protected HttpRequestBase(string url)
        {
            if (url.IsEmpty())
                throw new ArgumentNullException(nameof(url));
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _url = url;
            _contentType = HttpContentTypeEnum.FormData;
            _encoding = System.Text.Encoding.UTF8;
            _timeout = new TimeSpan(0, 0, 30);
            _cookieContainer = new CookieContainer();
            _headers = new Dictionary<string, string>();
            _token = Web.AccessToken;
            _params = new List<Item>();
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
        public TRequest ContentType(HttpContentTypeEnum contentType)
        {
            if (contentType.IsNull())
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
        /// 参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public TRequest Data(string key, object value)
        {
            if (key.IsEmpty() || value.IsEmpty())
                return This();
            _params.Add(new Item(key, value, _params.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        /// <summary>
        /// 参数字典
        /// </summary>
        /// <param name="parameters">参数字典</param>
        public TRequest Data(IDictionary<string, object> parameters)
        {
            if (parameters.IsEmpty())
                return This();
            foreach (var item in parameters.Effective())
                _params.Add(new Item(item.Key, item.Value, _params.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        /// <summary>
        /// 参数对象
        /// </summary>
        /// <param name="value">参数字典</param>
        public TRequest Data<T>(T value) where T : class
        {
            if (value.IsNull())
                return This();
            var items = value.AnalyzingToItems().Effective();
            foreach (var item in items)
                _params.Add(new Item(item.Text, item.Value, _params.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        /// <summary>
        /// 参数集合对象
        /// </summary>
        /// <param name="value">参数字典</param>
        public TRequest Data<T>(IEnumerable<T> value) where T : class
        {
            if (value.IsEmpty())
                return This();
            var items = value.AnalyzingToItems().Effective();
            foreach (var item in items)
                _params.Add(new Item(item.Text, item.Value, _params.Max(t => t.SortId).SafeValue() + 1));
            return This();
        }

        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="contentType">内容类型</param>
        /// <param name="value">值</param>
        public TRequest Data(HttpContentTypeEnum contentType, string value)
        {
            ContentType(contentType);
            if (value.IsEmpty())
                return This();
            _data = value;
            return This();
        }

        #endregion

        #region 回调函数配置

        /// <summary>
        /// 请求成功回调函数
        /// </summary>
        /// <param name="action">执行成功的回调函数,参数为响应结果</param>
        public TRequest Ok(Action<HttpResponse<TResult>> action)
        {
            if (action.IsNull())
                return This();
            _okAction = action;
            return This();
        }

        /// <summary>
        /// 请求失败回调函数
        /// </summary>
        /// <param name="action">执行失败的回调函数,参数为响应结果</param>
        public TRequest Error(Action<HttpResponse<TResult>> action)
        {
            if (action.IsNull())
                return This();
            _errorAction = action;
            return This();
        }

        /// <summary>
        /// 将结果字符串转换为指定类型函数，当默认转换实现无法转换时使用
        /// </summary>
        /// <param name="action">响应结果转换回调函数,参数为响应结果</param>
        public TRequest Convert(Func<string, TResult> action)
        {
            if (action.IsNull())
                return This();
            _convertAction = action;
            return This();
        }

        #endregion

        #region 获取结果

        /// <summary>
        /// 获取结果
        /// </summary>
        public HttpResponse<TResult> Result()
        {
            return Async.RunSync(ResultAsync);
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public async Task<HttpResponse<TResult>> ResultAsync()
        {
            SendBefore();
            var response = await SendAsync();
            var result = ConvertTo(response);
            SendAfter(result);
            return result;
        }

        /// <summary>
        /// 将结果字符串转换为指定类型
        /// </summary>
        private HttpResponse<TResult> ConvertTo(HttpResponse response)
        {
            if (_convertAction.IsNotNull())
                return response.To(_convertAction(response.Data));
            return response.To<TResult>();
        }

        #endregion

        #region 发送请求

        #region 发送前操作

        /// <summary>
        /// 发送前操作
        /// </summary>
        protected virtual void SendBefore()
        {
        }

        #endregion

        /// <summary>
        /// 发送请求
        /// </summary>
        protected abstract Task<HttpResponse> SendAsync();

        #region 发送后操作

        /// <summary>
        /// 发送后操作
        /// </summary>
        protected virtual void SendAfter(HttpResponse<TResult> result)
        {
            OkHandler(result);
            ErrorHandler(result);
        }

        /// <summary>
        /// 成功处理操作
        /// </summary>
        protected virtual void OkHandler(HttpResponse<TResult> result)
        {
            if (result.Code != HttpStatusCode.OK)
                return;
            _okAction?.Invoke(result);
        }

        /// <summary>
        /// 失败处理操作
        /// </summary>
        protected virtual void ErrorHandler(HttpResponse<TResult> result)
        {
            if (result.Code == HttpStatusCode.OK)
                return;
            _errorAction?.Invoke(result);
        }
        #endregion

        #endregion
    }
}