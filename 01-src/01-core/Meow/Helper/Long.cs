using Meow.Extension.Helper;
using MicrosoftSystem = System;

namespace Meow.Helper
{
    /// <summary>
    /// 64位整型操作
    /// </summary>
    public static class Long
    {
        /// <summary>
        /// 转换为64位整型
        /// </summary>
        /// <param name="value">值</param>
        public static long ToLong(object value)
        {
            return ToLongOrNull(value) ?? 0;
        }

        /// <summary>
        /// 转换为64位可空整型
        /// </summary>
        /// <param name="value">值</param>
        public static long? ToLongOrNull(object value)
        {
            var success = long.TryParse(value.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = Decimal.ToDecimalOrNull(value, 0);
                if (temp == null)
                    return null;
                return MicrosoftSystem.Convert.ToInt64(temp);
            }
            catch
            {
                return null;
            }
        }
    }
}