﻿using System;
using Meow.Data.Store;
using Meow.Domain.Entity;

namespace Meow.Domain.Repository;

/// <summary>
/// 查询仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, Guid> where TEntity : class, IAggregateRoot<Guid>
{
}

/// <summary>
/// 查询仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">实体标识类型</typeparam>
public interface IQueryRepository<TEntity, in TKey> : IQueryStore<TEntity, TKey> where TEntity : class, IAggregateRoot<TKey>
{
}