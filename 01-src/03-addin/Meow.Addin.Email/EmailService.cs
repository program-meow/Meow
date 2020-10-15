using MimeKit;
using Meow.Exception;
using MailKit.Net.Smtp;
using Meow.Parameter.Object;
using Meow.Extension.Helper;
using System.Threading.Tasks;
using Meow.Extension.Validation;
using System.Collections.Generic;
using Meow.Addin.Email.Core.Config;
using Meow.Addin.Email.Core.Parameter;
using Meow.Helper;
using Meow.Parameter.Response;

namespace Meow.Addin.Email
{
    /// <summary>
    /// 邮箱服务
    /// </summary>
    public class EmailService : IEmailService
    {
        #region 基础字段

        /// <summary>
        /// 短信配置提供器
        /// </summary>
        private readonly IEmailConfigProvider _emailConfigProvider;
        /// <summary>
        /// 消息
        /// </summary>
        private MimeMessage MimeMessage { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        private EmailFromAccount Account { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        private Ip Ip { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化Email服务
        /// </summary>
        /// <param name="emailConfigProvider">邮箱配置提供器</param>
        public EmailService(IEmailConfigProvider emailConfigProvider)
        {
            emailConfigProvider.CheckNull(nameof(emailConfigProvider));
            _emailConfigProvider = emailConfigProvider;
            MimeMessage = new MimeMessage();
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
            Account = account;
            var ip = GetIpConfig();
            ip.Validate();
            Ip = ip;
        }

        /// <summary>
        /// 获取邮箱IP配置
        /// </summary>
        private Ip GetIpConfig()
        {
            var emailConfig = EmailConfigFactory.Create(_emailConfigProvider.Email());
            return emailConfig.Smtp();
        }

        /// <summary>
        /// 添加发送邮箱
        /// </summary>
        private void AddFromEmail()
        {
            MimeMessage.From.Add(new MailboxAddress(Account.Name, Account.UserName));
        }

        #endregion

        #region 发送/抄送方配置

        /// <summary>
        /// 添加接收邮箱
        /// </summary>
        /// <param name="toEmailAddress">接收邮箱地址</param>
        /// <param name="name">名称</param>
        public IEmailService AddToEmail(string toEmailAddress, string name = "")
        {
            toEmailAddress.CheckEmail("接收方");
            MimeMessage.To.Add(new MailboxAddress(name.EmptyCover(toEmailAddress.GetEmailName()), toEmailAddress));
            return this;
        }

        /// <summary>
        /// 添加接收邮箱集合
        /// </summary>
        /// <param name="toEmailAddress">接收邮箱地址集合</param>
        public IEmailService AddToEmail(IEnumerable<string> toEmailAddress)
        {
            foreach (var toEmail in toEmailAddress)
            {
                toEmail.CheckEmpty(nameof(toEmail));
                AddToEmail(toEmail);
            }
            return this;
        }

        /// <summary>
        /// 添加接收邮箱集合
        /// </summary>
        /// <param name="toEmails">接收邮箱集合</param>
        public IEmailService AddToEmail(IEnumerable<EmailToAccount> toEmails)
        {
            foreach (var toEmail in toEmails)
            {
                toEmail.Validate();
                AddToEmail(toEmail.Address, toEmail.Name);
            }
            return this;
        }

        /// <summary>
        /// 添加抄送邮箱
        /// </summary>
        /// <param name="ccEmailAddress">抄送邮箱地址</param>
        /// <param name="name">名称</param>
        public IEmailService AddCcEmail(string ccEmailAddress, string name = "")
        {
            ccEmailAddress.CheckEmpty("抄送方");
            MimeMessage.Cc.Add(new MailboxAddress(name.EmptyCover(ccEmailAddress.GetEmailName()), ccEmailAddress));
            return this;
        }

        /// <summary>
        /// 添加抄送邮箱集合
        /// </summary>
        /// <param name="ccEmailAddress">抄送邮箱地址集合</param>
        public IEmailService AddCcEmail(IEnumerable<string> ccEmailAddress)
        {
            foreach (var ccEmail in ccEmailAddress)
            {
                ccEmail.CheckEmpty(nameof(ccEmail));
                AddCcEmail(ccEmail);
            }
            return this;
        }

        /// <summary>
        /// 添加抄送邮箱集合
        /// </summary>
        /// <param name="ccEmails">抄送邮箱集合</param>
        public IEmailService AddCcEmail(IEnumerable<EmailToAccount> ccEmails)
        {
            foreach (var ccEmail in ccEmails)
            {
                ccEmail.Validate();
                AddCcEmail(ccEmail.Address, ccEmail.Name);
            }
            return this;
        }

        #endregion

        #region 内容配置

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message">消息</param>
        public IEmailService Message(Message message)
        {
            message.Validate();
            MimeMessage.Subject = message.Title;
            MimeMessage.Body = new TextPart("plain")
            {
                Text = message.Content
            };
            return this;
        }

        #endregion

        #region 发送

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public ResultResponse Send()
        {
            return Async.RunSync(SendAsync);
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public async Task<ResultResponse> SendAsync()
        {
            Validate();
            try
            {
                using (var client = new SmtpClient())
                {
                    SetSmtpClient(client);
                    await client.SendAsync(MimeMessage);
                    client.Disconnect(true);
                }
                return new ResultResponse();
            }
            catch (System.Exception e)
            {
                return new ResultResponse(false, "", e.Message);
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        private void Validate()
        {
            if (MimeMessage.To.IsEmpty())
                throw new Warning("至少存在一个收件邮箱");
            if (MimeMessage.Subject.IsEmpty() || MimeMessage.Body.IsNull())
                throw new Warning("缺少邮件内容");
        }

        /// <summary>
        /// 设置SMTP客户端
        /// </summary>
        /// <param name="smtpClient">SMTP客户端</param>
        private void SetSmtpClient(SmtpClient smtpClient)
        {
            smtpClient.Connect(Ip.Address, Ip.Port.SafeValue(), false);
            smtpClient.Authenticate(Account.UserName, Account.Password);
        }

        #endregion
    }
}