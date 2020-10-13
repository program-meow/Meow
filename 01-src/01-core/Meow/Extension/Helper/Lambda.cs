using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Meow.Expression;
using Meow.Helper;
using Meow.Mathematics.Enum;
using MicrosoftExpression = System.Linq.Expressions.Expression;

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
        public static MicrosoftExpression Property(this MicrosoftExpression expression, string propertyName)
        {
            if (propertyName.All(t => t != '.'))
                return MicrosoftExpression.Property(expression, propertyName);
            var propertyNameList = propertyName.Split('.');
            MicrosoftExpression result = null;
            for (int i = 0; i < propertyNameList.Length; i++)
            {
                if (i == 0)
                {
                    result = MicrosoftExpression.Property(expression, propertyNameList[0]);
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
        public static MicrosoftExpression Property(this MicrosoftExpression expression, MemberInfo member)
        {
            return MicrosoftExpression.MakeMemberAccess(expression, member);
        }

        #endregion

        #region And(与表达式)

        /// <summary>
        /// 与操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static MicrosoftExpression And(this MicrosoftExpression left, MicrosoftExpression right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return MicrosoftExpression.AndAlso(left, right);
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
            return left.Compose(right, MicrosoftExpression.AndAlso);
        }

        #endregion

        #region Or(或表达式)

        /// <summary>
        /// 或操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static MicrosoftExpression Or(this MicrosoftExpression left, MicrosoftExpression right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return MicrosoftExpression.OrElse(left, right);
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
            return left.Compose(right, MicrosoftExpression.OrElse);
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
        public static MicrosoftExpression Equal(this MicrosoftExpression left, MicrosoftExpression right)
        {
            return MicrosoftExpression.Equal(left, right);
        }

        /// <summary>
        /// 创建等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static MicrosoftExpression Equal(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression NotEqual(this MicrosoftExpression left, MicrosoftExpression right)
        {
            return MicrosoftExpression.NotEqual(left, right);
        }

        /// <summary>
        /// 创建不等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static MicrosoftExpression NotEqual(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression Greater(this MicrosoftExpression left, MicrosoftExpression right)
        {
            return MicrosoftExpression.GreaterThan(left, right);
        }

        /// <summary>
        /// 创建大于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static MicrosoftExpression Greater(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression GreaterEqual(this MicrosoftExpression left, MicrosoftExpression right)
        {
            return MicrosoftExpression.GreaterThanOrEqual(left, right);
        }

        /// <summary>
        /// 创建大于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static MicrosoftExpression GreaterEqual(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression Less(this MicrosoftExpression left, MicrosoftExpression right)
        {
            return MicrosoftExpression.LessThan(left, right);
        }

        /// <summary>
        /// 创建小于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static MicrosoftExpression Less(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression LessEqual(this MicrosoftExpression left, MicrosoftExpression right)
        {
            return MicrosoftExpression.LessThanOrEqual(left, right);
        }

        /// <summary>
        /// 创建小于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static MicrosoftExpression LessEqual(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression StartsWith(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression EndsWith(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression Contains(this MicrosoftExpression left, object value)
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
        public static MicrosoftExpression Operation(this MicrosoftExpression left, Operator @operator, object value)
        {
            return @operator switch
            {
                Operator.Equal => left.Equal(value),
                Operator.NotEqual => left.NotEqual(value),
                Operator.Greater => left.Greater(value),
                Operator.GreaterEqual => left.GreaterEqual(value),
                Operator.Less => left.Less(value),
                Operator.LessEqual => left.LessEqual(value),
                Operator.Starts => left.StartsWith(value),
                Operator.Ends => left.EndsWith(value),
                Operator.Contains => left.Contains(value),
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="operator">运算符</param>
        /// <param name="value">值</param>
        public static MicrosoftExpression Operation(this MicrosoftExpression left, Operator @operator, MicrosoftExpression value)
        {
            return @operator switch
            {
                Operator.Equal => left.Equal(value),
                Operator.NotEqual => left.NotEqual(value),
                Operator.Greater => left.Greater(value),
                Operator.GreaterEqual => left.GreaterEqual(value),
                Operator.Less => left.Less(value),
                Operator.LessEqual => left.LessEqual(value),
                _ => throw new NotImplementedException()
            };
        }

        #endregion

        #region Call(调用方法表达式)

        /// <summary>
        /// 创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="values">参数值列表</param>
        public static MicrosoftExpression Call(this MicrosoftExpression instance, string methodName, params MicrosoftExpression[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            var methodInfo = instance.Type.GetMethod(methodName);
            if (methodInfo == null)
                return null;
            return MicrosoftExpression.Call(instance, methodInfo, values);
        }

        /// <summary>
        /// 创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="values">参数值列表</param>
        public static MicrosoftExpression Call(this MicrosoftExpression instance, string methodName, params object[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            var methodInfo = instance.Type.GetMethod(methodName);
            if (methodInfo == null)
                return null;
            if (values == null || values.Length == 0)
                return MicrosoftExpression.Call(instance, methodInfo);
            return MicrosoftExpression.Call(instance, methodInfo, values.Select(MicrosoftExpression.Constant));
        }

        /// <summary>
        /// 创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="paramTypes">参数类型列表</param>
        /// <param name="values">参数值列表</param>
        public static MicrosoftExpression Call(this MicrosoftExpression instance, string methodName, System.Type[] paramTypes, params object[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            var methodInfo = instance.Type.GetMethod(methodName, paramTypes);
            if (methodInfo == null)
                return null;
            if (values == null || values.Length == 0)
                return MicrosoftExpression.Call(instance, methodInfo);
            return MicrosoftExpression.Call(instance, methodInfo, values.Select(MicrosoftExpression.Constant));
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
            Func<MicrosoftExpression, MicrosoftExpression, MicrosoftExpression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return MicrosoftExpression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        #endregion

        #region ToLambda(创建Lambda表达式)

        /// <summary>
        /// 创建Lambda表达式
        /// </summary>
        /// <typeparam name="TDelegate">委托类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数列表</param>
        public static Expression<TDelegate> ToLambda<TDelegate>(this MicrosoftExpression body, params ParameterExpression[] parameters)
        {
            if (body == null)
                return null;
            return MicrosoftExpression.Lambda<TDelegate>(body, parameters);
        }

        #endregion

        #region ToPredicate(创建谓词表达式)

        /// <summary>
        /// 创建谓词表达式
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数列表</param>
        public static Expression<Func<T, bool>> ToPredicate<T>(this MicrosoftExpression body, params ParameterExpression[] parameters)
        {
            return ToLambda<Func<T, bool>>(body, parameters);
        }

        #endregion
    }
}