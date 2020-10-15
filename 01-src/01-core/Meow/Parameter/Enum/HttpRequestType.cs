using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// Http请求类型
    /// </summary>
    public enum HttpRequestType
    {
        /// <summary>
        /// Get请求
        /// </summary>
        [Description("Get请求")]
        Get = 1,
        /// <summary>
        /// Post请求
        /// </summary>
        [Description("Post请求")]
        Post = 2,
        /// <summary>
        /// Put请求
        /// </summary>
        [Description("Put请求")]
        Put = 3,
        /// <summary>
        /// Delete请求
        /// </summary>
        [Description("Delete请求")]
        Delete = 4,
    }
}