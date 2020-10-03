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
        /// 验证
        /// </summary>
        /// <typeparam name="T">验证对象类型</typeparam>
        /// <param name="value">验证值</param>
        public static ValidationResultCollection Validate<T>(this T value) where T : class
        {
            var result = DataAnnotationValidation.Validate(value);
            if (result.IsValid)
                return ValidationResultCollection.Success;
            throw new Warning(result.First().ErrorMessage);
        }
    }
}