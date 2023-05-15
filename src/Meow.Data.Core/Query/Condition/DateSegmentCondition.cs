using System;
using System.Linq.Expressions;
using Meow.Extension;
using Meow.Math;
using SystemExpression = System.Linq.Expressions.Expression;

namespace Meow.Data.Query.Condition;

/// <summary>
/// 日期范围过滤条件 - 不包含时间
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TProperty">属性类型</typeparam>
public class DateSegmentCondition<TEntity, TProperty> : DateTimeSegmentCondition<TEntity, TProperty> where TEntity : class
{
    /// <summary>
    /// 初始化日期范围过滤条件 - 不包含时间
    /// </summary>
    /// <param name="propertyExpression">属性表达式</param>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <param name="boundary">包含边界</param>
    public DateSegmentCondition(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max, BoundaryEnum boundary)
        : base(propertyExpression, min, max, boundary)
    {
    }

    /// <summary>
    /// 获取最小值
    /// </summary>
    protected override SystemExpression GetMinValueExpression()
    {
        DateTime minValue = GetMinValue().SafeValue().Date;
        if (GetBoundary() == BoundaryEnum.Right || GetBoundary() == BoundaryEnum.Neither)
            minValue = minValue.AddDays(1);
        return GetMinValueExpression(minValue);
    }

    /// <summary>
    /// 获取最大值
    /// </summary>
    protected override SystemExpression GetMaxValueExpression()
    {
        DateTime maxValue = GetMaxValue().SafeValue().Date;
        if (GetBoundary() == BoundaryEnum.Right || GetBoundary() == BoundaryEnum.Both)
            maxValue = maxValue.AddDays(1);
        return GetMaxValueExpression(maxValue);
    }

    /// <summary>
    /// 创建左操作符
    /// </summary>
    protected override OperatorEnum CreateLeftOperator(BoundaryEnum? boundary)
    {
        return OperatorEnum.GreaterEqual;
    }

    /// <summary>
    /// 创建右操作符
    /// </summary>
    protected override OperatorEnum CreateRightOperator(BoundaryEnum? boundary)
    {
        return OperatorEnum.Less;
    }
}