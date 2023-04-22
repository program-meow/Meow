using Meow.Consts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Meow.Helpers
{
    /// <summary>
    /// 验证操作
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// 是否为null
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNull(object value)
        {
            return value == null;
        }

        /// <summary>
        /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(object obj, string parameterName)
        {
            if (IsNull(obj))
                throw new ArgumentNullException(parameterName);
        }

        #region IsEmpty  [是否为空]

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(Guid value)
        {
            return value == Guid.Empty;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(Guid? value)
        {
            if (value == null)
                return true;
            return value == Guid.Empty;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(DateTime value)
        {
            return value == DateTime.MinValue|| value == DateTime.MaxValue;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(DateTime? value)
        {
            if (value == null)
                return true;
            return value == DateTime.MinValue|| value == DateTime.MaxValue;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty<T>(IEnumerable<T> value)
        {
            if (value == null)
                return true;
            return !value.Any();
        }

        #endregion


    }
}
