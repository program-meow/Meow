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
            return value.SafeString().ToLower() switch
            {
                "0" => false,
                "否" => false,
                "不" => false,
                "no" => false,
                "fail" => false,
                "1" => true,
                "是" => true,
                "ok" => true,
                "yes" => true,
                _ => (bool?)null
            };
        }
    }
}