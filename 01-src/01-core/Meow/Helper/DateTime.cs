using Meow.Extension.Helper;
using MicrosoftSystem = System;

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
        public static MicrosoftSystem.DateTime ToDate(object value)
        {
            return ToDateOrNull(value) ?? MicrosoftSystem.DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="value">值</param>
        public static MicrosoftSystem.DateTime? ToDateOrNull(object value)
        {
            return MicrosoftSystem.DateTime.TryParse(value.SafeString(), out var result) ? (MicrosoftSystem.DateTime?)result : null;
        }
    }
}