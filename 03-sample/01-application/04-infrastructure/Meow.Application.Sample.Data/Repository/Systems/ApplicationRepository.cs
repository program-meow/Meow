using Meow.Application.Data.Ef;
using Meow.Application.Sample.Data.UnitOfWork;
using Meow.Application.Sample.Domain.Repository;

namespace Meow.Application.Sample.Data.Repository.Systems
{
    /// <summary>
    /// 应用程序仓储
    /// </summary>
    public class ApplicationRepository : CrudRepository<Domain.Model.Application>, IApplicationRepository
    {
        /// <summary>
        /// 初始化应用程序仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public ApplicationRepository(ISampleUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
