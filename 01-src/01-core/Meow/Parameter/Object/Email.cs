using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// 邮箱
    /// </summary>
    public class Email
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }
    }
}