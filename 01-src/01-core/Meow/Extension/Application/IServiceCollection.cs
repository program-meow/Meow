using System;
using System.Text;
using AspectCore.Configuration;
using Meow.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Meow.Extension.Application
{
    /// <summary>
    /// 服务代理扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 注册Meow基础设施服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configs">依赖配置</param>
        public static IServiceProvider AddMeow(this IServiceCollection services, params IConfig[] configs)
        {
            return AddMeow(services, null, configs);
        }

        /// <summary>
        /// 注册Meow基础设施服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="aopConfigAction">Aop配置操作</param>
        /// <param name="configs">依赖配置</param>
        public static IServiceProvider AddMeow(this IServiceCollection services, Action<IAspectConfiguration> aopConfigAction, params IConfig[] configs)
        {
            services.AddHttpContextAccessor();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return Bootstrapper.Run(services, configs, aopConfigAction);
        }
    }
}
