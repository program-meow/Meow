namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// PgSql连接对象
    /// </summary>
    public class ConnectionPgSql : Connection
    {
        /// <summary>
        /// 初始化连接对象
        /// </summary>
        public ConnectionPgSql() : base(5432) { }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"server={Server};port={Port};database={Database};User Id={UserId};password={Password};";
        }
    }
}