namespace Meow.Http.Core
{
    /// <summary>
    /// HttpClient请求
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    public interface IHttpClientRequest<out TRequest> : IRequest<TRequest> where TRequest : IHttpClientRequest<TRequest>
    {

    }
}