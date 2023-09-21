using Meow.Aop;

namespace Meow.Application.Extension;

/// <summary>
/// 异常扩展
/// </summary>
public static class ExceptionExtensions {
    /// <summary>
    /// 获取异常提示
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="isProduction">是否生产环境</param>
    public static string GetPrompt( this SystemException exception , bool isProduction = false ) {
        if( exception == null )
            return null;
        exception = exception.GetRawException();
        if( exception == null )
            return null;
        if( exception is Warning warning )
            return warning.GetMessage( isProduction );
        return isProduction ? "系统忙，请稍后再试" : exception.Message;
    }

    /// <summary>
    /// 获取Http状态码
    /// </summary>
    /// <param name="exception">异常</param>
    public static int? GetHttpStatusCode( this SystemException exception ) {
        if( exception == null )
            return null;
        exception = exception.GetRawException();
        if( exception == null )
            return null;
        if( exception is Warning warning )
            return warning.HttpStatusCode;
        return null;
    }

    /// <summary>
    /// 获取错误码
    /// </summary>
    /// <param name="exception">异常</param>
    public static string GetErrorCode( this SystemException exception ) {
        if( exception == null )
            return null;
        exception = exception.GetRawException();
        if( exception == null )
            return null;
        if( exception is Warning warning )
            return warning.Code;
        return null;
    }
}