using Meow.Application;
using Meow.Sample.Application.Dtos;
using Meow.Sample.Domain.Queries;
using System.Threading.Tasks;

namespace Meow.Sample.Application.Services.Abstractions;

/// <summary>
/// 样本服务
/// </summary>
public interface ISampleService : ICrudService<SampleDto , SampleQuery> {
    /// <summary>
    /// 测试添加
    /// </summary>
    Task TestAddAsync();
    /// <summary>
    /// 测试修改
    /// </summary>
    Task TestUpdateAsync();
}
