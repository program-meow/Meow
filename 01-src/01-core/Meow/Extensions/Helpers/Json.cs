namespace Meow.Extensions.Helpers
{
    /// <summary>
    /// JSON扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static T ToObject<T>(this string json)
        {
            var result = Meow.Helpers.Json.ToObject<T>(json);
            return result;
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="quotes">引号，默认不填为双引号，范例：单引号 "'"</param>
        public static string ToJson(this object target, string quotes = "")
        {
            var result = Meow.Helpers.Json.ToJson(target, quotes);
            return result;
        }
    }
}
