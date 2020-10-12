using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Meow.Aspect;
using Meow.Parameter.Enum;

namespace Meow.Http.Core
{
    /// <summary>
    /// Http请求
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    public interface IRequest<out TRequest> where TRequest : IRequest<TRequest>
    {
        #region 基础配置

        /// <summary>
        /// 设置字符编码
        /// </summary>
        /// <param name="encoding">字符编码</param>
        TRequest Encoding([NotNull] Encoding encoding);
        /// <summary>
        /// 设置字符编码
        /// </summary>
        /// <param name="encoding">字符编码</param>
        TRequest Encoding([NotEmpty] string encoding);
        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        TRequest ContentType([NotNull] HttpContent contentType);
        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        TRequest ContentType([NotEmpty] string contentType);
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">有效时间，单位：天</param>
        TRequest Cookie([NotEmpty] string name, string value, double expiresDate);
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">到期时间</param>
        TRequest Cookie([NotEmpty] string name, string value, DateTime expiresDate);
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="path">源服务器URL子集</param>
        /// <param name="domain">所属域</param>
        /// <param name="expiresDate">到期时间</param>
        TRequest Cookie([NotEmpty] string name, string value, string path = "/", string domain = null, DateTime? expiresDate = null);
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookie">cookie</param>
        TRequest Cookie([NotNull] Cookie cookie);
        /// <summary>
        /// 超时时间
        /// </summary>
        /// <param name="timeout">超时时间</param>
        TRequest Timeout([NotNull] int timeout);
        /// <summary>
        /// 请求头
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        TRequest Header<T>([NotEmpty] string key, T value);
        /// <summary>
        /// 请求失败回调函数
        /// </summary>
        /// <param name="action">执行失败的回调函数,参数为响应结果</param>
        TRequest OnFail([NotNull] Action<string> action);
        /// <summary>
        /// 请求失败回调函数
        /// </summary>
        /// <param name="action">执行失败的回调函数,第一个参数为响应结果，第二个参数为状态码</param>
        TRequest OnFail([NotNull] Action<string, HttpStatusCode> action);
        /// <summary>
        /// 忽略Ssl
        /// </summary>
        TRequest IgnoreSsl();
        /// <summary>
        /// 设置Bearer令牌
        /// </summary>
        /// <param name="token">令牌</param>
        TRequest BearerToken([NotEmpty] string token);
        /// <summary>
        /// 设置证书
        /// </summary>
        /// <param name="path">证书路径</param>
        /// <param name="password">证书密码</param>
        TRequest Certificate([NotEmpty] string path, [NotEmpty] string password);

        #endregion

        #region 数据配置

        /// <summary>
        /// Url参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        TRequest UrlData(string key, object value);
        /// <summary>
        /// Url参数字典
        /// </summary>
        /// <param name="parameters">参数字典</param>
        TRequest UrlData(IDictionary<string, object> parameters);
        /// <summary>
        /// Url参数对象
        /// </summary>
        /// <param name="value">参数字典</param>
        TRequest UrlData<T>(T value) where T : class;
        /// <summary>
        /// Url参数集合对象
        /// </summary>
        /// <param name="value">参数字典</param>
        TRequest UrlData<T>(IEnumerable<T> value) where T : class;

        #endregion

        #region ResultAsync(获取结果)

        /// <summary>
        /// 获取结果
        /// </summary>
        string Result();
        /// <summary>
        /// 获取结果
        /// </summary>
        Task<string> ResultAsync();

        #endregion

    }
}