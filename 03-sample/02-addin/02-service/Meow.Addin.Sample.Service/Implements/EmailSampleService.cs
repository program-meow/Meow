using System.Collections.Generic;
using System.Threading.Tasks;
using Meow.Addin.Email;
using Meow.Addin.Email.Core.Parameter;
using Meow.Addin.Sample.Service.Abstractions;
using Meow.Parameter.Object;

namespace Meow.Addin.Sample.Service.Implements
{
    /// <summary>
    /// 邮箱演示服务
    /// </summary>
    public class EmailSampleService : IEmailSampleService
    {
        /// <summary>
        /// 邮箱服务
        /// </summary>
        protected IEmailService EmailService { get; set; }

        /// <summary>
        /// 初始化邮箱演示服务
        /// </summary>
        /// <param name="emailService">邮箱服务</param>
        public EmailSampleService(
            IEmailService emailService
             )
        {
            EmailService = emailService;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public Result Send()
        {
            return EmailService
                .AddToEmail(new List<EmailToAccount>
                {
                    new EmailToAccount
                    {
                        Address = "455855199@qq.com",
                        Name = "测试接收方"
                    }
                })
                .AddCcEmail(new List<EmailToAccount>
                {
                    new EmailToAccount
                    {
                        Address = "program_meow_test@163.com",
                        Name = "测试抄送方"
                    }
                })
                .Message(new Message
                {
                    Title = "测试标题",
                    Content = "                  测试内容A        " 
                })
                .Send();
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public async Task<Result> SendAsync()
        {
            return await EmailService
                .AddToEmail(new List<EmailToAccount>
                {
                    new EmailToAccount
                    {
                        Address = "455855199@qq.com",
                        Name = "测试接收方"
                    }
                })
                .AddCcEmail(new List<EmailToAccount>
                {
                    new EmailToAccount
                    {
                        Address = "program_meow_test@163.com",
                        Name = "测试抄送方"
                    }
                })
                .Message(new Message
                {
                    Title = "测试标题",
                    Content = "                  测试内容A        "
                })
                .SendAsync();
        }
    }
}