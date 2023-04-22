using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Meow.Extensions
{
    /// <summary>
    /// 验证扩展
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// 是否为null
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNull(this object value)
        {
            return Meow.Helpers.Validation.IsNull(value);
        }

        /// <summary>
        /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object obj, string parameterName)
        {
            Meow.Helpers.Validation.CheckNull(obj, parameterName);
        }

        #region IsEmpty  [是否为空]

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty([NotNullWhen(false)] this string value)
        {
            return Meow.Helpers.Validation.IsEmpty(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid value)
        {
            return Meow.Helpers.Validation.IsEmpty(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty([NotNullWhen(false)] this Guid? value)
        {
            return Meow.Helpers.Validation.IsEmpty(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this DateTime value)
        {
            return Meow.Helpers.Validation.IsEmpty(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty([NotNullWhen(false)] this DateTime? value)
        {
            return Meow.Helpers.Validation.IsEmpty(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="array">集合</param>
        public static bool IsEmpty<T>(this IEnumerable<T> array)
        {
            return Meow.Helpers.Validation.IsEmpty<T>(array);
        }

        #endregion

    }
}
