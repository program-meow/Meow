using System.Threading.Tasks;
using Meow.Dependency;
using Meow.Parameter.Response;

namespace Meow.Addin.Sample.Service.Abstractions
{
    /// <summary>
    /// 邮箱演示服务
    /// </summary>
    public interface IEmailSampleService : IScopeDependency
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        ResultResponse Send();
        /// <summary>
        /// 发送邮件
        /// </summary>
        Task<ResultResponse> SendAsync();
    }
}
