namespace Meow.Application.Dto;

/// <summary>
/// 请求参数
/// </summary>
public abstract class RequestBase : IRequest {
    /// <summary>
    /// 验证
    /// </summary>
    public virtual ValidationResultCollection Validate() {
        IObjectModelValidator validator = Ioc.Create<IObjectModelValidator>();
        if( validator == null ) {
            ValidationResultCollection result = DataAnnotationValidation.Validate( this );
            if( result.IsValid )
                return ValidationResultCollection.Success;
            throw new Warning( result.First().ErrorMessage );
        }
        ActionContext actionContext = new ActionContext();
        validator.Validate( actionContext , null , string.Empty , this );
        if( actionContext.ModelState.IsValid )
            return ValidationResultCollection.Success;
        throw new Warning( actionContext.ModelState.Values.First().Errors.First().ErrorMessage );
    }
}