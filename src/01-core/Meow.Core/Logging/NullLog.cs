using System;
using Microsoft.Extensions.Logging;

namespace Meow.Logging {
    /// <summary>
    /// 空日志操作
    /// </summary>
    /// <typeparam name="TCategoryName">日志类别</typeparam>
    public class NullLog<TCategoryName> : ILog<TCategoryName> {
        /// <summary>
        /// 空日志操作实例
        /// </summary>
        public static readonly ILog<TCategoryName> Instance = new NullLog<TCategoryName>();

        /// <summary>
        /// 设置日志事件标识
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        public ILog EventId( EventId eventId ) {
            return this;
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="exception">异常</param>
        public ILog Exception( System.Exception exception ) {
            return this;
        }

        /// <summary>
        /// 设置自定义扩展属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        public ILog Property( string propertyName, string propertyValue ) {
            return this;
        }

        /// <summary>
        /// 设置日志状态对象
        /// </summary>
        /// <param name="state">状态对象</param>
        public ILog State( object state ) {
            return this;
        }

        /// <summary>
        /// 设置日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public ILog Message( string message, params object[] args ) {
            return this;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        public bool IsEnabled( LogLevel logLevel ) {
            return false;
        }

        /// <summary>
        /// 开启日志范围
        /// </summary>
        /// <typeparam name="TState">日志状态类型</typeparam>
        /// <param name="state">日志状态</param>
        public IDisposable BeginScope<TState>( TState state ) {
            return new Meow.Action.DisposeAction( () => { } );
        }

        /// <summary>
        /// 写跟踪日志
        /// </summary>
        public ILog LogTrace() {
            return this;
        }

        /// <summary>
        /// 写调试日志
        /// </summary>
        public ILog LogDebug() {
            return this;
        }

        /// <summary>
        /// 写信息日志
        /// </summary>
        public ILog LogInformation() {
            return this;
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        public ILog LogWarning() {
            return this;
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        public ILog LogError() {
            return this;
        }

        /// <summary>
        /// 写致命日志
        /// </summary>
        public ILog LogCritical() {
            return this;
        }
    }

    /// <summary>
    /// 空日志操作
    /// </summary>
    public class NullLog : ILog {
        /// <summary>
        /// 空日志操作实例
        /// </summary>
        public static readonly  ILog Instance = new NullLog();

        /// <summary>
        /// 设置日志事件标识
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        public ILog EventId( EventId eventId ) {
            return this;
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="exception">异常</param>
        public ILog Exception( System.Exception exception ) {
            return this;
        }

        /// <summary>
        /// 设置自定义扩展属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        public ILog Property( string propertyName, string propertyValue ) {
            return this;
        }

        /// <summary>
        /// 设置日志状态对象
        /// </summary>
        /// <param name="state">状态对象</param>
        public ILog State( object state ) {
            return this;
        }

        /// <summary>
        /// 设置日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">日志消息参数</param>
        public ILog Message( string message, params object[] args ) {
            return this;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        public bool IsEnabled( LogLevel logLevel ) {
            return false;
        }

        /// <summary>
        /// 开启日志范围
        /// </summary>
        /// <typeparam name="TState">日志状态类型</typeparam>
        /// <param name="state">日志状态</param>
        public IDisposable BeginScope<TState>( TState state ) {
            return new Meow.Action.DisposeAction( () => { } );
        }

        /// <summary>
        /// 写跟踪日志
        /// </summary>
        public ILog LogTrace() {
            return this;
        }

        /// <summary>
        /// 写调试日志
        /// </summary>
        public ILog LogDebug() {
            return this;
        }

        /// <summary>
        /// 写信息日志
        /// </summary>
        public ILog LogInformation() {
            return this;
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        public ILog LogWarning() {
            return this;
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        public ILog LogError() {
            return this;
        }

        /// <summary>
        /// 写致命日志
        /// </summary>
        public ILog LogCritical() {
            return this;
        }
    }
}
