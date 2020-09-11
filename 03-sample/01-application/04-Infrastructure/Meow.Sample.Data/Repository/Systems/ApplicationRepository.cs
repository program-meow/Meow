using Meow.Data.Ef;
using Meow.Sample.Data.UnitOfWork;
using Meow.Sample.Domain.Model;
using Meow.Sample.Domain.Repository;

namespace Meow.Sample.Data.Repository.Systems
{
    /// <summary>
    /// 应用程序仓储
    /// </summary>
    public class ApplicationRepository : CrudRepository<Application>, IApplicationRepository
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
