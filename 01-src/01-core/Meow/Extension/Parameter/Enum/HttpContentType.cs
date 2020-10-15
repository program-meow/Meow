using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;

namespace Meow.Extension.Parameter.Enum
{
    /// <summary>
    /// HTTP内容类型枚举扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// HTTP内容类型标签
        /// </summary>
        /// <param name="value">HTTP数据内容类型</param>
        public static string Label(this HttpContentType value)
        {
            return value switch
            {
                HttpContentType.FormData => "application/x-www-form-urlencoded",
                HttpContentType.FormFile => "multipart/form-data",
                HttpContentType.Json => "application/json",
                HttpContentType.Xml => "application/xml",
                _ => throw new Warning("暂不支持的HTTP请求数据内容类型")
            };
        }

        /// <summary>
        /// HTTP内容类型标签
        /// </summary>
        /// <param name="value">HTTP数据内容类型</param>
        public static string Label(this HttpContentType? value)
        {
            if (value.IsNull())
                return string.Empty;
            return value.SafeValue().Label();
        }
    }
}