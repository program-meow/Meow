using Meow.Extension;
using Meow.Logging.Serilog.Enricher;
using Serilog;
using Serilog.Configuration;

namespace Meow.Logging.Serilog.Extension;

/// <summary>
/// Serilog扩展属性配置扩展
/// </summary>
public static class LoggerEnrichmentConfigurationExtensions
{
    /// <summary>
    /// 添加日志上下文扩展属性
    /// </summary>
    /// <param name="source">日志扩展配置</param>
    public static LoggerConfiguration WithLogContext(this LoggerEnrichmentConfiguration source)
    {
        source.CheckNull(nameof(source));
        return source.With<LogContextEnricher>();
    }

    /// <summary>
    /// 添加日志级别扩展属性
    /// </summary>
    /// <param name="source">日志扩展配置</param>
    public static LoggerConfiguration WithLogLevel(this LoggerEnrichmentConfiguration source)
    {
        source.CheckNull(nameof(source));
        return source.With<LogLevelEnricher>();
    }
}