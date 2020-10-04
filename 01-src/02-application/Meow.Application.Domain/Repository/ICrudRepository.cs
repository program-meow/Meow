using System;
using Meow.Application.Domain.Core.Model;
using Meow.Application.Domain.Core.Store;

namespace Meow.Application.Domain.Repository
{
    /// <summary>
    /// 增删改查仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, Guid>
        where TEntity : class, IAggregateRoot, IKey<Guid>
    {
    }

    /// <summary>
    /// 增删改查仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface ICrudRepository<TEntity, in TKey> : IQueryRepository<TEntity, TKey>, ICrudStore<TEntity, TKey>
        where TEntity : class, IAggregateRoot, IKey<TKey>
    {
    }
}