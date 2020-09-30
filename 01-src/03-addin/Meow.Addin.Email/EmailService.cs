using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Meow.Addin.Email.Core.Config;
using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Extension.Validation;
using Meow.Parameter.Object;
using MimeKit;

namespace Meow.Addin.Email
{
    /// <summary>
    /// 邮箱服务
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// 短信配置提供器
        /// </summary>
        private readonly IEmailConfigProvider _emailConfigProvider;
        /// <summary>
        /// 消息
        /// </summary>
        private MimeMessage _mimeMessage { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        private Account _account { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        private Ip _ip { get; set; }

        /// <summary>
        /// 初始化Email服务
        /// </summary>
        /// <param name="emailConfigProvider">邮箱配置提供器</param>
        public EmailService(IEmailConfigProvider emailConfigProvider)
        {
            emailConfigProvider.CheckNull(nameof(emailConfigProvider));
            _emailConfigProvider = emailConfigProvider;
            _mimeMessage = new MimeMessage();
            SetConfig();
            AddFromEmail();
        }

        /// <summary>
        /// 设置配置
        /// </summary>
        private void SetConfig()
        {
            var account = _emailConfigProvider.Account();
            account.Validate();
            _account = account;
            var ip = GetIpConfig();
            ip.Validate();
            _ip = ip;
        }

        /// <summary>
        /// 获取邮箱IP配置
        /// </summary>
        private Ip GetIpConfig()
        {
            var emailConfigFactory = new EmailConfigFactory();
            var emailConfig = emailConfigFactory.GetEmail(_emailConfigProvider.Email());
            return emailConfig.Smtp();
        }

        /// <summary>
        /// 添加发送邮箱
        /// </summary>
        private void AddFromEmail()
        {
            _mimeMessage.From.Add(new MailboxAddress(_account.Name, _account.UserName));
        }

        /// <summary>
        /// 添加接收邮箱
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="name">名称</param>
        public void AddToEmail(string address, string name = "")
        {
            _mimeMessage.To.Add(new MailboxAddress(name, address));
        }

        /// <summary>
        /// 添加接收邮箱集合
        /// </summary>
        /// <param name="toEmails">接收邮箱集合</param>
        public void AddToEmails(List<Parameter.Object.Email> toEmails)
        {
            foreach (var email in toEmails)
            {
                email.Validate();
                AddToEmail(email.Name, email.Address);
            }
        }

        /// <summary>
        /// 添加抄送邮箱
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="name">名称</param>
        public void AddCcEmail(string address, string name = "")
        {
            _mimeMessage.Cc.Add(new MailboxAddress(name, address));
        }

        /// <summary>
        /// 添加抄送邮箱集合
        /// </summary>
        /// <param name="toEmails">抄送邮箱集合</param>
        public void AddCcEmails(List<Parameter.Object.Email> toEmails)
        {
            foreach (var email in toEmails)
            {
                email.Validate();
                AddCcEmail(email.Name, email.Address);
            }
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message">消息</param>
        public void Message(Message message)
        {
            message.Validate();
            _mimeMessage.Subject = message.Title;
            _mimeMessage.Body = new TextPart("plain")
            {
                Text = message.Content
            };
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public Result Send()
        {
            Validate();
            try
            {
                using (var client = new SmtpClient())
                {
                    SetSmtpClient(client);
                    client.Send(_mimeMessage);
                    client.Disconnect(true);
                }
                return new Result();
            }
            catch (System.Exception e)
            {
                return new Result(false, "", e.Message);
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public async Task<Result> SendAsync()
        {
            Validate();
            try
            {
                using (var client = new SmtpClient())
                {
                    SetSmtpClient(client);
                    await client.SendAsync(_mimeMessage);
                    client.Disconnect(true);
                }
                return new Result();
            }
            catch (System.Exception e)
            {
                return new Result(false, "", e.Message);
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        private void Validate()
        {
            if (_mimeMessage.To.IsEmpty())
                throw new Warning("至少存在一个收件邮箱");
            if (_mimeMessage.Subject.IsEmpty() || _mimeMessage.Body.IsNull())
                throw new Warning("缺少邮件内容");
        }

        /// <summary>
        /// 设置SMTP客户端
        /// </summary>
        /// <param name="smtpClient">SMTP客户端</param>
        private void SetSmtpClient(SmtpClient smtpClient)
        {
            smtpClient.Connect(_ip.Address, _ip.Port.SafeValue(), false);
            smtpClient.Authenticate(_account.UserName, _account.Password);
        }
    }
}