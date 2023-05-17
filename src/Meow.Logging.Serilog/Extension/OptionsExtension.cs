﻿using MeowOption = Meow.Config.Option;

namespace Meow.Logging.Serilog.Extension;

/// <summary>
/// Serilog日志操作扩展
/// </summary>
public static class OptionsExtension
{
    /// <summary>
    /// 配置Serilog日志操作
    /// </summary>
    /// <param name="options">配置项</param>
    /// <param name="isClearProviders">是否清除默认设置的日志提供程序</param>
    public static MeowOption UseSerilog(this MeowOption options, bool isClearProviders = false)
    {
        options.AddExtension(new SerilogOptionsExtension(isClearProviders));
        return options;
    }
}