using Meow.Extension.Helper;

namespace Meow.Helper
{
    /// <summary>
    /// 时间型操作
    /// </summary>
    public static class DateTime
    {
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="value">值</param>
        public static System.DateTime ToDate(object value)
        {
            return ToDateOrNull(value) ?? System.DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="value">值</param>
        public static System.DateTime? ToDateOrNull(object value)
        {
            return System.DateTime.TryParse(value.SafeString(), out var result) ? (System.DateTime?)result : null;
        }
    }
}