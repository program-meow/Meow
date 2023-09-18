namespace Meow.Validation;

/// <summary>
/// 数据注解验证操作
/// </summary>
public static class DataAnnotationValidation {
    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="target">验证目标</param>
    public static ValidationResultCollection Validate( object target ) {
        if( target == null )
            throw new ArgumentNullException( nameof( target ) );
        ValidationResultCollection result = new ValidationResultCollection();
        List<ValidationResult> validationResults = new List<ValidationResult>();
        ValidationContext context = new ValidationContext( target , null , null );
        bool isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject( target , context , validationResults , true );
        if( !isValid )
            result.AddList( validationResults );
        return result;
    }
}