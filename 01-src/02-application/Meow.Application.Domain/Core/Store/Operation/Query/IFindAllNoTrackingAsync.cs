﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Meow.Application.Domain.Core.Model;

namespace Meow.Application.Domain.Core.Store.Operation.Query
{
    /// <summary>
    /// 查找实体列表
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface IFindAllNoTrackingAsync<TEntity, in TKey> where TEntity : class, IKey<TKey>
    {
        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        Task<List<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}