namespace Meow.Http;

/// <summary>
/// Http客户端
/// </summary>
public interface IHttpClient {
    /// <summary>
    /// 发送Get请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<string> Get( string url );
    /// <summary>
    /// 发送Get请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<TResult> Get<TResult>( string url ) where TResult : class;
    /// <summary>
    /// 发送Post请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<string> Post( string url );
    /// <summary>
    /// 发送Post请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<TResult> Post<TResult>( string url ) where TResult : class;
    /// <summary>
    /// 发送Put请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<string> Put( string url );
    /// <summary>
    /// 发送Put请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<TResult> Put<TResult>( string url ) where TResult : class;
    /// <summary>
    /// 发送Delete请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<string> Delete( string url );
    /// <summary>
    /// 发送Delete请求
    /// </summary>
    /// <param name="url">服务地址</param>
    IHttpRequest<TResult> Delete<TResult>( string url ) where TResult : class;
}