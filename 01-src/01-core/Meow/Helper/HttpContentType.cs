using HttpContentTypeEnum = Meow.Parameter.Enum.HttpContentType;

namespace Meow.Helper
{
    /// <summary>
    /// HTTP内容类型操作
    /// </summary>
    public static class HttpContentType
    {
        /// <summary>
        /// 转换为HTTP数据内容类型枚举
        /// </summary>
        /// <param name="value">值</param>
        public static HttpContentTypeEnum? ToEnum(string value)
        {
            return value switch
            {
                "application/x-www-form-urlencoded" => HttpContentTypeEnum.FormData,
                "multipart/form-data" => HttpContentTypeEnum.FormFile,
                "application/json" => HttpContentTypeEnum.Json,
                "application/xml" => HttpContentTypeEnum.Xml,
                _ => (HttpContentTypeEnum?)null
            };
        }
    }
}