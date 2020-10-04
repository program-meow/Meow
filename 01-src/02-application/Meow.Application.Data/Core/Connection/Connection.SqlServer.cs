namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// SqlServer连接对象
    /// </summary>
    public class ConnectionSqlServer : Connection
    {
        /// <summary>
        /// 默认端口号
        /// </summary>
        protected override int DefaultPort()
        {
            return 1433;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"Server={Server}{(Port == 1433 ? "" : $",{Port}")};Database={Database};uid={UserId};pwd={Password};MultipleActiveResultSets=true";
        }
    }
}