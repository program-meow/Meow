﻿using System;
using System.Linq.Expressions;
using Meow.Domain.Operation;
using Meow.Model;
using Microsoft.EntityFrameworkCore;

namespace Meow.Data.EntityFramework.Filter;

/// <summary>
/// 逻辑删除过滤器
/// </summary>
public class DeleteFilter : FilterBase<IDelete>
{
    /// <summary>
    /// 获取过滤表达式
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public override Expression<Func<TEntity, bool>> GetExpression<TEntity>() where TEntity : class
    {
        return entity => !EF.Property<bool>(entity, "IsDeleted");
    }
}