using Meow.Dependency;

namespace Meow.Sample.Service
{
    /// <summary>
    /// 配置提供器
    /// </summary>
    public interface IConfigProvider : IScopeDependency
    {
        /// <summary>
        /// 获取搜索配置
        /// </summary>
        void GetConfig();
    }

}
