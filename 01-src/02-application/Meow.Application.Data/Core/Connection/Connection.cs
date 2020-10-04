using System.ComponentModel;
using Meow.Extension.Validation;
using System.ComponentModel.DataAnnotations;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// 连接对象
    /// </summary>
    public abstract class Connection : IConnection
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        [DisplayName("服务端地址")]
        [Required(ErrorMessage = "服务端地址不能为空")]
        public string Server { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        [DisplayName("数据库名称")]
        [Required(ErrorMessage = "数据库名称不能为空")]
        public string Database { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [DisplayName("用户")]
        [Required(ErrorMessage = "用户不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        [Required(ErrorMessage = "端口不能为空")]
        public abstract int? Port { get; set; }

        /// <summary>
        /// 初始化连接对象
        /// </summary>
        /// <param name="server">服务端地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口</param>
        public void Init(string server, string database, string userId, string password, int? port = null)
        {
            Server = server;
            Database = database;
            UserId = userId;
            Password = password;
            Port = port;
        }

        /// <summary>
        /// 转换为连接字符串
        /// </summary>
        public string ToConnectionString()
        {
            this.Validate();
            return GetConnectionString();
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected abstract string GetConnectionString();
    }
}