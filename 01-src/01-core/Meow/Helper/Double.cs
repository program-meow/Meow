using System;
using Meow.Extension.Helper;

namespace Meow.Helper
{
    /// <summary>
    /// 浮点型操作
    /// </summary>
    public static class Double
    {
        /// <summary>
        /// 转换为64位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(object value, int? digits = null)
        {
            return ToDoubleOrNull(value, digits) ?? 0;
        }

        /// <summary>
        /// 转换为64位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(object value, int? digits = null)
        {
            var success = double.TryParse(value.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }
    }
}