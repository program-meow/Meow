using System;
using Meow.Extension.Helper;

namespace Meow.Helper
{
    /// <summary>
    /// 128位浮点型操作
    /// </summary>
    public static class Decimal
    {
        /// <summary>
        /// 转换为128位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(object value, int? digits = null)
        {
            return ToDecimalOrNull(value, digits) ?? 0;
        }

        /// <summary>
        /// 转换为128位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(object value, int? digits = null)
        {
            var success = decimal.TryParse(value.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }
    }
}