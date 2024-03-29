﻿namespace Meow.Http;

/// <summary>
/// Http客户端服务
/// </summary>
public class HttpClientService : IHttpClient {

    #region 字段

    /// <summary>
    /// Http客户端工厂
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;
    /// <summary>
    /// Http客户端
    /// </summary>
    private HttpClient _httpClient;

    #endregion

    #region 构造方法

    /// <summary>
    /// 初始化Http客户端服务
    /// </summary>
    /// <param name="factory">Http客户端工厂</param>
    public HttpClientService( IHttpClientFactory factory = null ) {
        _httpClientFactory = factory;
    }

    #endregion

    #region SetHttpClient  [设置Http客户端]

    /// <summary>
    /// 设置Http客户端
    /// </summary>
    /// <param name="client">Http客户端</param>
    public HttpClientService SetHttpClient( HttpClient client ) {
        _httpClient = client;
        return this;
    }

    #endregion

    #region Get

    /// <summary>
    /// 发送Get请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<string> Get( string url ) {
        return Get<string>( url );
    }

    /// <summary>
    /// 发送Get请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<TResult> Get<TResult>( string url ) where TResult : class {
        return new HttpRequest<TResult>( _httpClientFactory , _httpClient , HttpMethod.Get , url );
    }

    #endregion

    #region Post

    /// <summary>
    /// 发送Post请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<string> Post( string url ) {
        return Post<string>( url );
    }

    /// <summary>
    /// 发送Post请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<TResult> Post<TResult>( string url ) where TResult : class {
        return new HttpRequest<TResult>( _httpClientFactory , _httpClient , HttpMethod.Post , url );
    }

    #endregion

    #region Put

    /// <summary>
    /// 发送Put请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<string> Put( string url ) {
        return Put<string>( url );
    }

    /// <summary>
    /// 发送Put请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<TResult> Put<TResult>( string url ) where TResult : class {
        return new HttpRequest<TResult>( _httpClientFactory , _httpClient , HttpMethod.Put , url );
    }

    #endregion

    #region Delete

    /// <summary>
    /// 发送Delete请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<string> Delete( string url ) {
        return Delete<string>( url );
    }

    /// <summary>
    /// 发送Delete请求
    /// </summary>
    /// <param name="url">服务地址</param>
    public IHttpRequest<TResult> Delete<TResult>( string url ) where TResult : class {
        return new HttpRequest<TResult>( _httpClientFactory , _httpClient , HttpMethod.Delete , url );
    }

    #endregion

}