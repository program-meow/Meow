using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Meow.Expression;
using Meow.Helper;
using Meow.Mathematics.Enum;
using SystemExpression = System.Linq.Expressions.Expression;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// Lambda表达式
    /// </summary>
    public static partial class Extension
    {
        #region Property(属性表达式)

        /// <summary>
        /// 创建属性表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="propertyName">属性名,支持多级属性名，与句点分隔，范例：Customer.Name</param>
        public static SystemExpression Property(this SystemExpression expression, string propertyName)
        {
            if (propertyName.All(t => t != '.'))
                return SystemExpression.Property(expression, propertyName);
            var propertyNameList = propertyName.Split('.');
            SystemExpression result = null;
            for (int i = 0; i < propertyNameList.Length; i++)
            {
                if (i == 0)
                {
                    result = SystemExpression.Property(expression, propertyNameList[0]);
                    continue;
                }
                result = result.Property(propertyNameList[i]);
            }
            return result;
        }

        /// <summary>
        /// 创建属性表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="member">属性</param>
        public static SystemExpression Property(this SystemExpression expression, MemberInfo member)
        {
            return SystemExpression.MakeMemberAccess(expression, member);
        }

        #endregion

        #region And(与表达式)

        /// <summary>
        /// 与操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression And(this SystemExpression left, SystemExpression right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return SystemExpression.AndAlso(left, right);
        }

        /// <summary>
        /// 与操作表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return left.Compose(right, SystemExpression.AndAlso);
        }

        #endregion

        #region Or(或表达式)

        /// <summary>
        /// 或操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Or(this SystemExpression left, SystemExpression right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return SystemExpression.OrElse(left, right);
        }

        /// <summary>
        /// 或操作表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return left.Compose(right, SystemExpression.OrElse);
        }

        #endregion

        #region Value(获取lambda表达式的值)

        /// <summary>
        /// 获取lambda表达式的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public static object Value<T>(this Expression<Func<T, bool>> expression)
        {
            return Lambda.GetValue(expression);
        }

        #endregion

        #region Equal(等于表达式)

        /// <summary>
        /// 创建等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Equal(this SystemExpression left, SystemExpression right)
        {
            return SystemExpression.Equal(left, right);
        }

        /// <summary>
        /// 创建等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Equal(this SystemExpression left, object value)
        {
            return left.Equal(Lambda.Constant(value, left));
        }

        #endregion

        #region NotEqual(不等于表达式)

        /// <summary>
        /// 创建不等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression NotEqual(this SystemExpression left, SystemExpression right)
        {
            return SystemExpression.NotEqual(left, right);
        }

        /// <summary>
        /// 创建不等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression NotEqual(this SystemExpression left, object value)
        {
            return left.NotEqual(Lambda.Constant(value, left));
        }

        #endregion

        #region Greater(大于表达式)

        /// <summary>
        /// 创建大于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Greater(this SystemExpression left, SystemExpression right)
        {
            return SystemExpression.GreaterThan(left, right);
        }

        /// <summary>
        /// 创建大于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Greater(this SystemExpression left, object value)
        {
            return left.Greater(Lambda.Constant(value, left));
        }

        #endregion

        #region GreaterEqual(大于等于表达式)

        /// <summary>
        /// 创建大于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression GreaterEqual(this SystemExpression left, SystemExpression right)
        {
            return SystemExpression.GreaterThanOrEqual(left, right);
        }

        /// <summary>
        /// 创建大于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression GreaterEqual(this SystemExpression left, object value)
        {
            return left.GreaterEqual(Lambda.Constant(value, left));
        }

        #endregion

        #region Less(小于表达式)

        /// <summary>
        /// 创建小于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Less(this SystemExpression left, SystemExpression right)
        {
            return SystemExpression.LessThan(left, right);
        }

        /// <summary>
        /// 创建小于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Less(this SystemExpression left, object value)
        {
            return left.Less(Lambda.Constant(value, left));
        }

        #endregion

        #region LessEqual(小于等于表达式)

        /// <summary>
        /// 创建小于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression LessEqual(this SystemExpression left, SystemExpression right)
        {
            return SystemExpression.LessThanOrEqual(left, right);
        }

        /// <summary>
        /// 创建小于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression LessEqual(this SystemExpression left, object value)
        {
            return left.LessEqual(Lambda.Constant(value, left));
        }

        #endregion

        #region StartsWith(头匹配)

        /// <summary>
        /// 头匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression StartsWith(this SystemExpression left, object value)
        {
            return left.Call("StartsWith", new[] { typeof(string) }, value);
        }

        #endregion

        #region EndsWith(尾匹配)

        /// <summary>
        /// 尾匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression EndsWith(this SystemExpression left, object value)
        {
            return left.Call("EndsWith", new[] { typeof(string) }, value);
        }

        #endregion

        #region Contains(模糊匹配)

        /// <summary>
        /// 模糊匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Contains(this SystemExpression left, object value)
        {
            return left.Call("Contains", new[] { typeof(string) }, value);
        }

        #endregion

        #region Operation(操作)

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="operator">运算符</param>
        /// <param name="value">值</param>
        public static SystemExpression Operation(this SystemExpression left, Operator @operator, object value)
        {
            switch (@operator)
            {
                case Operator.Equal:
                    return left.Equal(value);
                case Operator.NotEqual:
                    return left.NotEqual(value);
                case Operator.Greater:
                    return left.Greater(value);
                case Operator.GreaterEqual:
                    return left.GreaterEqual(value);
                case Operator.Less:
                    return left.Less(value);
                case Operator.LessEqual:
                    return left.LessEqual(value);
                case Operator.Starts:
                    return left.StartsWith(value);
                case Operator.Ends:
                    return left.EndsWith(value);
                case Operator.Contains:
                    return left.Contains(value);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="operator">运算符</param>
        /// <param name="value">值</param>
        public static SystemExpression Operation(this SystemExpression left, Operator @operator, SystemExpression value)
        {
            switch (@operator)
            {
                case Operator.Equal:
                    return left.Equal(value);
                case Operator.NotEqual:
                    return left.NotEqual(value);
                case Operator.Greater:
                    return left.Greater(value);
                case Operator.GreaterEqual:
                    return left.GreaterEqual(value);
                case Operator.Less:
                    return left.Less(value);
                case Operator.LessEqual:
                    return left.LessEqual(value);
            }
            throw new NotImplementedException();
        }

        #endregion

        #region Call(调用方法表达式)

        /// <summary>
        /// 创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="values">参数值列表</param>
        public static SystemExpression Call(this SystemExpression instance, string methodName, params SystemExpression[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            var methodInfo = instance.Type.GetMethod(methodName);
            if (methodInfo == null)
                return null;
            return SystemExpression.Call(instance, methodInfo, values);
        }

        /// <summary>
        /// 创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="values">参数值列表</param>
        public static SystemExpression Call(this SystemExpression instance, string methodName, params object[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            var methodInfo = instance.Type.GetMethod(methodName);
            if (methodInfo == null)
                return null;
            if (values == null || values.Length == 0)
                return SystemExpression.Call(instance, methodInfo);
            return SystemExpression.Call(instance, methodInfo, values.Select(SystemExpression.Constant));
        }

        /// <summary>
        /// 创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="paramTypes">参数类型列表</param>
        /// <param name="values">参数值列表</param>
        public static SystemExpression Call(this SystemExpression instance, string methodName, System.Type[] paramTypes, params object[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            var methodInfo = instance.Type.GetMethod(methodName, paramTypes);
            if (methodInfo == null)
                return null;
            if (values == null || values.Length == 0)
                return SystemExpression.Call(instance, methodInfo);
            return SystemExpression.Call(instance, methodInfo, values.Select(SystemExpression.Constant));
        }

        #endregion

        #region Compose(组合表达式)

        /// <summary>
        /// 组合表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="first">左操作数</param>
        /// <param name="second">右操作数</param>
        /// <param name="merge">合并操作</param>
        internal static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<SystemExpression, SystemExpression, SystemExpression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return SystemExpression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        #endregion

        #region ToLambda(创建Lambda表达式)

        /// <summary>
        /// 创建Lambda表达式
        /// </summary>
        /// <typeparam name="TDelegate">委托类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数列表</param>
        public static Expression<TDelegate> ToLambda<TDelegate>(this SystemExpression body, params ParameterExpression[] parameters)
        {
            if (body == null)
                return null;
            return SystemExpression.Lambda<TDelegate>(body, parameters);
        }

        #endregion

        #region ToPredicate(创建谓词表达式)

        /// <summary>
        /// 创建谓词表达式
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数列表</param>
        public static Expression<Func<T, bool>> ToPredicate<T>(this SystemExpression body, params ParameterExpression[] parameters)
        {
            return ToLambda<Func<T, bool>>(body, parameters);
        }

        #endregion
    }
}