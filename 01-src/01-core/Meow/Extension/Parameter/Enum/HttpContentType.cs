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
        /// <param name="value">HTTP内容类型</param>
        public static string Label(this HttpContentType value)
        {
            return "";
        }

        /// <summary>
        /// HTTP内容类型标签
        /// </summary>
        /// <param name="value">HTTP内容类型</param>
        public static string Label(this HttpContentType? value)
        {
            if (value.IsNull())
                return string.Empty;
            return value.SafeValue().Label();
        }

        /// <summary>
        /// 转换为HTTP内容类型枚举
        /// </summary>
        /// <param name="value">HTTP内容类型</param>
        /// <param name="label">标签</param>
        public static HttpContentType ToEnum(this HttpContentType value, string label)
        {
            if (label.IsEmpty())
                return HttpContentType.Null;
            return HttpContentType.Null;
        }
    }
}