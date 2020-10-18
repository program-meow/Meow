namespace Meow.Http.Core
{
    /// <summary>
    /// Http请求配置
    /// </summary>
    /// <typeparam name="TRequest">请求对象</typeparam>
    public interface IRequestConfig<TRequest> where TRequest : IRequestConfig<TRequest>
    {
     
    }
}