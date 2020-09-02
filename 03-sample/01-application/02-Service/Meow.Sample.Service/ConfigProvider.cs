using System;
using Microsoft.Extensions.Configuration;

namespace Meow.Sample.Service
{
    /// <summary>
    /// 搜索配置提供器
    /// </summary>
    public class ConfigProvider : IConfigProvider
    {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 初始化搜索配置提供器
        /// </summary>
        /// <param name="configuration">配置</param>
        public ConfigProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// 获取搜索配置
        /// </summary>
        public void GetConfig()
        {
            var aa = _configuration["Connection:SqlServer"];
        }
    }
}
