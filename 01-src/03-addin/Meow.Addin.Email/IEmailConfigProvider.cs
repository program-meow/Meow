using Meow.Dependency;
using Meow.Parameter.Object;

namespace Meow.Addin.Email
{
    /// <summary>
    /// 邮箱配置提供器
    /// </summary>
    public interface IEmailConfigProvider : IScopeDependency
    {
        /// <summary>
        /// 获取邮箱账户
        /// </summary>
        Account Account();
        /// <summary>
        /// 邮箱类型
        /// </summary>
        Parameter.Enum.Email Email();
    }
}