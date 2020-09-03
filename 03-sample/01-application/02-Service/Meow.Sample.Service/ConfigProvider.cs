using System;
using Meow.Data.Core.Connection;

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
        private readonly IConnectionProvider _connectionProvider;

        /// <summary>
        /// 初始化搜索配置提供器
        /// </summary>
        /// <param name="connectionProvider">配置</param>
        public ConfigProvider(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
        }

        /// <summary>
        /// 获取搜索配置
        /// </summary>
        public void GetConfig()
        {
            var aa = _connectionProvider.GetConnection("SqlServer");
        }
    }
}
