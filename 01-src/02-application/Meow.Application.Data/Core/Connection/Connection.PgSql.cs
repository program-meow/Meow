using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// PgSql连接对象
    /// </summary>
    public class ConnectionPgSql : Connection
    {
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        [Required(ErrorMessage = "端口不能为空")]
        public override int? Port { get; set; } = 5432;

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"server={Server};port={Port};database={Database};User Id={UserId};password={Password};";
        }
    }
}