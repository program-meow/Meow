using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;

namespace Meow.Data
{
    /// <summary>
    /// 连接对象
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// 初始化连接对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="server">服务端地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户</param>
        /// <param name="password">密码</param>
        public Connection(string type, string server, string database, string userId, string password)
        : this(type, server, "", database, userId, password)
        {
        }

        /// <summary>
        /// 初始化连接对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="server">服务端地址</param>
        /// <param name="port">端口</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户</param>
        /// <param name="password">密码</param>
        public Connection(string type, string server, string port, string database, string userId, string password)
        {
            Type = type;
            Server = server;
            Port = port;
            Database = database;
            UserId = userId;
            Password = password;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 服务端地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public Database GetDatabaseType()
        {
            return Type.ToEnum<Database>();
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public string GetConnectionString()
        {
            var type = GetDatabaseType();
            switch (type)
            {
                case Parameter.Enum.Database.SqlServer:
                    return "";
                case Parameter.Enum.Database.MySql:
                    return "";
                case Parameter.Enum.Database.PgSql:
                    return "";
                case Parameter.Enum.Database.Oracle:
                    return "";
                default:
                    throw new Warning("不支持该数据库类型");
            }
        }
    }
}
