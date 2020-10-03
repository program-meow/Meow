using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Addin.Email.Core.Parameter
{
    /// <summary>
    /// 邮箱接收方账户
    /// </summary>
    public class EmailToAccount
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("邮箱地址")]
        [Required(ErrorMessage = "邮箱地址不能为空")]
        [EmailAddress(ErrorMessage = "邮箱地址不正确")]
        public string Address { get; set; }
    }
}