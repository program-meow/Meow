using System;
using Meow.Extension.Validation;
using Microsoft.Extensions.Configuration;

namespace Meow.Data.Core.Connection
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
        /// <param name="name">名称</param>
        /// <param name="root">根名称</param>
        public Connection GetConnection(string name, string root = "Connection")
        {
            var connection = new Connection(
                _configuration[$"{root}:{name}:Type"]
                , _configuration[$"{root}:{name}:Server"]
                , _configuration[$"{root}:{name}:Port"]
                , _configuration[$"{root}:{name}:Database"]
                , _configuration[$"{root}:{name}:UserId"]
                , _configuration[$"{root}:{name}:Password"]
            );
            connection.Validate();
            return connection;
        }
    }
}