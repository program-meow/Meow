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
        /// <param name="input">输入值</param>
        public static System.DateTime ToDate(object input)
        {
            return ToDateOrNull(input) ?? System.DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static System.DateTime? ToDateOrNull(object input)
        {
            return System.DateTime.TryParse(input.SafeString(), out var result) ? (System.DateTime?)result : null;
        }
    }
}
