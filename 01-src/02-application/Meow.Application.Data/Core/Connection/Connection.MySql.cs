using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// MySql连接对象
    /// </summary>
    public class ConnectionMySql : Connection
    {
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        [Required(ErrorMessage = "端口不能为空")]
        public new int? Port { get; set; } = 3306;

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"server={Server};port={Port};database={Database};user id={UserId};password={Password};CharSet=utf8;";
        }
    }
}