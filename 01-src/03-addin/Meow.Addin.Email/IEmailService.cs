using Meow.Aspect;
using Meow.Dependency;
using Meow.Parameter.Object;
using System.Threading.Tasks;
using System.Collections.Generic;
using Meow.Addin.Email.Core.Parameter;

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
        /// <param name="toEmailAddress">接收邮箱地址</param>
        /// <param name="name">名称</param>
        IEmailService AddToEmail([NotEmpty] string toEmailAddress, string name = "");
        /// <summary>
        /// 添加接收邮箱集合
        /// </summary>
        /// <param name="toEmailAddress">接收邮箱地址集合</param>
        IEmailService AddToEmail([NotNull] IEnumerable<string> toEmailAddress);
        /// <summary>
        /// 添加接收邮箱集合
        /// </summary>
        /// <param name="toEmails">接收邮箱集合</param>
        IEmailService AddToEmail([NotNull] IEnumerable<EmailToAccount> toEmails);
        /// <summary>
        /// 添加抄送邮箱
        /// </summary>
        /// <param name="ccEmailAddress">抄送邮箱地址</param>
        /// <param name="name">名称</param>
        IEmailService AddCcEmail([NotEmpty] string ccEmailAddress, string name = "");
        /// <summary>
        /// 添加抄送邮箱集合
        /// </summary>
        /// <param name="ccEmailAddress">抄送邮箱地址集合</param>
        IEmailService AddCcEmail([NotNull] IEnumerable<string> ccEmailAddress);
        /// <summary>
        /// 添加抄送邮箱集合
        /// </summary>
        /// <param name="ccEmails">抄送邮箱集合</param>
        IEmailService AddCcEmail([NotNull] IEnumerable<EmailToAccount> ccEmails);
        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message">消息</param>
        IEmailService Message([NotNull] Message message);
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