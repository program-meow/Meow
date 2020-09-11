using System.Data;

namespace Meow.Data.Sql
{
    /// <summary>
    /// 数据库
    /// </summary>
    [Meow.Aspect.Ignore]
    public interface IDatabase
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        IDbConnection GetConnection();
    }
}