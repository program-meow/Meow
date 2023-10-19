namespace Meow.Response;

/// <summary>
/// 结果扩展
/// </summary>
public static class ResultExtensions {

    /// <summary>
    /// 转换为结果状态码
    /// </summary>
    /// <param name="httpStatusCode">HTTP状态码</param>
    public static ResultStatusCodeEnum ToResultStatusCode( this HttpStatusCode httpStatusCode ) {
        return httpStatusCode switch {
            HttpStatusCode.OK => ResultStatusCodeEnum.Success,
            HttpStatusCode.InternalServerError => ResultStatusCodeEnum.Error,
            HttpStatusCode.Unauthorized => ResultStatusCodeEnum.Unauthorized,
            _ => ResultStatusCodeEnum.Error
        };
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    /// <param name="resultStatusCode">结果状态码</param>
    public static bool IsSuccess( this ResultStatusCodeEnum resultStatusCode ) {
        return resultStatusCode == ResultStatusCodeEnum.Success;
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    /// <param name="result">结果</param>
    public static bool IsSuccess<T>( this IResult<T> result ) {
        if( result == null )
            return false;
        return result.Code.IsSuccess();
    }
}