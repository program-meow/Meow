using System.Collections.Generic;
using System.Threading.Tasks;
using Meow.Dependency;
using Meow.Sample.Domain.Model;

namespace Meow.Sample.Service.Abstractions.Systems
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public interface IApplicationService: IScopeDependency
    {
        /// <summary>
        /// 添加
        /// </summary>
        Task AddAsync();
        /// <summary>
        /// 获取所有
        /// </summary>
        Task<List<Application>> GetAllAsync();
        /// <summary>
        /// 删除所有
        /// </summary>
        Task DeleteAllAsync();
    }
}
