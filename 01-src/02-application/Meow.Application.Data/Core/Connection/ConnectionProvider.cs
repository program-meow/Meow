using System;
using Meow.Parameter.Enum;
using Meow.Extension.Helper;
using Microsoft.Extensions.Configuration;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// 连接提供器
    /// </summary>
    public class ConnectionProvider : IConnectionProvider
    {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 初始化连接提供器
        /// </summary>
        /// <param name="configuration">配置</param>
        public ConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="key">标识</param>
        public IConnection GetConnection(string key)
        {
            var connection = ConnectionFactory.Create(_configuration[$"{key}:Type"].ToEnum<Database>());
            connection.Init(_configuration[$"{key}:Server"]
                            , _configuration[$"{key}:Database"]
                            , _configuration[$"{key}:UserId"]
                            , _configuration[$"{key}:Password"]
                            , _configuration[$"{key}:Port"].ToIntOrNull());
            return connection;
        }
    }
}