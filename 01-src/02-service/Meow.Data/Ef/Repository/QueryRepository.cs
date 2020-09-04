using System;
using Meow.Data.Core.UnitOfWork;
using Meow.Data.Ef.Core.Base;
using Meow.Domain.Core.Model;
using Meow.Domain.Repository;

namespace Meow.Data.Ef.Repository
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class QueryRepository<TEntity> : QueryRepository<TEntity, Guid>, IQueryRepository<TEntity>
        where TEntity : class, IAggregateRoot<TEntity, Guid>
    {
        /// <summary>
        /// 初始化仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected QueryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// 查询仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class QueryRepository<TEntity, TKey> : QueryStoreBase<TEntity, TKey>, IQueryRepository<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        /// 初始化仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected QueryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}