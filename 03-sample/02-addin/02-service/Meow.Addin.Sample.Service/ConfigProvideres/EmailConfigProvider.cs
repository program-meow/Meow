using Meow.Addin.Email;
using Meow.Addin.Email.Core.Parameter;

namespace Meow.Addin.Sample.Service.ConfigProvideres
{
    /// <summary>
    /// 邮箱配置提供器
    /// </summary>
    public class EmailConfigProvider : IEmailConfigProvider
    {
        /// <summary>
        /// 设置邮箱账户
        /// </summary>
        public EmailFromAccount Account()
        {
            return new EmailFromAccount{
                Name = "测试发送方",
                UserName = "program_meow_test@163.com",
                Password = "Password"
            };
        }

        /// <summary>
        /// 邮箱类型
        /// </summary>
        public Parameter.Enum.Email Email()
        {
            return Parameter.Enum.Email.NetEase163;
        }
    }
}
