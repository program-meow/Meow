using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;

namespace Meow.Extension.Parameter.Enum
{
    /// <summary>
    /// HTTP数据内容类型枚举扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// HTTP内容类型标签
        /// </summary>
        /// <param name="value">HTTP数据内容类型</param>
        public static string Label(this HttpDataContentType value)
        {
            return value switch
            {
                HttpDataContentType.FormData => "application/x-www-form-urlencoded",
                HttpDataContentType.FormFile => "multipart/form-data",
                HttpDataContentType.Json => "application/json",
                HttpDataContentType.Xml => "application/xml",
                _ => throw new Warning("暂不支持的HTTP请求数据内容类型")
            };
        }

        /// <summary>
        /// HTTP内容类型标签
        /// </summary>
        /// <param name="value">HTTP数据内容类型</param>
        public static string Label(this HttpDataContentType? value)
        {
            if (value.IsNull())
                return string.Empty;
            return value.SafeValue().Label();
        }
    }
}