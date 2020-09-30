using Meow.Helper;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// JSON扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="value">值</param>
        public static T ToObject<T>(this string value)
        {
            return Json.ToObject<T>(value);
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="quotes">引号，默认不填为双引号，范例：单引号 "'"</param>
        public static string ToJson(this object value, string quotes = "")
        {
            return Json.ToJson(value, quotes);
        }
    }
}