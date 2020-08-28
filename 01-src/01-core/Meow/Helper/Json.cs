using Meow.Extension.Validate;
using Newtonsoft.Json;

namespace Meow.Helper
{
    /// <summary>
    /// Json操作
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static T ToObject<T>(string json)
        {
            if (json.IsEmpty())
                return default(T);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="quotes">引号，默认不填为双引号，范例：单引号 "'"</param>
        public static string ToJson(object target, string quotes = "")
        {
            if (target.IsNull())
                return string.Empty;
            var result = JsonConvert.SerializeObject(target);
            if (!quotes.IsEmpty())
                result = result.Replace("\"", quotes);
            return result;
        }
    }
}
