using Meow.Addin.Email.Core.Parameter;
using Meow.Dependency;

namespace Meow.Addin.Email
{
    /// <summary>
    /// 邮箱配置提供器
    /// </summary>
    public interface IEmailConfigProvider : IScopeDependency
    {
        /// <summary>
        /// 设置邮箱账户
        /// </summary>
        EmailFromAccount Account();
        /// <summary>
        /// 邮箱类型
        /// </summary>
        Parameter.Enum.Email Email();
    }
}