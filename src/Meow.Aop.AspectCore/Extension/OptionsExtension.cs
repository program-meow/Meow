using System;
using AspectCore.Configuration;
using MeowOption = Meow.Config.Option;

namespace Meow.Aop.Extension;

/// <summary>
/// Aop配置扩展
/// </summary>
public static class OptionsExtension
{
    /// <summary>
    /// 启用AspectCore拦截器
    /// </summary>
    /// <param name="options">配置项</param>
    /// <param name="setupAction">AspectCore拦截器配置操作</param>
    public static MeowOption UseAop(this MeowOption options, Action<IAspectConfiguration> setupAction = null)
    {
        options.AddExtension(new AopOptionsExtension(setupAction));
        return options;
    }
}