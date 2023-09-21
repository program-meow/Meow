namespace Meow.Exception;

/// <summary>
/// 应用程序异常
/// </summary>
public class Warning : SystemException {
    /// <summary>
    /// 初始化应用程序异常
    /// </summary>
    /// <param name="exception">异常</param>
    public Warning( SystemException exception )
        : this( null , exception ) {
    }

    /// <summary>
    /// 初始化应用程序异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="code">错误码</param>
    /// <param name="exception">异常</param>
    /// <param name="httpStatusCode">Http状态码</param>
    public Warning( string message , SystemException exception = null , string code = null , int? httpStatusCode = null )
        : base( message ?? "" , exception ) {
        Code = code;
        HttpStatusCode = httpStatusCode;
        IsLocalization = true;
    }

    /// <summary>
    /// 错误码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Http状态码
    /// </summary>
    public int? HttpStatusCode { get; set; }

    /// <summary>
    /// 是否本地化异常消息
    /// </summary>
    public bool IsLocalization { get; set; }

    /// <summary>
    /// 获取错误消息
    /// </summary>
    /// <param name="isProduction">是否生产环境</param>
    public virtual string GetMessage( bool isProduction = true ) {
        return GetMessage( this );
    }

    /// <summary>
    /// 获取错误消息
    /// </summary>
    public static string GetMessage( SystemException ex ) {
        StringBuilder result = new StringBuilder();
        IList<SystemException> list = GetExceptions( ex );
        foreach( SystemException exception in list )
            AppendMessage( result , exception );
        return result.ToString().Trim( Environment.NewLine.ToCharArray() );
    }

    /// <summary>
    /// 添加异常消息
    /// </summary>
    private static void AppendMessage( StringBuilder result , SystemException exception ) {
        if( exception == null )
            return;
        result.AppendLine( exception.Message );
    }

    /// <summary>
    /// 获取异常列表
    /// </summary>
    public IList<SystemException> GetExceptions() {
        return GetExceptions( this );
    }

    /// <summary>
    /// 获取异常列表
    /// </summary>
    /// <param name="ex">异常</param>
    public static IList<SystemException> GetExceptions( SystemException ex ) {
        List<SystemException> result = new List<SystemException>();
        AddException( result , ex );
        return result;
    }

    /// <summary>
    /// 添加内部异常
    /// </summary>
    private static void AddException( List<SystemException> result , SystemException exception ) {
        if( exception == null )
            return;
        result.Add( exception );
        AddException( result , exception.InnerException );
    }
}