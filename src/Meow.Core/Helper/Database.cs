using System.Text;
using Meow.Database;
using Meow.Extension;

namespace Meow.Helper
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="connection">连接</param>
        public static string ConnectionString(DatabaseEnum dbType, DatabaseConnection connection)
        {
            connection.CheckNull(nameof(connection));
            return dbType switch
            {
                DatabaseEnum.SqlServer => SqlServerConnectionString(connection),
                DatabaseEnum.MySql => MySqlConnectionString(connection),
                DatabaseEnum.PgSql => PgSqlConnectionString(connection),
                DatabaseEnum.Oracle => OracleConnectionString(connection),
                DatabaseEnum.Sqlite => string.Empty,
                _ => string.Empty
            };
        }

        /// <summary>
        /// MySql连接字符串
        /// </summary>
        /// <param name="connection">连接</param>
        private static string MySqlConnectionString(DatabaseConnection connection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"server={connection.Server};");
            stringBuilder.Append($"port={connection.Port};");
            stringBuilder.Append($"database={connection.Database};");
            stringBuilder.Append($"uid={connection.UserName};");
            stringBuilder.Append($"password={connection.Password};");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Oracle连接字符串
        /// </summary>
        /// <param name="connection">连接</param>
        private static string OracleConnectionString(DatabaseConnection connection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={connection.Server})(PORT={connection.Port}))(CONNECT_DATA=(SERVICE_NAME={connection.Database})));");
            stringBuilder.Append($"User ID={connection.UserName};");
            stringBuilder.Append($"Password={connection.Password};");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// PgSql连接字符串
        /// </summary>
        /// <param name="connection">连接</param>
        private static string PgSqlConnectionString(DatabaseConnection connection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Host={connection.Server};");
            stringBuilder.Append($"Port={connection.Port};");
            stringBuilder.Append($"Database={connection.Database};");
            stringBuilder.Append($"Username={connection.UserName};");
            stringBuilder.Append($"Password={connection.Password};");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// SqlServer连接字符串
        /// </summary>
        /// <param name="connection">连接</param>
        private static string SqlServerConnectionString(DatabaseConnection connection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Server={connection.Server}{(connection.Port.IsEmpty() ? "" : $",{connection.Port}")};");
            stringBuilder.Append($"Database={connection.Database};");
            stringBuilder.Append($"uid={connection.UserName};");
            stringBuilder.Append($"pwd={connection.Password};");
            stringBuilder.Append("MultipleActiveResultSets=true;");
            stringBuilder.Append("TrustServerCertificate=True;");
            return stringBuilder.ToString();
        }
    }
}
