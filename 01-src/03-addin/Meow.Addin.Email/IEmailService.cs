using System.Collections.Generic;
using System.Threading.Tasks;
using Meow.Aspect;
using Meow.Dependency;
using Meow.Parameter.Object;

namespace Meow.Addin.Email
{
    /// <summary>
    /// 邮箱服务
    /// </summary>
    public interface IEmailService : IScopeDependency
    {
        /// <summary>
        /// 添加接收邮箱
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="name">名称</param>
        void AddToEmail([NotEmpty] string address, string name = "");
        /// <summary>
        /// 添加接收邮箱集合
        /// </summary>
        /// <param name="toEmails">接收邮箱集合</param>
        void AddToEmails([NotNull]List<Parameter.Object.Email> toEmails);
        /// <summary>
        /// 添加抄送邮箱
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="name">名称</param>
        void AddCcEmail([NotEmpty]string address, string name = "");
        /// <summary>
        /// 添加抄送邮箱集合
        /// </summary>
        /// <param name="toEmails">抄送邮箱集合</param>
        void AddCcEmails([NotNull]List<Parameter.Object.Email> toEmails);
        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message">消息</param>
        void Message([NotNull]Message message);
        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        Result Send();
        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        Task<Result> SendAsync();
    }
}