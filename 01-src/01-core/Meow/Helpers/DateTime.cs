using Meow.Extensions.Helpers;

namespace Meow.Helpers
{
    /// <summary>
    /// 时间型操作
    /// </summary>
    public class DateTime
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
