using Meow.Data.EntityFrameworkCore;
using Meow.Sample.Domain.Repositories;
using DomainSample = Meow.Sample.Domain.Models.Sample;

namespace Meow.Sample.Data.Repositories;

/// <summary>
/// 样本仓储
/// </summary>
public class SampleRepository : RepositoryBase<DomainSample>, ISampleRepository {
    /// <summary>
    /// 初始化样本仓储
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    public SampleRepository( ISampleUnitOfWork unitOfWork ) : base( unitOfWork ) {
    }
}
