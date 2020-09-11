using Meow.Aspect;
using Meow.Dependency;

namespace Meow.Data.Core.Connection
{
    /// <summary>
    /// 连接提供器
    /// </summary>
    public interface IConnectionProvider : IScopeDependency
    {
        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="key">标识</param>
        Connection GetConnection([NotEmpty] string key);
    }
}