﻿namespace Meow.Validation.Validator;

/// <summary>
/// 验证参数不能为空
/// </summary>
public class NotEmptyAttribute : ParameterInterceptorBase {
    /// <summary>
    /// 执行
    /// </summary>
    public override Task Invoke( ParameterAspectContext context , ParameterAspectDelegate next ) {
        if( string.IsNullOrWhiteSpace( context.Parameter.Value.SafeString() ) )
            throw new ArgumentNullException( context.Parameter.Name );
        return next( context );
    }
}