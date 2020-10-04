using System;
using Meow.Application.Domain.Core.Model;
using Meow.Application.Domain.Core.Store.Operation.Crud;

namespace Meow.Application.Domain.Core.Store
{
    /// <summary>
    /// 增删改查存储器
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    public interface ICrudStore<TEntity> : ICrudStore<TEntity, Guid>
        where TEntity : class, IKey<Guid>, IVersion
    {
    }

    /// <summary>
    /// 增删改查存储器
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface ICrudStore<TEntity, in TKey> : IQueryStore<TEntity, TKey>,
        IAdd<TEntity, TKey>,
        IAddAsync<TEntity, TKey>,
        IUpdate<TEntity, TKey>,
        IUpdateAsync<TEntity, TKey>,
        IRemove<TEntity, TKey>,
        IRemoveAsync<TEntity, TKey>
        where TEntity : class, IKey<TKey>
    {
    }
}