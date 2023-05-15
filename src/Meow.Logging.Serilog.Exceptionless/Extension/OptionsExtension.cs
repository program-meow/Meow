﻿using System;
using Meow.Config;
using Meow.Extension;

namespace Meow.Logging.Serilog.Extension;

/// <summary>
/// Exceptionless日志操作扩展
/// </summary>
public static class OptionsExtension
{
    /// <summary>
    /// 启用Serilog日志操作
    /// </summary>
    /// <param name="options">配置项</param>
    /// <param name="configAction">Exceptionless日志配置操作</param>
    /// <param name="isClearProviders">是否清除默认设置的日志提供程序</param>
    public static Options UseSerilog(this Options options, Action<SerilogExceptionlessConfiguration> configAction, bool isClearProviders = false)
    {
        configAction.CheckNull(nameof(configAction));
        SerilogExceptionlessConfiguration config = new SerilogExceptionlessConfiguration();
        configAction(config);
        options.AddExtension(new ExceptionlessOptionsExtension(config.ConfigAction, isClearProviders));
        return options;
    }
}