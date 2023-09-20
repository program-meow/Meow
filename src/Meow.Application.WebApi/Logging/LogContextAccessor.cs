namespace Meow.Application.Logging;

/// <summary>
/// 日志上下文访问器
/// </summary>
public class LogContextAccessor : ILogContextAccessor {
    /// <summary>
    /// 日志上下文键名
    /// </summary>
    public const string LogContextKey = "Meow.Logging.LogContext";

    /// <summary>
    /// 日志上下文
    /// </summary>
    public LogContext Context {
        get => Meow.Helper.Convert.To<LogContext>( Web.HttpContext.Items[ LogContextKey ] );
        set => Web.HttpContext.Items[ LogContextKey ] = value;
    }
}