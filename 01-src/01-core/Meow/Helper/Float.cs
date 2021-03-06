﻿using System;
using Meow.Extension.Helper;

namespace Meow.Helper
{
    /// <summary>
    /// 32位浮点型操作
    /// </summary>
    public static class Float
    {
        /// <summary>
        /// 转换为32位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static float ToFloat(object value, int? digits = null)
        {
            return ToFloatOrNull(value, digits) ?? 0;
        }

        /// <summary>
        /// 转换为32位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static float? ToFloatOrNull(object value, int? digits = null)
        {
            var success = float.TryParse(value.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return (float)Math.Round(result, digits.Value);
        }
    }
}