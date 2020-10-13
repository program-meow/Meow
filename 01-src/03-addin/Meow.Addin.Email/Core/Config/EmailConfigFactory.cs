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
        public static IEmailConfig Create(Meow.Parameter.Enum.Email emailType)
        {
            return emailType switch
            {
                Meow.Parameter.Enum.Email.NetEase163 => new EmailConfigNetEase163(),
                _ => throw new Warning("暂不支持邮箱类型")
            };
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="emailAgreement">邮箱协议</param>
        /// <param name="emailConfig">邮箱配置</param>
        public Ip GetConfig(EmailAgreement emailAgreement, IEmailConfig emailConfig)
        {
            return emailAgreement switch
            {
                EmailAgreement.Smtp => emailConfig.Smtp(),
                EmailAgreement.Pop3 => emailConfig.Pop3(),
                EmailAgreement.Imap => emailConfig.Imap(),
                _ => throw new Warning("邮箱协议类型不支持")
            };
        }
    }
}