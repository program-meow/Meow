using System;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// DateTime扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this DateTime value)
        {
            return value == DateTime.MinValue || value == DateTime.MaxValue;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this DateTime? value)
        {
            if (value.IsNull())
                return true;
            return value.SafeValue().IsEmpty();
        }

        /// <summary>
        /// 如果为空则返回覆盖值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="coverValue">覆盖值</param>
        public static DateTime EmptyCover(this DateTime value, DateTime coverValue)
        {
            return value.IsEmpty() ? coverValue : value;
        }

        /// <summary>
        /// 如果为空则返回覆盖值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="coverValue">覆盖值</param>
        public static DateTime EmptyCover(this DateTime? value, DateTime coverValue)
        {
            return value.IsEmpty() ? coverValue : value.SafeValue();
        }
    }
}