namespace Meow.Extension;

/// <summary>
/// 验证扩展
/// </summary>
public static class ValidationExtensions {
    /// <summary>
    /// 是否为null
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsNull( this object value ) {
        return value == null;
    }

    /// <summary>
    /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="parameterName">参数名</param>
    public static void CheckNull( this object obj , string parameterName ) {
        if( IsNull( obj ) )
            throw new ArgumentNullException( parameterName );
    }

    #region IsEmpty  [是否为空]

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty( [NotNullWhen( false )] this string value ) {
        return string.IsNullOrWhiteSpace( value );
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty( this Guid value ) {
        return value == Guid.Empty;
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty( [NotNullWhen( false )] this Guid? value ) {
        if( value == null )
            return true;
        return value == Guid.Empty;
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty( this DateTime value ) {
        return value == DateTime.MinValue || value == DateTime.MaxValue;
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty( [NotNullWhen( false )] this DateTime? value ) {
        if( value == null )
            return true;
        return value == DateTime.MinValue || value == DateTime.MaxValue;
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">集合</param>
    public static bool IsEmpty<T>( this IEnumerable<T> value ) {
        if( value == null )
            return true;
        return !value.Any();
    }

    #endregion

    /// <summary>
    /// 是否数字
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsNumber( this string value ) {
        if( IsEmpty( value ) )
            return false;
        return Regex.IsMatch( value , Meow.Const.RegexPattern.Number );
    }

    /// <summary>
    /// 是否手机号
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsPhone( this string value ) {
        if( IsEmpty( value ) )
            return false;
        return Regex.IsMatch( value , Meow.Const.RegexPattern.Phone );
    }

    /// <summary>
    /// 是否座机号码（国内）
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsLandline( this string value ) {
        if( IsEmpty( value ) )
            return false;
        return Regex.IsMatch( value , Meow.Const.RegexPattern.Landline );
    }

    /// <summary>
    /// 是否邮箱
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsEmail( this string value ) {
        if( IsEmpty( value ) )
            return false;
        return Regex.IsMatch( value , Meow.Const.RegexPattern.Email );
    }

    /// <summary>
    /// 是否包含数字
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsContainsNumber( this string value ) {
        if( IsEmpty( value ) )
            return false;
        return Regex.IsMatch( value , Meow.Const.RegexPattern.ContainsNumber );
    }

    /// <summary>
    /// 是否包含中文
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsContainsCn( this string value ) {
        if( IsEmpty( value ) )
            return false;
        return Regex.IsMatch( value , Meow.Const.RegexPattern.ContainsCn );
    }

    /// <summary>
    /// 是否身份证号
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsIdCard( this string value ) {
        if( IsEmpty( value ) )
            return false;
        return Regex.IsMatch( value , Meow.Const.RegexPattern.IdCard );
    }
}