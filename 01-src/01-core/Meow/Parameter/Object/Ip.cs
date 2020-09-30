using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// IP
    /// </summary>
    public class Ip
    {
        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        [Required(ErrorMessage = "IP地址能为空")]
        public string Address { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        [DisplayName("端口号")]
        [Required(ErrorMessage = "端口号不能为空")]
        public int? Port { get; set; }
    }
}