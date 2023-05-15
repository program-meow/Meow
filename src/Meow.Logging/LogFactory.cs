using System;
using Microsoft.Extensions.Logging;
using SystemType = System.Type;

namespace Meow.Logging;

/// <summary>
/// 日志操作工厂
/// </summary>
public class LogFactory : ILogFactory
{
    /// <summary>
    /// 日志记录器工厂
    /// </summary>
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// 初始化日志操作工厂
    /// </summary>
    /// <param name="factory">日志记录器工厂</param>
    /// <exception cref="ArgumentNullException"></exception>
    public LogFactory(ILoggerFactory factory)
    {
        _loggerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <summary>
    /// 创建日志操作
    /// </summary>
    /// <param name="categoryName">日志类别</param>
    public ILog CreateLog(string categoryName)
    {
        ILogger logger = _loggerFactory.CreateLogger(categoryName);
        LoggerWrapper wrapper = new LoggerWrapper(logger);
        return new Log(wrapper);
    }

    /// <summary>
    /// 创建日志操作
    /// </summary>
    /// <param name="type">日志类别类型</param>
    public ILog CreateLog(SystemType type)
    {
        ILogger logger = _loggerFactory.CreateLogger(type);
        LoggerWrapper wrapper = new LoggerWrapper(logger);
        return new Log(wrapper);
    }
}