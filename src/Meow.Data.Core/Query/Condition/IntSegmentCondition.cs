﻿using System;
using System.Linq.Expressions;
using Meow.Data.Query.Condition.Internal;
using SystemExpression = System.Linq.Expressions.Expression;

namespace Meow.Data.Query.Condition;

/// <summary>
/// 整数范围过滤条件
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TProperty">属性类型</typeparam>
public class IntSegmentCondition<TEntity, TProperty> : SegmentConditionBase<TEntity, TProperty, int> where TEntity : class
{
    /// <summary>
    /// 范围查询参数对象
    /// </summary>
    private readonly IntQuery _query;

    /// <summary>
    /// 初始化整数范围过滤条件
    /// </summary>
    /// <param name="propertyExpression">属性表达式</param>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <param name="boundary">包含边界</param>
    public IntSegmentCondition(Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max, BoundaryEnum boundary = BoundaryEnum.Both)
        : base(propertyExpression, min, max, boundary)
    {
        _query = new IntQuery();
    }

    /// <summary>
    /// 最小值是否大于最大值
    /// </summary>
    protected override bool IsMinGreaterMax(int? min, int? max)
    {
        return min > max;
    }

    /// <summary>
    /// 获取最小值表达式
    /// </summary>
    protected override SystemExpression GetMinValueExpression()
    {
        _query.MinValue = GetMinValue();
        return SystemExpression.Property(SystemExpression.Constant(_query), "MinValue");
    }

    /// <summary>
    /// 获取最大值表达式
    /// </summary>
    protected override SystemExpression GetMaxValueExpression()
    {
        _query.MaxValue = GetMaxValue();
        return SystemExpression.Property(SystemExpression.Constant(_query), "MaxValue");
    }
}