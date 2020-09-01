﻿using System;

namespace Meow.Extension.Validate
{
    /// <summary>
    /// 验证扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object value, string parameterName)
        {
            if (value.IsNull())
                throw new ArgumentNullException(parameterName);
        }
    }
}