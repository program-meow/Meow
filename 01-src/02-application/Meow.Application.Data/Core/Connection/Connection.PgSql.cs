using Meow.Extension.Helper;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// PgSql连接对象
    /// </summary>
    public class ConnectionPgSql : Connection
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"server={Server};{(Port.IsNull() ? "" : $"port={Port};")}database={Database};User Id={UserId};password={Password};";
        }
    }
}