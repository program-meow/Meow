namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// 连接对象
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        string Server { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        string Database { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        string UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        int? Port { get; set; }
        /// <summary>
        /// 初始化连接对象
        /// </summary>
        /// <param name="server">服务端地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口</param>
        void Init(string server, string database, string userId, string password, int? port = null);
        /// <summary>
        /// 转换为连接字符串
        /// </summary>
        string ToConnectionString();
    };
}
