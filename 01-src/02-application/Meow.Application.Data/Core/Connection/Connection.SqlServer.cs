namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// SqlServer连接对象
    /// </summary>
    public class ConnectionSqlServer : Connection
    {
        /// <summary>
        /// 是否验证端口
        /// </summary>
        protected override bool IsValidatePort()
        {
            return false;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"Server={Server};Database={Database};uid={UserId};pwd={Password};MultipleActiveResultSets=true";
        }
    }
}