using Meow.Extension.Helper;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// MySql连接对象
    /// </summary>
    public class ConnectionMySql : Connection
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"server={Server};{(Port.IsNull() ? "" : $"port={Port};")}database={Database};user id={UserId};password={Password};CharSet=utf8;";
        }
    }
}