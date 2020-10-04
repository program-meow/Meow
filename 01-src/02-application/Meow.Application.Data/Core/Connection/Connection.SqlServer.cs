using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// SqlServer连接对象
    /// </summary>
    public class ConnectionSqlServer : Connection
    {
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        [Required(ErrorMessage = "端口不能为空")]
        public override int? Port { get; set; } = 1433;

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            return $"Server={Server}{(Port == 1433 ? "" : $",{Port}")};Database={Database};uid={UserId};pwd={Password};MultipleActiveResultSets=true";
        }
    }
}