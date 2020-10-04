using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Meow.Exception;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// Oracle连接对象
    /// </summary>
    public class ConnectionOracle: Connection
    {
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        [Required(ErrorMessage = "端口不能为空")]
        public override int? Port { get; set; } = 1521;

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        protected override string GetConnectionString()
        {
            throw new Warning("暂不支持该Oracle数据库，后续支持");
        }
    }
}