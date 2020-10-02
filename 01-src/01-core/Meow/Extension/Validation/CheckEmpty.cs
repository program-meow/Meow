using System;
using Meow.Exception;
using System.Collections.Generic;

namespace Meow.Extension.Validation
{
    /// <summary>
    /// 检测为空扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty(this string value, string parameterName)
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }

        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty(this Guid value, string parameterName)
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }

        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty(this Guid? value, string parameterName)
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }

        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty<T>(this IEnumerable<T> value, string parameterName)
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }
    }
}