using Meow.Domain.Repository;
using DomainSample = Meow.Sample.Domain.Models.Sample;

namespace Meow.Sample.Domain.Repositories;

/// <summary>
/// 样本仓储
/// </summary>
public interface ISampleRepository : IRepository<DomainSample> {
}