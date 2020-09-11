using System;
using Meow.Data.Ef.Core.Store;
using Meow.Domain.Core.Model;
using Meow.Domain.Repository;

namespace Meow.Data.Ef
{
    /// <summary>
    /// 增删改查仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class CrudRepository<TEntity> : CrudRepository<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IAggregateRoot<TEntity, Guid>
    {
        /// <summary>
        /// 初始化增删改查仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected CrudRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// 增删改查仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class CrudRepository<TEntity, TKey> : CrudStoreBase<TEntity, TKey>, IRepository<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        /// 初始化增删改查仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected CrudRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}