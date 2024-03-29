﻿using Meow.Extension;
using Meow.Math;

namespace Meow.Expression;

/// <summary>
/// 谓词表达式生成器
/// </summary>
public class PredicateExpressionBuilder<TEntity> {
    /// <summary>
    /// 参数
    /// </summary>
    private readonly ParameterExpression _parameter;
    /// <summary>
    /// 结果表达式
    /// </summary>
    private SystemExpression _result;

    /// <summary>
    /// 初始化谓词表达式生成器
    /// </summary>
    public PredicateExpressionBuilder() {
        _parameter = Meow.Helper.Expression.CreateParameter<TEntity>();
    }

    /// <summary>
    /// 获取参数
    /// </summary>
    public ParameterExpression GetParameter() {
        return _parameter;
    }

    /// <summary>
    /// 添加表达式
    /// </summary>
    /// <param name="property">属性表达式</param>
    /// <param name="operator">运算符</param>
    /// <param name="value">值</param>
    public void Append<TProperty>( Expression<Func<TEntity , TProperty>> property , OperatorEnum @operator , object value ) {
        _result = _result.And( _parameter.Property( Meow.Helper.Expression.GetMember( property ) ).Operation( @operator , value ) );
    }

    /// <summary>
    /// 添加表达式
    /// </summary>
    /// <param name="property">属性表达式</param>
    /// <param name="operator">运算符</param>
    /// <param name="value">值</param>
    public void Append<TProperty>( Expression<Func<TEntity , TProperty>> property , OperatorEnum @operator , SystemExpression value ) {
        _result = _result.And( _parameter.Property( Meow.Helper.Expression.GetMember( property ) ).Operation( @operator , value ) );
    }

    /// <summary>
    /// 添加表达式
    /// </summary>
    /// <param name="property">属性名</param>
    /// <param name="operator">运算符</param>
    /// <param name="value">值</param>
    public void Append( string property , OperatorEnum @operator , object value ) {
        _result = _result.And( _parameter.Property( property ).Operation( @operator , value ) );
    }

    /// <summary>
    /// 添加表达式
    /// </summary>
    /// <param name="property">属性名</param>
    /// <param name="operator">运算符</param>
    /// <param name="value">值</param>
    public void Append( string property , OperatorEnum @operator , SystemExpression value ) {
        _result = _result.And( _parameter.Property( property ).Operation( @operator , value ) );
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear() {
        _result = null;
    }

    /// <summary>
    /// 转换为Lambda表达式
    /// </summary>
    public Expression<Func<TEntity , bool>> ToLambda() {
        return _result.ToLambda<Func<TEntity , bool>>( _parameter );
    }
}