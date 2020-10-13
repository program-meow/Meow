using Meow.Exception;
using Meow.Parameter.Enum;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// 连接对象工厂
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// 创建链接对象
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        public static IConnection Create(Database databaseType)
        {
            return databaseType switch
            {
                Parameter.Enum.Database.SqlServer => (IConnection)new ConnectionSqlServer(),
                Parameter.Enum.Database.MySql => new ConnectionMySql(),
                Parameter.Enum.Database.PgSql => new ConnectionPgSql(),
                Parameter.Enum.Database.Oracle => new ConnectionOracle(),
                _ => throw new Warning("不支持该数据库类型")
            };
        }
    }
}