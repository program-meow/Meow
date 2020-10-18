using System;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Meow.Extension.Helper;
using Meow.Extension.Mathematics;

namespace Meow.Http.Core
{
    /// <summary>
    /// Http请求配置
    /// </summary>
    /// <typeparam name="TRequest">请求对象</typeparam>
    public abstract class RequestConfig<TRequest> : IRequestConfig<TRequest> where TRequest : IRequestConfig<TRequest>
    {
        #region 基础

        /// <summary>
        /// 返回自身
        /// </summary>
        private TRequest This()
        {
            return (TRequest)(object)this;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 期望接受数据类型：获取或设置期望接受数据类型HTTP标头的值
        /// </summary>
        protected string Accept { get; set; }
        /// <summary>
        /// 地址：获取实际响应请求的Internet资源的统一资源标识符（URI）
        /// </summary>
        protected Uri Address { get; set; }
        /// <summary>
        /// 允许自动重定向：获取或设置一个值，该值指示请求是否应遵循重定向响应
        /// </summary>
        protected bool AllowAutoRedirect { get; set; }
        /// <summary>
        /// 允许读取流缓冲：获取或设置一个值，该值指示是否缓冲从Internet资源接收的消息
        /// </summary>
        protected bool AllowReadStreamBuffering { get; set; }
        /// <summary>
        /// 允许写入流缓冲：获取或设置一个值，该值指示是否缓冲发送到Internet资源的数据
        /// </summary>
        protected bool AllowWriteStreamBuffering { get; set; }
        /// <summary>
        /// 认证等级：获取或设置值，该值指示此请求使用的身份验证和模拟级别
        /// </summary>
        protected AuthenticationLevel AuthenticationLevel { get; set; }
        /// <summary>
        /// 自动压缩解压缩：获取或设置使用的压缩解压缩类型
        /// </summary>
        protected DecompressionMethods AutomaticDecompression { get; set; }
        /// <summary>
        /// 缓存策略：获取或设置此请求的缓存策略
        /// </summary>
        protected RequestCachePolicy CachePolicy { get; set; }
        /// <summary>
        /// 客户端证书：获取或设置与此请求关联的安全证书的集合
        /// </summary>
        protected X509CertificateCollection ClientCertificates { get; set; }
        /// <summary>
        /// 连接：获取或设置连接HTTP标头的值
        /// </summary>
        protected string Connection { get; set; }
        /// <summary>
        /// 连接组名称：获取或设置请求的连接组的名称
        /// </summary>
        protected string ConnectionGroupName { get; set; }








        #endregion









        /// <summary>
        /// 内容长度：获取或设置内容长度HTTP标头
        /// </summary>
        protected long ContentLength { get; set; } = -1;
        /// <summary>
        /// 内容类型：获取或设置内容类型HTTP标头的值
        /// </summary>
        protected string ContentType { get; set; }
        /// <summary>
        /// 继续委托：获取或设置从Internet资源接收到HTTP 100继续响应时调用的委托方法
        /// </summary>
        protected HttpContinueDelegate ContinueDelegate { get; set; }
        /// <summary>
        /// 继续超时：获取或设置一个超时（以毫秒为单位），以等待从服务器收到100-Continue
        /// </summary>
        protected int ContinueTimeout { get; set; }
        /// <summary>
        /// Cookie：获取或设置与请求关联的cookie
        /// </summary>
        protected CookieContainer CookieContainer { get; set; }
        /// <summary>
        /// 创建者实例：在子孙类中重写时，获取从IWebRequestCreate类派生的工厂对象，该类用于创建实例化WebRequest的WebRequest，以向指定URI发出请求
        /// </summary>
        [Obsolete("该API支持.NET Framework基础结构，不能直接在您的代码中使用", true)]
        protected IWebRequestCreate CreatorInstance { get; set; }
        /// <summary>
        /// 证书：获取或设置请求的身份验证信息
        /// </summary>
        protected ICredentials Credentials { get; set; }
        /// <summary>
        /// 日期：获取或设置Date要在HTTP请求中使用的HTTP标头值
        /// </summary>
        protected DateTime Date { get; set; }
        /// <summary>
        /// 默认缓存策略：获取或设置此请求的默认缓存策略
        /// </summary>
        protected RequestCachePolicy DefaultCachePolicy { get; set; }
        /// <summary>
        /// 默认最大错误响应长度：获取或设置HTTP错误响应的默认最大长度
        /// </summary>
        protected int DefaultMaximumErrorResponseLength { get; set; }
        /// <summary>
        /// 默认最大响应标题长度：获取或设置默认最大响应标题长度属性的默认值
        /// </summary>
        protected int DefaultMaximumResponseHeadersLength { get; set; }
        /// <summary>
        /// 期望：获取或设置期望HTTP标头的值
        /// </summary>
        protected string Expect { get; set; }
        /// <summary>
        /// 有回应：获取一个值，该值指示是否已从Internet资源接收到响应
        /// </summary>
        protected bool HaveResponse { get; set; }
        /// <summary>
        /// 标头：指定组成HTTP标头的名称/值对的集合
        /// </summary>
        protected WebHeaderCollection Headers { get; set; }
        /// <summary>
        /// 主机：获取或设置主机标头值，以独立于请求URI在HTTP请求中使用
        /// </summary>
        protected string Host { get; set; }
        /// <summary>
        /// 如果从属性修改：获取或设置如果从属性修改HTTP标头的值
        /// </summary>
        protected DateTime IfModifiedSince { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 模拟级别：获取或设置当前请求的模拟级别
        /// </summary>
        protected TokenImpersonationLevel ImpersonationLevel { get; set; }
        /// <summary>
        /// 保持活力：获取或设置一个值，该值指示是否与Internet资源建立持久连接
        /// </summary>
        protected bool KeepAlive { get; set; }
        /// <summary>
        /// 最大自动重定向：获取或设置请求遵循的最大重定向数
        /// </summary>
        protected int MaximumAutomaticRedirections { get; set; }
        /// <summary>
        /// 最大响应标题长度：获取或设置响应头的最大允许长度
        /// </summary>
        protected int MaximumResponseHeadersLength { get; set; }
        /// <summary>
        /// 媒体类型：获取或设置请求的媒体类型
        /// </summary>
        protected string MediaType { get; set; }
        /// <summary>
        /// 方法：获取或设置请求的方法
        /// </summary>
        protected string Method { get; set; }
        /// <summary>
        /// 流水线：获取或设置一个值，该值指示是否将请求管道传输到Internet资源
        /// </summary>
        protected bool Pipelined { get; set; }
        /// <summary>
        /// 预认证：获取或设置一个值，该值指示是否与请求一起发送授权标头
        /// </summary>
        protected bool PreAuthenticate { get; set; }
        /// <summary>
        /// 协议版本：获取或设置用于请求的HTTP版本
        /// </summary>
        protected Version ProtocolVersion { get; set; }
        /// <summary>
        /// 代理：获取或设置请求的代理信息
        /// </summary>
        protected IWebProxy Proxy { get; set; }
        /// <summary>
        /// 读写超时：在写入或读取流时获取或设置超时（以毫秒为单位）
        /// </summary>
        protected int ReadWriteTimeout { get; set; }
        /// <summary>
        /// 推荐人：获取或设置推荐人HTTP标头的值
        /// </summary>
        protected string Referer { get; set; }
        /// <summary>
        /// 请求地址：获取请求的原始统一资源标识符（URI）
        /// </summary>
        protected Uri RequestUri { get; set; }
        /// <summary>
        /// 发送块：获取或设置一个值，该值指示是否将分段数据发送到Internet资源
        /// </summary>
        protected bool SendChunked { get; set; }
        /// <summary>
        /// 服务器证书验证回调：获取或设置一个回调函数来验证服务器证书
        /// </summary>
        protected RemoteCertificateValidationCallback ServerCertificateValidationCallback { get; set; }
        /// <summary>
        /// 服务点：获取用于请求的服务点
        /// </summary>
        protected ServicePoint ServicePoint { get; set; }
        /// <summary>
        /// 支持Cookie容器：获取一个值，该值指示请求是否提供对Cookie容器的支持
        /// </summary>
        protected bool SupportsCookieContainer { get; set; }
        /// <summary>
        /// 超时：获取或设置GetResponse（）和GetRequestStream（）方法的超时值（以毫秒为单位）
        /// </summary>
        protected int Timeout { get; set; }
        /// <summary>
        /// 传输编码：获取或设置传输编码HTTP标头的值
        /// </summary>
        protected string TransferEncoding { get; set; }
        /// <summary>
        /// 不安全的身份验证连接共享：获取或设置一个值，该值指示是否允许高速NTLM身份验证的连接共享
        /// </summary>
        protected bool UnsafeAuthenticatedConnectionSharing { get; set; }
        /// <summary>
        /// 使用默认凭证：获取或设置一个布尔值，该值控制是否随请求一起发送默认凭据
        /// </summary>
        protected bool UseDefaultCredentials { get; set; }
        /// <summary>
        /// 用户代理：获取或设置用户代理HTTP标头的值
        /// </summary>
        protected string UserAgent { get; set; }


        /// <summary>
        /// 初始化Http请求配置
        /// </summary>
        protected RequestConfig()
        {
            Accept = null;
            Address = null;
            AllowAutoRedirect = true;
            AllowReadStreamBuffering = false;
            AllowWriteStreamBuffering = true;
            AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
            AutomaticDecompression = DecompressionMethods.None;
            CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);
            ClientCertificates = new X509CertificateCollection();
            Connection = null;
            ConnectionGroupName = null;











        }



        #region 配置

        /// <summary>
        /// 配置期望接受数据类型
        /// </summary>
        /// <param name="accept">期望接受数据类型</param>
        public TRequest ConfigAccept(string accept)
        {
            if (accept.IsEmpty())
                return This();
            Accept = accept;
            return This();
        }

        /// <summary>
        /// 配置实际响应请求的Internet资源的统一资源标识符（URI）
        /// </summary>
        /// <param name="address">实际响应请求的Internet资源的统一资源标识符（URI）</param>
        public TRequest ConfigAddress(string address)
        {
            if (address.IsUrl())
                return This();
            Address = new Uri(address);
            return This();
        }

        /// <summary>
        /// 配置允许自动重定向
        /// </summary>
        /// <param name="allowAutoRedirect">是否允许自动重定向</param>
        public TRequest ConfigAllowAutoRedirect(bool allowAutoRedirect)
        {
            AllowAutoRedirect = allowAutoRedirect;
            return This();
        }

        /// <summary>
        /// 配置允许读取流缓冲
        /// </summary>
        /// <param name="allowReadStreamBuffering">是否允许读取流缓冲</param>
        public TRequest ConfigAllowReadStreamBuffering(bool allowReadStreamBuffering)
        {
            AllowReadStreamBuffering = allowReadStreamBuffering;
            return This();
        }

        /// <summary>
        /// 配置允许写入流缓冲
        /// </summary>
        /// <param name="allowWriteStreamBuffering">是否允许写入流缓冲</param>
        public TRequest ConfigAllowWriteStreamBuffering(bool allowWriteStreamBuffering)
        {
            AllowWriteStreamBuffering = allowWriteStreamBuffering;
            return This();
        }

        /// <summary>
        /// 配置身份验证认证等级
        /// </summary>
        /// <param name="authenticationLevel">身份验证认证等级</param>
        public TRequest ConfigAuthenticationLevel(AuthenticationLevel authenticationLevel)
        {
            AuthenticationLevel = authenticationLevel;
            return This();
        }

        /// <summary>
        /// 配置自动压缩解压缩类型
        /// </summary>
        /// <param name="automaticDecompression">自动压缩解压缩类型</param>
        public TRequest ConfigAutomaticDecompression(DecompressionMethods automaticDecompression)
        {
            AutomaticDecompression = automaticDecompression;
            return This();
        }

        /// <summary>
        /// 配置缓存策略
        /// </summary>
        /// <param name="cacheLevel">缓存级别</param>
        public TRequest ConfigCachePolicy(HttpRequestCacheLevel cacheLevel)
        {
            CachePolicy = new HttpRequestCachePolicy(cacheLevel);
            return This();
        }

        /// <summary>
        /// 配置添加客户端安全证书
        /// </summary>
        /// <param name="certificatePath">客户端安全证书路径</param>
        public TRequest ConfigAddClientCertificate(string certificatePath)
        {
            if (certificatePath.IsEmpty())
                return This();
            ClientCertificates.Add(new X509Certificate2(certificatePath));
            return This();
        }

        /// <summary>
        /// 配置添加客户端安全证书
        /// </summary>
        /// <param name="certificatePath">客户端安全证书路径</param>
        /// <param name="certificatePassword">客户端安全证书密码</param>
        public TRequest ConfigAddClientCertificate(string certificatePath, string certificatePassword)
        {
            if (certificatePath.IsEmpty())
                return This();
            if (certificatePassword.IsEmpty())
                return ConfigAddClientCertificate(certificatePath);
            ClientCertificates.Add(new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));
            return This();
        }

        /// <summary>
        /// 配置连接
        /// </summary>
        /// <param name="connection">连接</param>
        public TRequest ConfigConnection(string connection)
        {
            if (connection.IsEmpty())
                return This();
            Connection = connection;
            return This();
        }

        /// <summary>
        /// 配置连接连接组名称
        /// </summary>
        /// <param name="connectionGroupName">连接连接组名称</param>
        public TRequest ConfigConnectionGroupName(string connectionGroupName)
        {
            if (connectionGroupName.IsEmpty())
                return This();
            ConnectionGroupName = connectionGroupName.Sha256();
            return This();
        }









        #endregion

    }
}