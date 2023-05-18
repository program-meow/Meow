using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using SystemExpression = System.Linq.Expressions.Expression;
using SystemType = System.Type;

namespace Meow.Extension;

/// <summary>
/// 表达式扩展
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name</param>
    public static SystemType GetType(this SystemExpression expression)
    {
        return Helper.Expression.GetType(expression);
    }

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name</param>
    public static MemberInfo GetMember(this SystemExpression expression)
    {
        return Helper.Expression.GetMember(expression);
    }

    /// <summary>
    /// 获取成员表达式
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <param name="right">取表达式右侧,(l,r) => l.id == r.id，设置为true,返回r.id表达式</param>
    public static MemberExpression GetMemberExpression(this SystemExpression expression, bool right = false)
    {
        return Helper.Expression.GetMemberExpression(expression, right);
    }

    /// <summary>
    /// 获取成员名称，范例：t => t.A.Name,返回 A.Name
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name</param>
    public static string GetName(this SystemExpression expression)
    {
        return Helper.Expression.GetName(expression);
    }

    /// <summary>
    /// 获取成员名称
    /// </summary>
    public static string GetMemberName(this MemberExpression memberExpression)
    {
        return Helper.Expression.GetMemberName(memberExpression);
    }

    /// <summary>
    /// 获取名称列表，范例：t => new object[] { t.A.B, t.C },返回A.B,C
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="expression">属性集合表达式,范例：t => new object[]{t.A,t.B}</param>
    public static List<string> GetNames<T>(this Expression<Func<T, object[]>> expression)
    {
        return Helper.Expression.GetNames<T>(expression);

    }

    /// <summary>
    /// 获取最后一级成员名称，范例：t => t.A.Name,返回 Name
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name</param>
    /// <param name="right">取表达式右侧,(l,r) => l.LId == r.RId，设置为true,返回RId</param>
    public static string GetLastName(this SystemExpression expression, bool right = false)
    {
        return Helper.Expression.GetLastName(expression, right);
    }

    /// <summary>
    /// 获取最后一级成员名称列表，范例：t => new object[] { t.A.B, t.C },返回B,C
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="expression">属性集合表达式,范例：t => new object[]{t.A,t.B}</param>
    public static List<string> GetLastNames<T>(this Expression<Func<T, object[]>> expression)
    {
        return Helper.Expression.GetLastNames<T>(expression);
    }

    /// <summary>
    /// 获取值,范例：t => t.Name == "A",返回 A
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name == "A"</param>
    public static object GetValue(this SystemExpression expression)
    {
        return Helper.Expression.GetValue(expression);
    }

    /// <summary>
    /// 获取查询操作符,范例：t => t.Name == "A",返回 Operator.Equal
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name == "A"</param>
    public static Meow.Math.OperatorEnum? GetOperator(this SystemExpression expression)
    {
        return Helper.Expression.GetOperator(expression);
    }

    /// <summary>
    /// 获取参数，范例：t.Name,返回 t
    /// </summary>
    /// <param name="expression">表达式，范例：t.Name</param>
    public static ParameterExpression GetParameter(this SystemExpression expression)
    {
        return Helper.Expression.GetParameter(expression);
    }

    /// <summary>
    /// 获取分组的谓词表达式，通过Or进行分组
    /// </summary>
    /// <param name="expression">谓词表达式</param>
    public static List<List<SystemExpression>> GetGroupPredicates(this SystemExpression expression)
    {
        return Helper.Expression.GetGroupPredicates(expression);
    }

    /// <summary>
    /// 获取查询条件个数
    /// </summary>
    /// <param name="expression">谓词表达式,范例1：t => t.Name == "A" ，结果1。
    /// 范例2：t => t.Name == "A" &amp;&amp; t.Age =1 ，结果2。</param>
    public static int GetConditionCount(this LambdaExpression expression)
    {
        return Helper.Expression.GetConditionCount(expression);
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="expression">属性表达式</param>
    public static TAttribute GetAttribute<TAttribute>(this SystemExpression expression) where TAttribute : Attribute
    {
        return Helper.Expression.GetAttribute<TAttribute>(expression);
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="propertyExpression">属性表达式</param>
    public static TAttribute GetAttribute<TEntity, TProperty, TAttribute>(this Expression<Func<TEntity, TProperty>> propertyExpression) where TAttribute : Attribute
    {
        return Helper.Expression.GetAttribute<TEntity, TProperty, TAttribute>(propertyExpression);
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="propertyExpression">属性表达式</param>
    public static TAttribute GetAttribute<TProperty, TAttribute>(this Expression<Func<TProperty>> propertyExpression) where TAttribute : Attribute
    {
        return Helper.Expression.GetAttribute<TProperty, TAttribute>(propertyExpression);
    }

    /// <summary>
    /// 获取特性列表
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="propertyExpression">属性表达式</param>
    public static IEnumerable<TAttribute> GetAttributes<TEntity, TProperty, TAttribute>(this Expression<Func<TEntity, TProperty>> propertyExpression) where TAttribute : Attribute
    {
        return Helper.Expression.GetAttributes<TEntity, TProperty, TAttribute>(propertyExpression);
    }

    /// <summary>
    /// 获取常量表达式
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="expression">表达式</param>
    public static ConstantExpression Constant(this object value, SystemExpression expression = null)
    {
        return Helper.Expression.Constant(value, expression);
    }

    /// <summary>
    /// 创建等于运算lambda表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> Equal<T>(this object value, string propertyName)
    {
        return Helper.Expression.Equal<T>(propertyName, value);
    }

    /// <summary>
    /// 创建不等于运算lambda表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> NotEqual<T>(this object value, string propertyName)
    {
        return Helper.Expression.NotEqual<T>(propertyName, value);

    }

    /// <summary>
    /// 创建大于运算lambda表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> Greater<T>(this object value, string propertyName)
    {
        return Helper.Expression.Greater<T>(propertyName, value);
    }

    /// <summary>
    /// 创建大于等于运算lambda表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> GreaterEqual<T>(this object value, string propertyName)
    {
        return Helper.Expression.GreaterEqual<T>(propertyName, value);
    }

    /// <summary>
    /// 创建小于运算lambda表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> Less<T>(this object value, string propertyName)
    {
        return Helper.Expression.Less<T>(propertyName, value);
    }

    /// <summary>
    /// 创建小于等于运算lambda表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> LessEqual<T>(this object value, string propertyName)
    {
        return Helper.Expression.LessEqual<T>(propertyName, value);
    }

    /// <summary>
    /// 调用StartsWith方法
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> Starts<T>(this object value, string propertyName)
    {
        return Helper.Expression.Starts<T>(propertyName, value);
    }

    /// <summary>
    /// 调用EndsWith方法
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> Ends<T>(this object value, string propertyName)
    {
        return Helper.Expression.Ends<T>(propertyName, value);
    }

    /// <summary>
    /// 调用Contains方法
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    public static Expression<Func<T, bool>> Contains<T>(this object value, string propertyName)
    {
        return Helper.Expression.Contains<T>(propertyName, value);
    }

    /// <summary>
    /// 解析为谓词表达式
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="propertyName">属性名</param>
    /// <param name="operator">运算符</param>
    public static Expression<Func<T, bool>> ParsePredicate<T>(this object value, string propertyName, Meow.Math.OperatorEnum @operator)
    {
        return Helper.Expression.ParsePredicate<T>(propertyName, value, @operator);
    }

    /// <summary>
    /// 创建属性表达式
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <param name="propertyName">属性名,支持多级属性名，用句点分隔，范例：Customer.Name</param>
    public static SystemExpression Property(this SystemExpression expression, string propertyName)
    {
        return Helper.Expression.Property(expression, propertyName);
    }

    /// <summary>
    /// 创建属性表达式
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <param name="member">属性</param>
    public static SystemExpression Property(this SystemExpression expression, MemberInfo member)
    {
        return Helper.Expression.Property(expression, member);
    }

    /// <summary>
    /// 与操作表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression And(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.And(left, right);
    }

    /// <summary>
    /// 与操作表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        return Helper.Expression.And<T>(left, right);
    }

    /// <summary>
    /// 或操作表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression Or(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.Or(left, right);
    }

    /// <summary>
    /// 或操作表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        return Helper.Expression.Or<T>(left, right);
    }

    /// <summary>
    /// 获取lambda表达式的值
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public static object Value<T>(this Expression<Func<T, bool>> expression)
    {
        return Helper.Expression.Value<T>(expression);
    }

    /// <summary>
    /// 创建等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression Equal(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.Equal(left, right);
    }

    /// <summary>
    /// 创建等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression Equal(this SystemExpression left, object value)
    {
        return Helper.Expression.Equal(left, value);
    }

    /// <summary>
    /// 创建不等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression NotEqual(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.NotEqual(left, right);
    }

    /// <summary>
    /// 创建不等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression NotEqual(this SystemExpression left, object value)
    {
        return Helper.Expression.NotEqual(left, value);
    }

    /// <summary>
    /// 创建大于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression Greater(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.Greater(left, right);
    }

    /// <summary>
    /// 创建大于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression Greater(this SystemExpression left, object value)
    {
        return Helper.Expression.Greater(left, value);
    }

    /// <summary>
    /// 创建大于等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression GreaterEqual(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.GreaterEqual(left, right);
    }

    /// <summary>
    /// 创建大于等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression GreaterEqual(this SystemExpression left, object value)
    {
        return Helper.Expression.GreaterEqual(left, value);
    }

    /// <summary>
    /// 创建小于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression Less(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.Less(left, right);
    }

    /// <summary>
    /// 创建小于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression Less(this SystemExpression left, object value)
    {
        return Helper.Expression.Less(left, value);
    }

    /// <summary>
    /// 创建小于等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    public static SystemExpression LessEqual(this SystemExpression left, SystemExpression right)
    {
        return Helper.Expression.LessEqual(left, right);
    }

    /// <summary>
    /// 创建小于等于运算表达式
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression LessEqual(this SystemExpression left, object value)
    {
        return Helper.Expression.LessEqual(left, value);
    }

    /// <summary>
    /// 头匹配
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression StartsWith(this SystemExpression left, object value)
    {
        return Helper.Expression.StartsWith(left, value);
    }

    /// <summary>
    /// 尾匹配
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression EndsWith(this SystemExpression left, object value)
    {
        return Helper.Expression.EndsWith(left, value);
    }

    /// <summary>
    /// 模糊匹配
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="value">值</param>
    public static SystemExpression Contains(this SystemExpression left, object value)
    {
        return Helper.Expression.Contains(left, value);
    }

    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="operator">运算符</param>
    /// <param name="value">值</param>
    public static SystemExpression Operation(this SystemExpression left, Meow.Math.OperatorEnum @operator, object value)
    {
        return Helper.Expression.Operation(left, @operator, value);
    }

    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="operator">运算符</param>
    /// <param name="value">值</param>
    public static SystemExpression Operation(this SystemExpression left, Meow.Math.OperatorEnum @operator, SystemExpression value)
    {
        return Helper.Expression.Operation(left, @operator, value);
    }

    /// <summary>
    /// 创建调用方法表达式
    /// </summary>
    /// <param name="instance">调用的实例</param>
    /// <param name="methodName">方法名</param>
    /// <param name="values">参数值列表</param>
    public static SystemExpression Call(this SystemExpression instance, string methodName, params SystemExpression[] values)
    {
        return Helper.Expression.Call(instance, methodName, values);
    }

    /// <summary>
    /// 创建调用方法表达式
    /// </summary>
    /// <param name="instance">调用的实例</param>
    /// <param name="methodName">方法名</param>
    /// <param name="values">参数值列表</param>
    public static SystemExpression Call(this SystemExpression instance, string methodName, params object[] values)
    {
        return Helper.Expression.Call(instance, methodName, values);
    }

    /// <summary>
    /// 创建调用方法表达式
    /// </summary>
    /// <param name="instance">调用的实例</param>
    /// <param name="methodName">方法名</param>
    /// <param name="paramTypes">参数类型列表</param>
    /// <param name="values">参数值列表</param>
    public static SystemExpression Call(this SystemExpression instance, string methodName, SystemType[] paramTypes, params object[] values)
    {
        return Helper.Expression.Call(instance, methodName, paramTypes, values);
    }

    /// <summary>
    /// 组合表达式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="first">左操作数</param>
    /// <param name="second">右操作数</param>
    /// <param name="merge">合并操作</param>
    internal static Expression<T> Compose<T>(
        this Expression<T> first
        , Expression<T> second
        , Func<
            SystemExpression
            , SystemExpression
            , SystemExpression> merge)
    {
        return Helper.Expression.Compose<T>(first, second, merge);
    }

    /// <summary>
    /// 创建Lambda表达式
    /// </summary>
    /// <typeparam name="TDelegate">委托类型</typeparam>
    /// <param name="body">表达式</param>
    /// <param name="parameters">参数列表</param>
    public static Expression<TDelegate> ToLambda<TDelegate>(this SystemExpression body, params ParameterExpression[] parameters)
    {
        return Helper.Expression.ToLambda<TDelegate>(body, parameters);
    }

    /// <summary>
    /// 创建谓词表达式
    /// </summary>
    /// <typeparam name="T">委托类型</typeparam>
    /// <param name="body">表达式</param>
    /// <param name="parameters">参数列表</param>
    public static Expression<Func<T, bool>> ToPredicate<T>(this SystemExpression body, params ParameterExpression[] parameters)
    {
        return Helper.Expression.ToPredicate<T>(body, parameters);
    }
}