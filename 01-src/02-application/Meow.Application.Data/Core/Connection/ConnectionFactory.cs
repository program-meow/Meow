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
            switch (databaseType)
            {
                case Parameter.Enum.Database.SqlServer:
                    return new ConnectionSqlServer();
                case Parameter.Enum.Database.MySql:
                    return new ConnectionMySql();
                case Parameter.Enum.Database.PgSql:
                    return new ConnectionPgSql();
                case Parameter.Enum.Database.Oracle:
                    return new ConnectionOracle();
                default:
                    throw new Warning("不支持该数据库类型");
            }
        }
    }
}