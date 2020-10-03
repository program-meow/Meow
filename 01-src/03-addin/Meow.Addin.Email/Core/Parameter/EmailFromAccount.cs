using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Addin.Email.Core.Parameter
{
    /// <summary>
    /// 邮箱发送方账户
    /// </summary>
    public class EmailFromAccount
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        [EmailAddress(ErrorMessage = "邮箱地址不正确")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}