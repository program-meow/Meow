using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        [Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; }
    }
}
