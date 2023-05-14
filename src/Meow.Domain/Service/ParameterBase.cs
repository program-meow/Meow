using System.Linq;
using Meow.Exception;
using Meow.Validation;

namespace Meow.Domain.Service;

/// <summary>
/// 参数
/// </summary>
public abstract class ParameterBase : IValidation
{
    /// <summary>
    /// 验证
    /// </summary>
    public virtual ValidationResultCollection Validate()
    {
        var result = DataAnnotationValidation.Validate(this);
        if (result.IsValid)
            return ValidationResultCollection.Success;
        throw new Warning(result.First().ErrorMessage);
    }
}