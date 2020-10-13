using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// HTTP数据内容类型
    /// </summary>
    public enum HttpDataContentType
    {
        /// <summary>
        /// 表单数据格式
        /// </summary>
        [Description("表单数据格式")]
        FormData = 1,
        /// <summary>
        /// 表单文件数据格式
        /// </summary>
        [Description("表单文件数据格式")]
        FormFile = 2,
        /// <summary>
        /// JSON数据格式
        /// </summary>
        [Description("JSON数据格式")]
        Json = 3,
        /// <summary>
        /// XML数据格式
        /// </summary>
        [Description("XML数据格式")]
        Xml = 4,
    }
}