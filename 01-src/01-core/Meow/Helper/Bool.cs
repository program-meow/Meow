using Meow.Extension.Helper;

namespace Meow.Helper
{
    /// <summary>
    /// 布尔型操作
    /// </summary>
    public static class Bool
    {
        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="value">值</param>
        public static bool ToBool(object value)
        {
            return ToBoolOrNull(value) ?? false;
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="value">值</param>
        public static bool? ToBoolOrNull(object value)
        {
            var @bool = GetBool(value);
            if (@bool != null)
                return @bool.Value;
            return bool.TryParse(value.SafeString(), out var result) ? (bool?)result : null;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(object value)
        {
            switch (value.SafeString().ToLower())
            {
                case "0":
                    return false;
                case "否":
                    return false;
                case "不":
                    return false;
                case "no":
                    return false;
                case "fail":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "ok":
                    return true;
                case "yes":
                    return true;
                default:
                    return null;
            }
        }
    }
}