using Meow.Application;
using Meow.Sample.Application.Dtos;
using Meow.Sample.Domain.Queries;

namespace Meow.Sample.Application.Services.Abstractions {
    /// <summary>
    /// 样本服务
    /// </summary>
    public interface ISampleService : ICrudService<SampleDto, SampleQuery> {
        
    }
}