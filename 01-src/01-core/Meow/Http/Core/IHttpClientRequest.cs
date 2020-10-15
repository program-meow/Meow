namespace Meow.Http.Core
{
    /// <summary>
    /// HttpClient请求
    /// </summary>
    /// <typeparam name="TResult">请求结果类型</typeparam>
    public interface IHttpClientRequest<TResult> : IHttpRequest<IHttpClientRequest<TResult>, TResult>
    {
    }
}