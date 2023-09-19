using SystemException = System.Exception;

namespace Meow.Logging;

/// <summary>
/// 日志记录包装器
/// </summary>
public class LoggerWrapper : ILoggerWrapper {
    /// <summary>
    /// 初始化日志记录包装器
    /// </summary>
    /// <param name="logger">日志记录器</param>
    public LoggerWrapper( ILogger logger ) {
        Logger = logger ?? throw new ArgumentNullException( nameof( logger ) );
    }

    /// <summary>
    /// 日志记录包装器
    /// </summary>
    protected ILogger Logger { get; }

    /// <inheritdoc />
    public virtual bool IsEnabled( LogLevel logLevel ) {
        return Logger.IsEnabled( logLevel );
    }

    /// <inheritdoc />
    public virtual void Log<TState>( LogLevel logLevel , EventId eventId , TState state , SystemException exception , Func<TState , SystemException , string> formatter ) {
        Logger.Log( logLevel , eventId , state , exception , formatter );
    }

    /// <inheritdoc />
    public virtual IDisposable BeginScope<TState>( TState state ) {
        return Logger.BeginScope( state );
    }

    /// <inheritdoc />
    public void LogTrace( EventId eventId , SystemException exception , string message , params object[] args ) {
        Logger.LogTrace( eventId , exception , message , args );
    }

    /// <inheritdoc />
    public void LogDebug( EventId eventId , SystemException exception , string message , params object[] args ) {
        Logger.LogDebug( eventId , exception , message , args );
    }

    /// <inheritdoc />
    public void LogInformation( EventId eventId , SystemException exception , string message , params object[] args ) {
        Logger.LogInformation( eventId , exception , message , args );
    }

    /// <inheritdoc />
    public void LogWarning( EventId eventId , SystemException exception , string message , params object[] args ) {
        Logger.LogWarning( eventId , exception , message , args );
    }

    /// <inheritdoc />
    public void LogError( EventId eventId , SystemException exception , string message , params object[] args ) {
        Logger.LogError( eventId , exception , message , args );
    }

    /// <inheritdoc />
    public void LogCritical( EventId eventId , SystemException exception , string message , params object[] args ) {
        Logger.LogCritical( eventId , exception , message , args );
    }
}