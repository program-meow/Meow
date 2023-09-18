namespace Meow.Http;

/// <summary>
/// 内容类型
/// </summary>
public enum HttpContentTypeEnum {
    /// <summary>
    /// /<form encType=""/>中默认的encType，form表单数据被编码为key/value格式发送到服务器（表单默认的提交数据的格式）；
    /// application/x-www-form-urlencoded
    /// </summary>
    [Description( "application/x-www-form-urlencoded" )]
    FormData = 1,
    /// <summary>
    /// JSON数据格式；
    /// application/json
    /// </summary>
    [Description( "application/json" )]
    Json = 2,
    /// <summary>
    /// XML数据格式；
    /// application/xml
    /// </summary>
    [Description( "application/xml" )]
    Xml = 3,
    /// <summary>
    /// File数据格式；
    /// multipart/form-data
    /// </summary>
    [Description( "multipart/form-data" )]
    FormFile = 4,
}