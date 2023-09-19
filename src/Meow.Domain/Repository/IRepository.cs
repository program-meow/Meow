using Meow.Data.Store;
using Meow.Domain.Entity;

namespace Meow.Domain.Repository;

/// <summary>
/// 仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IRepository<TEntity> : IRepository<TEntity , Guid>, IQueryRepository<TEntity>
    where TEntity : class, IAggregateRoot<Guid> {
}

/// <summary>
/// 仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">实体标识类型</typeparam>
public interface IRepository<TEntity, in TKey> : IQueryRepository<TEntity , TKey>, IStore<TEntity , TKey> where TEntity : class, IAggregateRoot<TKey> {
}