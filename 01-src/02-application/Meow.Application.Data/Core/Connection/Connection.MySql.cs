namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// MySql连接对象
    /// </summary>
    public class ConnectionMySql : Connection
    {
        /// <summary>
        /// 初始化连接对象
        /// </summary>
        public ConnectionMySql() : base(3306) { }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"server={Server};port={Port};database={Database};user id={UserId};password={Password};CharSet=utf8;";
        }
    }
}