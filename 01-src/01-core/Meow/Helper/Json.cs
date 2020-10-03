using Meow.Extension.Helper;
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
        /// <param name="value">值</param>
        public static T ToObject<T>(string value)
        {
            if (value.IsEmpty())
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="quotes">引号，默认不填为双引号，范例：单引号 "'"</param>
        public static string ToJson(object value, string quotes = "")
        {
            if (value.IsNull())
                return string.Empty;
            var result = JsonConvert.SerializeObject(value);
            if (!quotes.IsEmpty())
                result = result.Replace("\"", quotes);
            return result;
        }
    }
}