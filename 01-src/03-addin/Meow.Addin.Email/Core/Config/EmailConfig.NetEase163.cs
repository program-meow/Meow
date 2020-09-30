using Meow.Parameter.Object;

namespace Meow.Addin.Email.Core.Config
{
    /// <summary>
    /// 网易163邮箱配置
    /// </summary>
    public class EmailConfigNetEase163 : IEmailConfig
    {
        /// <summary>
        /// SMTP协议配置
        /// </summary>
        public Ip Smtp()
        {
            return new Ip
            {
                Address = "smtp.163.com",
                Port = 25,
            };
        }

        /// <summary>
        /// POP3协议配置
        /// </summary>
        public Ip Pop3()
        {
            return new Ip
            {
                Address = "pop.163.com",
                Port = 110,
            };
        }

        /// <summary>
        /// IMAP协议配置
        /// </summary>
        public Ip Imap()
        {
            return new Ip
            {
                Address = "imap.163.com",
                Port = 143,
            };
        }
    }
}