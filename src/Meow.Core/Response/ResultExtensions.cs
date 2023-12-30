namespace Meow.Response;

/// <summary>
/// 结果扩展
/// </summary>
public static class ResultExtensions {

    /// <summary>
    /// 转换为结果状态码
    /// </summary>
    /// <param name="httpStatusCode">HTTP状态码</param>
    public static ResultStatusEnum ToResultStatusCode( this HttpStatusCode httpStatusCode ) {
        return httpStatusCode switch {
            HttpStatusCode.OK => ResultStatusEnum.Success,
            HttpStatusCode.InternalServerError => ResultStatusEnum.Error,
            HttpStatusCode.Unauthorized => ResultStatusEnum.Unauthorized,
            _ => ResultStatusEnum.Error
        };
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    /// <param name="resultStatusCode">结果状态码</param>
    public static bool IsSuccess( this ResultStatusEnum resultStatusCode ) {
        return resultStatusCode == ResultStatusEnum.Success;
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