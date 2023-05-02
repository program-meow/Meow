using System;
using System.Net.Http.Headers;

namespace Meow.Http
{
    /// <summary>
    /// HTTP标准请求头
    /// </summary>
    public class HttpStandardHeader
    {
        #region 设置接受参数

        /// <summary>
        /// 设置接受的内容类型
        /// </summary>
        public string Accept { get; set; }
        /// <summary>
        /// 设置接受的字符编码
        /// </summary>
        public string AcceptCharset { get; set; }
        /// <summary>
        /// 设置接受的编码格式
        /// </summary>
        public string AcceptEncoding { get; set; }
        /// <summary>
        /// 设置接受的语言
        /// </summary>
        public string AcceptLanguage { get; set; }

        #endregion

        /// <summary>
        /// 设置请求响应链上所有的缓存机制必须遵守的指令
        /// </summary>
        public CacheControlHeaderValue CacheControl { get; set; }
        /// <summary>
        /// 设置当前连接和hop-by-hop协议请求字段列表的控制选项
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 设置消息发送的日期和时间
        /// </summary>
        public DateTimeOffset? Date { get; set; }
        /// <summary>
        /// 标识客户端需要的特殊浏览器行为
        /// </summary>
        public NameValueWithParametersHeaderValue Expect { get; set; }
        /// <summary>
        /// 设置发送请求的用户的email地址
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 设置服务器域名和TCP端口号，如果使用的是服务请求标准端口号，端口号可以省略
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 设置客户端的ETag,当时客户端ETag和服务器生成的ETag一致才执行，适用于更新自从上次更新之后没有改变的资源
        /// </summary>
        public EntityTagHeaderValue IfMatch { get; set; }
        /// <summary>
        /// 设置更新时间，从更新时间到服务端接受请求这段时间内如果资源没有改变，允许服务端返回304 Not Modified
        /// </summary>
        public DateTimeOffset? IfModifiedSince { get; set; }
        /// <summary>
        /// 设置客户端ETag，如果和服务端接受请求生成的ETage相同，允许服务端返回304 Not Modified
        /// </summary>
        public EntityTagHeaderValue IfNoneMatch { get; set; }
        /// <summary>
        /// 设置客户端ETag，如果和服务端接受请求生成的ETage相同，返回缺失的实体部分；否则返回整个新的实体
        /// </summary>
        public RangeConditionHeaderValue IfRange { get; set; }
        /// <summary>
        /// 设置更新时间，只有从更新时间到服务端接受请求这段时间内实体没有改变，服务端才会发送响应
        /// </summary>
        public DateTimeOffset? IfUnmodifiedSince { get; set; }
        /// <summary>
        /// 限制代理或网关转发消息的次数
        /// </summary>
        public int? MaxForwards { get; set; }
        /// <summary>
        /// 设置特殊实现字段，可能会对请求响应链有多种影响
        /// </summary>
        public NameValueHeaderValue Pragma { get; set; }
        /// <summary>
        /// 为连接代理授权认证信息
        /// </summary>
        public AuthenticationHeaderValue ProxyAuthorization { get; set; }
        /// <summary>
        /// 请求部分实体，设置请求实体的字节数范围，具体可以参见HTTP/1.1中的Byte serving
        /// </summary>
        public RangeHeaderValue Range { get; set; }
        /// <summary>
        /// 设置前一个页面的地址，并且前一个页面中的连接指向当前请求，意思就是如果当前请求是在A页面中发送的，那么referer就是A页面的url地址
        /// （轶事：这个单词正确的拼法应该是"referrer",但是在很多规范中都拼成了"referer"，所以这个单词也就成为标准用法）
        /// </summary>
        public Uri Referrer { get; set; }
        /// <summary>
        /// 设置用户代理期望接受的传输编码格式，和响应头中的Transfer-Encoding字段一样
        /// </summary>
        public TransferCodingWithQualityHeaderValue TE { get; set; }
        /// <summary>
        /// 请求服务端升级协议
        /// </summary>
        public ProductHeaderValue Upgrade { get; set; }
        /// <summary>
        /// 用户代理的字符串值
        /// </summary>
        public ProductInfoHeaderValue UserAgent { get; set; }
        /// <summary>
        /// 通知服务器代理请求
        /// </summary>
        public ViaHeaderValue Via { get; set; }
        /// <summary>
        /// 实体可能会发生的问题的通用警告
        /// </summary>
        public WarningHeaderValue Warning { get; set; }

    }
}
