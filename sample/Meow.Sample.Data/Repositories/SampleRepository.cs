using Meow.Data.EntityFramework;
using Meow.Sample.Domain.Repositories;

namespace Meow.Sample.Data.Repositories
{
    /// <summary>
    /// 样本仓储
    /// </summary>
    public class SampleRepository : RepositoryBase<Domain.Models.Sample>, ISampleRepository
    {
        /// <summary>
        /// 初始化样本仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public SampleRepository(ISampleUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}