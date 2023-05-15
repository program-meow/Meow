﻿using System.Text;

namespace Meow.Data.Sql.Builder.Condition;

/// <summary>
/// 空查询条件
/// </summary>
public class NullCondition : ISqlCondition
{
    /// <summary>
    /// 封闭构造方法
    /// </summary>
    private NullCondition()
    {
    }

    /// <summary>
    /// 空查询条件实例
    /// </summary>
    public static readonly NullCondition Instance = new NullCondition();

    /// <summary>
    /// 添加到字符串生成器
    /// </summary>
    /// <param name="builder">字符串生成器</param>
    public void AppendTo(StringBuilder builder)
    {
    }
}