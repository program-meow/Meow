﻿using Meow.Query.Core;
using Meow.Query.Pager;

namespace Meow.Application.Domain.Core.Repository
{
    /// <summary>
    /// 查询对象
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IQueryBase<TEntity> : ICriteria<TEntity> where TEntity : class
    {
        /// <summary>
        /// 获取排序条件
        /// </summary>
        string GetOrder();
        /// <summary>
        /// 获取分页参数
        /// </summary>
        IPager GetPager();
    }
}