using Meow.Exception;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;

namespace Meow.Addin.Email.Core.Config
{
    /// <summary>
    /// 邮箱配置工厂
    /// </summary>
    public class EmailConfigFactory
    {
        /// <summary>
        /// 创建邮箱对象
        /// </summary>
        /// <param name="emailType">邮箱类型</param>
        public static IEmailConfig Create(Parameter.Enum.Email emailType)
        {
            switch (emailType)
            {
                case Parameter.Enum.Email.NetEase163:
                    return new EmailConfigNetEase163();
                default:
                    throw new Warning("暂不支持邮箱类型");
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="emailAgreement">邮箱协议</param>
        /// <param name="emailConfig">邮箱配置</param>
        public Ip GetConfig(EmailAgreement emailAgreement, IEmailConfig emailConfig)
        {
            switch (emailAgreement)
            {
                case EmailAgreement.Smtp:
                    return emailConfig.Smtp();
                case EmailAgreement.Pop3:
                    return emailConfig.Pop3();
                case EmailAgreement.Imap:
                    return emailConfig.Imap();
                default:
                    throw new Warning("邮箱协议类型不支持");
            }
        }
    }
}