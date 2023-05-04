namespace Meow.Http
{
    /// <summary>
    /// Http请求内容类型
    /// </summary>
    public static class HttpContentTypeCode
    {
        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        public const string FormData = "application/x-www-form-urlencoded";
        /// <summary>
        /// application/json
        /// </summary>
        public const string Json = "application/json";
        /// <summary>
        /// application/xml
        /// </summary>
        public const string Xml = "application/xml";
        /// <summary>
        /// multipart/form-data
        /// </summary>
        public const string FormFile = "multipart/form-data";
    }
}
