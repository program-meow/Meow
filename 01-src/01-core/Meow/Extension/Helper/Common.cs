namespace Meow.Extension.Helper
{
    /// <summary>
    /// 公共扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 是否为Null
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// 是否不为Null
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }

        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="value">值</param>
        public static string SafeString(this object value)
        {
            return value?.ToString().Trim() ?? string.Empty;
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">值</param>
        public static T To<T>(this object value)
        {
            return Meow.Helper.Common.To<T>(value);
        }
    }
}