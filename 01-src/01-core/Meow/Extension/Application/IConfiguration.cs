using Microsoft.Extensions.Configuration;

namespace Meow.Extension.Application
{
    /// <summary>
    /// 服务配置扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="configuration">配置</param>
        /// <param name="name">名称</param>
        /// <param name="root">根名称</param>
        public static string GetConnection(this IConfiguration configuration, string name, string root = "Connection")
        {
            return configuration?.GetSection(root)?[name];
        }
    }
}