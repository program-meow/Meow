using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// HTTP内容类型
    /// </summary>
    public enum HttpContent
    {
        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        [Description("application/x-www-form-urlencoded")]
        FormUrlEncoded = 1,
        /// <summary>
        /// application/json
        /// </summary>
        [Description("application/json")]
        Json = 2,
        /// <summary>
        /// text/xml
        /// </summary>
        [Description("text/xml")]
        Xml = 3,
    }
}