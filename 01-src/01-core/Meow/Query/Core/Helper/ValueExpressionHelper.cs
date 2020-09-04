using System;
using Meow.Extension.Helper;
using SystemExpression = System.Linq.Expressions.Expression;

namespace Meow.Query.Core.Helper
{
    /// <summary>
    /// 值表达式操作
    /// </summary>
    public static class ValueExpressionHelper
    {
        /// <summary>
        /// 获取日期常量表达式
        /// </summary>
        /// <param name="value">日期值</param>
        /// <param name="isNull">日期是否可空</param>
        public static SystemExpression CreateDateTimeExpression(object value, bool isNull = true)
        {
            Type type = isNull ? typeof(DateTime?) : typeof(DateTime);
            return CreateDateTimeExpression(value, type);
        }

        /// <summary>
        /// 获取日期常量表达式
        /// </summary>
        /// <param name="value">日期值</param>
        /// <param name="targetType">目标类型</param>
        public static SystemExpression CreateDateTimeExpression(object value, Type targetType)
        {
            var parse = typeof(DateTime).GetMethod("Parse", new[] { typeof(string) });
            if (parse == null)
                return null;
            var parseExpression = SystemExpression.Call(parse, SystemExpression.Constant(value.SafeString()));
            return SystemExpression.Convert(parseExpression, targetType);
        }
    }
}