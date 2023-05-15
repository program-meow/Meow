﻿using System.Text;
using Meow.Data.Sql.Builder.Param;

namespace Meow.Data.Sql.Builder.Condition;

/// <summary>
/// Sql大于查询条件
/// </summary>
public class GreaterCondition : SqlConditionBase
{
    /// <summary>
    /// 初始化Sql大于查询条件
    /// </summary>
    public GreaterCondition(IParameterManager parameterManager, string column, object value, bool isParameterization)
        : base(parameterManager, column, value, isParameterization)
    {
    }

    /// <summary>
    /// 添加Sql条件
    /// </summary>
    /// <param name="builder">字符串生成器</param>
    /// <param name="column">列名</param>
    /// <param name="value">值</param>
    protected override void AppendCondition(StringBuilder builder, string column, object value)
    {
        builder.AppendFormat("{0}>{1}", column, value);
    }

    /// <summary>
    /// 添加Sql生成器
    /// </summary>
    /// <param name="builder">字符串生成器</param>
    /// <param name="column">列名</param>
    /// <param name="sqlBuilder">Sql生成器</param>
    protected override void AppendSqlBuilder(StringBuilder builder, string column, ISqlBuilder sqlBuilder)
    {
        builder.AppendFormat("{0}>", column);
        builder.Append("(");
        sqlBuilder.AppendTo(builder);
        builder.Append(")");
    }
}