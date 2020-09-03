using System;
using System.Linq;
using Meow.Exception;
using Meow.Validation;

namespace Meow.Extension.Validation
{
    /// <summary>
    /// 公共验证扩展
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

        /// <summary>
        /// 验证
        /// </summary>
        /// <typeparam name="T">验证对象类型</typeparam>
        /// <param name="value">验证值</param>
        public static ValidationResultCollection Validate<T>(this T value) where T : IValidation
        {
            var result = DataAnnotationValidation.Validate(value);
            if (result.IsValid)
                return ValidationResultCollection.Success;
            throw new Warning(result.First().ErrorMessage);
        }
    }
}