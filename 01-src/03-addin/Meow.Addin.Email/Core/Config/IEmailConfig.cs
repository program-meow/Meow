using Meow.Parameter.Object;

namespace Meow.Addin.Email.Core.Config
{
    /// <summary>
    /// 邮箱配置
    /// </summary>
    public interface IEmailConfig
    {
        /// <summary>
        /// SMTP协议配置
        /// </summary>
        Ip Smtp();
        /// <summary>
        /// POP3协议配置
        /// </summary>
        Ip Pop3();
        /// <summary>
        /// IMAP协议配置
        /// </summary>
        Ip Imap();
    }
}
