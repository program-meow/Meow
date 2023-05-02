using System;
using Microsoft.Extensions.Logging;

namespace Meow.Logging {
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

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        public virtual bool IsEnabled( LogLevel logLevel ) {
            return Logger.IsEnabled( logLevel );
        }

        /// <summary>写日志</summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="state">日志状态对象</param>
        /// <param name="exception">异常</param>
        /// <param name="formatter">消息格式化操作</param>
        /// <typeparam name="TState">日志状态类型</typeparam>
        public virtual void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter ) {
            Logger.Log( logLevel, eventId, state, exception, formatter );
        }

        /// <summary>
        /// 创建日志范围
        /// </summary>
        /// <param name="state">日志状态</param>
        /// <typeparam name="TState">日志状态类型</typeparam>
        public virtual IDisposable BeginScope<TState>( TState state ) {
            return Logger.BeginScope( state );
        }

        /// <summary>
        /// 写跟踪日志
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public void LogTrace( EventId eventId, Exception exception, string message, params object[] args ) {
            Logger.LogTrace( eventId,exception,message,args );
        }

        /// <summary>
        /// 写调试日志
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public void LogDebug( EventId eventId, Exception exception, string message, params object[] args ) {
            Logger.LogDebug( eventId, exception, message, args );
        }

        /// <summary>
        /// 写信息日志
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public void LogInformation( EventId eventId, Exception exception, string message, params object[] args ) {
            Logger.LogInformation( eventId, exception, message, args );
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public void LogWarning( EventId eventId, Exception exception, string message, params object[] args ) {
            Logger.LogWarning( eventId, exception, message, args );
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public void LogError( EventId eventId, Exception exception, string message, params object[] args ) {
            Logger.LogError( eventId, exception, message, args );
        }

        /// <summary>
        /// 写致命日志
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public void LogCritical( EventId eventId, Exception exception, string message, params object[] args ) {
            Logger.LogCritical( eventId, exception, message, args );
        }
    }
}
