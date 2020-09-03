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
        /// <param name="name">名称</param>
        /// <param name="root">根名称</param>
        Connection GetConnection([NotEmpty] string name, [NotEmpty] string root = "Connection");
    }
}