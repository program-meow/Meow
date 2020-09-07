using System;
using Meow.Data.Core.Connection;
using Meow.Sample.Domain.Repository;

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
        /// 应用程序仓储
        /// </summary>
        private readonly IApplicationRepository _applicationRepository;

        /// <summary>
        /// 初始化搜索配置提供器
        /// </summary>
        /// <param name="connectionProvider">配置</param>
        /// <param name="applicationRepository">应用程序仓储</param>
        public ConfigProvider(IConnectionProvider connectionProvider, IApplicationRepository applicationRepository)
        {
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
            _applicationRepository = applicationRepository;
        }

        /// <summary>
        /// 获取搜索配置
        /// </summary>
        public void GetConfig()
        {
            var aa = _applicationRepository.FindAll();
        }
    }
}
