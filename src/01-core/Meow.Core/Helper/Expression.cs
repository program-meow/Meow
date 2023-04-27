using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SystemExpression = System.Linq.Expressions.Expression;

namespace Meow.Helper
{
    /// <summary>
    /// 表达式操作
    /// </summary>
    public static class Expression
    {
        #region GetType  [获取类型]

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        public static System.Type GetType(SystemExpression expression)
        {
            var memberExpression = GetMemberExpression(expression);
            return memberExpression?.Type;
        }

        #endregion

        #region GetMember  [获取成员]

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        public static MemberInfo GetMember(SystemExpression expression)
        {
            MemberExpression memberExpression = GetMemberExpression(expression);
            return memberExpression?.Member;
        }

        /// <summary>
        /// 获取成员表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="right">取表达式右侧,(l,r) => l.id == r.id，设置为true,返回r.id表达式</param>
        public static MemberExpression GetMemberExpression(SystemExpression expression, bool right = false)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetMemberExpression(((LambdaExpression)expression).Body, right);
                case ExpressionType.Convert:
                case ExpressionType.Not:
                    return GetMemberExpression(((UnaryExpression)expression).Operand, right);
                case ExpressionType.MemberAccess:
                    return (MemberExpression)expression;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    return GetMemberExpression(right ? ((BinaryExpression)expression).Right : ((BinaryExpression)expression).Left, right);
                case ExpressionType.Call:
                    return GetMethodCallExpressionName(expression);
            }
            return null;
        }

        /// <summary>
        /// 获取方法调用表达式的成员名称
        /// </summary>
        private static MemberExpression GetMethodCallExpressionName(SystemExpression expression)
        {
            MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
            MemberExpression left = (MemberExpression)methodCallExpression.Object;
            if (Reflection.IsGenericCollection(left?.Type))
            {
                SystemExpression argumentExpression = methodCallExpression.Arguments.FirstOrDefault();
                if (argumentExpression != null && argumentExpression.NodeType == ExpressionType.MemberAccess)
                    return (MemberExpression)argumentExpression;
            }
            return left;
        }

        #endregion

        #region GetName  [获取成员名称，范例：t => t.A.Name,返回 A.Name]

        /// <summary>
        /// 获取成员名称，范例：t => t.A.Name,返回 A.Name
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        public static string GetName(SystemExpression expression)
        {
            MemberExpression memberExpression = GetMemberExpression(expression);
            return GetMemberName(memberExpression);
        }

        /// <summary>
        /// 获取成员名称
        /// </summary>
        public static string GetMemberName(MemberExpression memberExpression)
        {
            if (memberExpression == null)
                return string.Empty;
            string result = memberExpression.ToString();
            return result.Substring(result.IndexOf(".", StringComparison.Ordinal) + 1);
        }

        #endregion

        #region GetNames  [获取名称列表，范例：t => new object[] { t.A.B, t.C },返回A.B,C]

        /// <summary>
        /// 获取名称列表，范例：t => new object[] { t.A.B, t.C },返回A.B,C
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expression">属性集合表达式,范例：t => new object[]{t.A,t.B}</param>
        public static List<string> GetNames<T>(System.Linq.Expressions.Expression<Func<T, object[]>> expression)
        {
            List<string> result = new List<string>();
            if (expression == null)
                return result;
            if (!(expression.Body is NewArrayExpression arrayExpression))
                return result;
            foreach (SystemExpression each in arrayExpression.Expressions)
            {
                string name = GetName(each);
                if (Validation.IsEmpty(name) == false)
                    result.Add(name);
            }
            return result;
        }

        #endregion

        #region GetLastName  [获取最后一级成员名称，范例：t => t.A.Name,返回 Name]

        /// <summary>
        /// 获取最后一级成员名称，范例：t => t.A.Name,返回 Name
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        /// <param name="right">取表达式右侧,(l,r) => l.LId == r.RId，设置为true,返回RId</param>
        public static string GetLastName(SystemExpression expression, bool right = false)
        {
            MemberExpression memberExpression = GetMemberExpression(expression, right);
            if (memberExpression == null)
                return string.Empty;
            if (IsValueExpression(memberExpression))
                return string.Empty;
            string result = memberExpression.ToString();
            return result.Substring(result.LastIndexOf(".", StringComparison.Ordinal) + 1);
        }

        /// <summary>
        /// 是否值表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        private static bool IsValueExpression(SystemExpression expression)
        {
            if (expression == null)
                return false;
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return IsValueExpression(((MemberExpression)expression).Expression);
                case ExpressionType.Constant:
                    return true;
            }
            return false;
        }

        #endregion

        #region GetLastNames  [获取最后一级成员名称列表，范例：t => new object[] { t.A.B, t.C },返回B,C]

        /// <summary>
        /// 获取最后一级成员名称列表，范例：t => new object[] { t.A.B, t.C },返回B,C
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expression">属性集合表达式,范例：t => new object[]{t.A,t.B}</param>
        public static List<string> GetLastNames<T>(System.Linq.Expressions.Expression<Func<T, object[]>> expression)
        {
            List<string> result = new List<string>();
            if (expression == null)
                return result;
            if (!(expression.Body is NewArrayExpression arrayExpression))
                return result;
            foreach (SystemExpression each in arrayExpression.Expressions)
            {
                string name = GetLastName(each);
                if (string.IsNullOrWhiteSpace(name) == false)
                    result.Add(name);
            }
            return result;
        }

        #endregion

        #region GetValue  [获取值,范例：t => t.Name == "A",返回 A]

        /// <summary>
        /// 获取值,范例：t => t.Name == "A",返回 A
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name == "A"</param>
        public static object GetValue(SystemExpression expression)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetValue(((LambdaExpression)expression).Body);
                case ExpressionType.Convert:
                    return GetValue(((UnaryExpression)expression).Operand);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    bool hasParameter = HasParameter(((BinaryExpression)expression).Left);
                    if (hasParameter)
                        return GetValue(((BinaryExpression)expression).Right);
                    return GetValue(((BinaryExpression)expression).Left);
                case ExpressionType.Call:
                    return GetMethodCallExpressionValue(expression);
                case ExpressionType.MemberAccess:
                    return GetMemberValue((MemberExpression)expression);
                case ExpressionType.Constant:
                    return GetConstantExpressionValue(expression);
                case ExpressionType.Not:
                    if (expression.Type == typeof(bool))
                        return false;
                    return null;
            }
            return null;
        }

        /// <summary>
        /// 是否包含参数，用于检测是属性，而不是值
        /// </summary>
        private static bool HasParameter(SystemExpression expression)
        {
            if (expression == null)
                return false;
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    return HasParameter(((UnaryExpression)expression).Operand);
                case ExpressionType.MemberAccess:
                    return HasParameter(((MemberExpression)expression).Expression);
                case ExpressionType.Parameter:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取方法调用表达式的值
        /// </summary>
        private static object GetMethodCallExpressionValue(SystemExpression expression)
        {
            MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
            object value = GetValue(methodCallExpression.Arguments.FirstOrDefault());
            if (value != null)
                return value;
            if (methodCallExpression.Arguments.Count > 1)
                return GetValue(methodCallExpression.Arguments[1]);
            if (methodCallExpression.Object == null)
                return methodCallExpression.Type.InvokeMember(methodCallExpression.Method.Name, BindingFlags.InvokeMethod, null, null, null);
            return GetValue(methodCallExpression.Object);
        }

        /// <summary>
        /// 获取属性表达式的值
        /// </summary>
        private static object GetMemberValue(MemberExpression expression)
        {
            if (expression == null)
                return null;
            FieldInfo field = expression.Member as FieldInfo;
            if (field != null)
            {
                object constValue = GetConstantExpressionValue(expression.Expression);
                return field.GetValue(constValue);
            }
            PropertyInfo property = expression.Member as PropertyInfo;
            if (property == null)
                return null;
            if (expression.Expression == null)
                return property.GetValue(null);
            object value = GetMemberValue(expression.Expression as MemberExpression);
            if (value == null)
            {
                if (property.PropertyType == typeof(bool))
                    return true;
                return null;
            }
            return property.GetValue(value);
        }

        /// <summary>
        /// 获取常量表达式的值
        /// </summary>
        private static object GetConstantExpressionValue(SystemExpression expression)
        {
            ConstantExpression constantExpression = (ConstantExpression)expression;
            return constantExpression.Value;
        }

        #endregion

        #region GetOperator  [获取查询操作符,范例：t => t.Name == "A",返回 Operator.Equal]

        /// <summary>
        /// 获取查询操作符,范例：t => t.Name == "A",返回 Operator.Equal
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name == "A"</param>
        public static Meow.Math.Operator? GetOperator(SystemExpression expression)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetOperator(((LambdaExpression)expression).Body);
                case ExpressionType.Convert:
                    return GetOperator(((UnaryExpression)expression).Operand);
                case ExpressionType.Equal:
                    return Meow.Math.Operator.Equal;
                case ExpressionType.NotEqual:
                    return Meow.Math.Operator.NotEqual;
                case ExpressionType.GreaterThan:
                    return Meow.Math.Operator.Greater;
                case ExpressionType.LessThan:
                    return Meow.Math.Operator.Less;
                case ExpressionType.GreaterThanOrEqual:
                    return Meow.Math.Operator.GreaterEqual;
                case ExpressionType.LessThanOrEqual:
                    return Meow.Math.Operator.LessEqual;
                case ExpressionType.Call:
                    return GetMethodCallExpressionOperator(expression);
            }
            return null;
        }

        /// <summary>
        /// 获取方法调用表达式的值
        /// </summary>
        private static Meow.Math.Operator? GetMethodCallExpressionOperator(SystemExpression expression)
        {
            var methodCallExpression = (MethodCallExpression)expression;
            switch (methodCallExpression?.Method?.Name?.ToLower())
            {
                case "contains":
                    return Meow.Math.Operator.Contains;
                case "endswith":
                    return Meow.Math.Operator.Ends;
                case "startswith":
                    return Meow.Math.Operator.Starts;
            }
            return null;
        }

        #endregion

        #region GetParameter  [获取参数，范例：t.Name,返回 t]

        /// <summary>
        /// 获取参数，范例：t.Name,返回 t
        /// </summary>
        /// <param name="expression">表达式，范例：t.Name</param>
        public static ParameterExpression GetParameter(SystemExpression expression)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetParameter(((LambdaExpression)expression).Body);
                case ExpressionType.Convert:
                    return GetParameter(((UnaryExpression)expression).Operand);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    return GetParameter(((BinaryExpression)expression).Left);
                case ExpressionType.MemberAccess:
                    return GetParameter(((MemberExpression)expression).Expression);
                case ExpressionType.Call:
                    return GetParameter(((MethodCallExpression)expression).Object);
                case ExpressionType.Parameter:
                    return (ParameterExpression)expression;
            }
            return null;
        }

        #endregion

        #region GetGroupPredicates  [获取分组的谓词表达式，通过Or进行分组]

        /// <summary>
        /// 获取分组的谓词表达式，通过Or进行分组
        /// </summary>
        /// <param name="expression">谓词表达式</param>
        public static List<List<SystemExpression>> GetGroupPredicates(SystemExpression expression)
        {
            List<List<SystemExpression>> result = new List<List<SystemExpression>>();
            if (expression == null)
                return result;
            AddPredicates(expression, result, CreateGroup(result));
            return result;
        }

        /// <summary>
        /// 创建分组
        /// </summary>
        private static List<SystemExpression> CreateGroup(List<List<SystemExpression>> result)
        {
            List<SystemExpression> group = new List<SystemExpression>();
            result.Add(group);
            return group;
        }

        /// <summary>
        /// 添加通过Or分割的谓词表达式
        /// </summary>
        private static void AddPredicates(SystemExpression expression, List<List<SystemExpression>> result, List<SystemExpression> group)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    AddPredicates(((LambdaExpression)expression).Body, result, group);
                    break;
                case ExpressionType.OrElse:
                    AddPredicates(((BinaryExpression)expression).Left, result, group);
                    AddPredicates(((BinaryExpression)expression).Right, result, CreateGroup(result));
                    break;
                case ExpressionType.AndAlso:
                    AddPredicates(((BinaryExpression)expression).Left, result, group);
                    AddPredicates(((BinaryExpression)expression).Right, result, group);
                    break;
                default:
                    group.Add(expression);
                    break;
            }
        }

        #endregion

        #region GetConditionCount  [获取查询条件个数]

        /// <summary>
        /// 获取查询条件个数
        /// </summary>
        /// <param name="expression">谓词表达式,范例1：t => t.Name == "A" ，结果1。
        /// 范例2：t => t.Name == "A" &amp;&amp; t.Age =1 ，结果2。</param>
        public static int GetConditionCount(LambdaExpression expression)
        {
            if (expression == null)
                return 0;
            string result = expression.ToString().Replace("AndAlso", "|").Replace("OrElse", "|");
            return result.Split('|').Count();
        }

        #endregion

        #region GetAttribute  [获取特性]

        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        public static TAttribute GetAttribute<TAttribute>(SystemExpression expression) where TAttribute : Attribute
        {
            MemberInfo memberInfo = GetMember(expression);
            return memberInfo.GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        public static TAttribute GetAttribute<TEntity, TProperty, TAttribute>(System.Linq.Expressions.Expression<Func<TEntity, TProperty>> propertyExpression) where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(propertyExpression);
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        public static TAttribute GetAttribute<TProperty, TAttribute>(System.Linq.Expressions.Expression<Func<TProperty>> propertyExpression) where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(propertyExpression);
        }

        #endregion

        #region GetAttributes  [获取特性列表]

        /// <summary>
        /// 获取特性列表
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        public static IEnumerable<TAttribute> GetAttributes<TEntity, TProperty, TAttribute>(System.Linq.Expressions.Expression<Func<TEntity, TProperty>> propertyExpression) where TAttribute : Attribute
        {
            MemberInfo memberInfo = GetMember(propertyExpression);
            return memberInfo.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Constant  [获取常量表达式]

        /// <summary>
        /// 获取常量表达式
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="expression">表达式</param>
        public static ConstantExpression Constant(object value, SystemExpression expression = null)
        {
            System.Type type = GetType(expression);
            if (type == null)
                return SystemExpression.Constant(value);
            return SystemExpression.Constant(value, type);
        }

        #endregion

        #region CreateParameter  [创建参数表达式]

        /// <summary>
        /// 创建参数表达式
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        public static ParameterExpression CreateParameter<T>()
        {
            return SystemExpression.Parameter(typeof(T), "t");
        }

        #endregion

        #region Equal  [创建等于运算lambda表达式]

        /// <summary>
        /// 创建等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> Equal<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = Equal(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region NotEqual  [不等于表达式]

        /// <summary>
        /// 创建不等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> NotEqual<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = NotEqual(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region Greater  [大于表达式]

        /// <summary>
        /// 创建大于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> Greater<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = Greater(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region GreaterEqual  [大于等于表达式]

        /// <summary>
        /// 创建大于等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> GreaterEqual<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = GreaterEqual(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region Less  [小于表达式]

        /// <summary>
        /// 创建小于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> Less<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = Less(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region LessEqual  [小于等于表达式]

        /// <summary>
        /// 创建小于等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> LessEqual<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = LessEqual(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region Starts  [调用StartsWith方法]

        /// <summary>
        /// 调用StartsWith方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> Starts<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = StartsWith(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region Ends  [调用EndsWith方法]

        /// <summary>
        /// 调用EndsWith方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> Ends<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = EndsWith(left, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region Contains  [调用Contains方法]

        /// <summary>
        /// 调用Contains方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> Contains<T>(string propertyName, object value)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression expression = Property(parameter, propertyName);
            SystemExpression body = Contains(expression, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region ParsePredicate  [解析为谓词表达式]

        /// <summary>
        /// 解析为谓词表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="operator">运算符</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> ParsePredicate<T>(string propertyName, object value, Meow.Math.Operator @operator)
        {
            ParameterExpression parameter = CreateParameter<T>();
            SystemExpression left = Property(parameter, propertyName);
            SystemExpression body = Operation(left, @operator, value);
            return ToPredicate<T>(body, parameter);
        }

        #endregion

        #region Property  [属性表达式]

        /// <summary>
        /// 创建属性表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="propertyName">属性名,支持多级属性名，用句点分隔，范例：Customer.Name</param>
        public static SystemExpression Property(SystemExpression expression, string propertyName)
        {
            if (propertyName.All(t => t != '.'))
                return SystemExpression.Property(expression, propertyName);
            string[] propertyNameList = propertyName.Split('.');
            SystemExpression result = null;
            for (int i = 0; i < propertyNameList.Length; i++)
            {
                if (i == 0)
                {
                    result = SystemExpression.Property(expression, propertyNameList[0]);
                    continue;
                }
                result = Property(result, propertyNameList[i]);
            }
            return result;
        }

        /// <summary>
        /// 创建属性表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="member">属性</param>
        public static SystemExpression Property(SystemExpression expression, MemberInfo member)
        {
            return SystemExpression.MakeMemberAccess(expression, member);
        }

        #endregion

        #region And  [与表达式]

        /// <summary>
        /// 与操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression And(SystemExpression left, SystemExpression right)
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
        public static System.Linq.Expressions.Expression<Func<T, bool>> And<T>(System.Linq.Expressions.Expression<Func<T, bool>> left, System.Linq.Expressions.Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return Compose(left, right, SystemExpression.AndAlso);
        }

        #endregion

        #region Or(或表达式)

        /// <summary>
        /// 或操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Or(SystemExpression left, SystemExpression right)
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
        public static System.Linq.Expressions.Expression<Func<T, bool>> Or<T>(System.Linq.Expressions.Expression<Func<T, bool>> left, System.Linq.Expressions.Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            return Compose(left, right, SystemExpression.OrElse);
        }

        #endregion

        #region Value  [获取lambda表达式的值]

        /// <summary>
        /// 获取lambda表达式的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public static object Value<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return GetValue(expression);
        }

        #endregion

        #region Equal  [等于表达式]

        /// <summary>
        /// 创建等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Equal(SystemExpression left, SystemExpression right)
        {
            return SystemExpression.Equal(left, right);
        }

        /// <summary>
        /// 创建等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Equal(SystemExpression left, object value)
        {
            ConstantExpression right = Constant(value, left);
            return Equal(left, right);
        }

        #endregion

        #region NotEqual  [不等于表达式]

        /// <summary>
        /// 创建不等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression NotEqual(SystemExpression left, SystemExpression right)
        {
            return SystemExpression.NotEqual(left, right);
        }

        /// <summary>
        /// 创建不等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression NotEqual(SystemExpression left, object value)
        {
            ConstantExpression right = Constant(value, left);
            return NotEqual(left, right);
        }

        #endregion

        #region Greater(大于表达式)

        /// <summary>
        /// 创建大于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Greater(SystemExpression left, SystemExpression right)
        {
            return SystemExpression.GreaterThan(left, right);
        }

        /// <summary>
        /// 创建大于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Greater(SystemExpression left, object value)
        {
            ConstantExpression right = Constant(value, left);
            return Greater(left, right);
        }

        #endregion

        #region GreaterEqual  [大于等于表达式]

        /// <summary>
        /// 创建大于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression GreaterEqual(SystemExpression left, SystemExpression right)
        {
            return SystemExpression.GreaterThanOrEqual(left, right);
        }

        /// <summary>
        /// 创建大于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression GreaterEqual(SystemExpression left, object value)
        {
            ConstantExpression right = Constant(value, left);
            return GreaterEqual(left, right);
        }

        #endregion

        #region Less  [小于表达式]

        /// <summary>
        /// 创建小于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression Less(SystemExpression left, SystemExpression right)
        {
            return SystemExpression.LessThan(left, right);
        }

        /// <summary>
        /// 创建小于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Less(SystemExpression left, object value)
        {
            ConstantExpression right = Constant(value, left);
            return Less(left, right);
        }

        #endregion

        #region LessEqual  [小于等于表达式]

        /// <summary>
        /// 创建小于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static SystemExpression LessEqual(SystemExpression left, SystemExpression right)
        {
            return SystemExpression.LessThanOrEqual(left, right);
        }

        /// <summary>
        /// 创建小于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression LessEqual(SystemExpression left, object value)
        {
            ConstantExpression right = Constant(value, left);
            return LessEqual(left, right);
        }

        #endregion

        #region StartsWith  [头匹配]

        /// <summary>
        /// 头匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression StartsWith(SystemExpression left, object value)
        {
            return Call(left, "StartsWith", new[] { typeof(string) }, value);
        }

        #endregion

        #region EndsWith  [尾匹配]

        /// <summary>
        /// 尾匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression EndsWith(SystemExpression left, object value)
        {
            return Call(left, "EndsWith", new[] { typeof(string) }, value);
        }

        #endregion

        #region Contains  [模糊匹配]

        /// <summary>
        /// 模糊匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static SystemExpression Contains(SystemExpression left, object value)
        {
            return Call(left, "Contains", new[] { typeof(string) }, value);
        }

        #endregion

        #region Operation  [操作]

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="operator">运算符</param>
        /// <param name="value">值</param>
        public static SystemExpression Operation(SystemExpression left, Meow.Math.Operator @operator, object value)
        {
            switch (@operator)
            {
                case Meow.Math.Operator.Equal:
                    return Equal(left, value);
                case Meow.Math.Operator.NotEqual:
                    return NotEqual(left, value);
                case Meow.Math.Operator.Greater:
                    return Greater(left, value);
                case Meow.Math.Operator.GreaterEqual:
                    return GreaterEqual(left, value);
                case Meow.Math.Operator.Less:
                    return Less(left, value);
                case Meow.Math.Operator.LessEqual:
                    return LessEqual(left, value);
                case Meow.Math.Operator.Starts:
                    return StartsWith(left, value);
                case Meow.Math.Operator.Ends:
                    return EndsWith(left, value);
                case Meow.Math.Operator.Contains:
                    return Contains(left, value);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="operator">运算符</param>
        /// <param name="value">值</param>
        public static SystemExpression Operation(SystemExpression left, Meow.Math.Operator @operator, SystemExpression value)
        {
            switch (@operator)
            {
                case Meow.Math.Operator.Equal:
                    return Equal(left, value);
                case Meow.Math.Operator.NotEqual:
                    return NotEqual(left, value);
                case Meow.Math.Operator.Greater:
                    return Greater(left, value);
                case Meow.Math.Operator.GreaterEqual:
                    return GreaterEqual(left, value);
                case Meow.Math.Operator.Less:
                    return Less(left, value);
                case Meow.Math.Operator.LessEqual:
                    return LessEqual(left, value);
            }
            throw new NotImplementedException();
        }

        #endregion

        #region Call  [调用方法表达式]

        /// <summary>
        /// 创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="values">参数值列表</param>
        public static SystemExpression Call(SystemExpression instance, string methodName, params SystemExpression[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            MethodInfo methodInfo = instance.Type.GetMethod(methodName);
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
        public static SystemExpression Call(SystemExpression instance, string methodName, params object[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            MethodInfo methodInfo = instance.Type.GetMethod(methodName);
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
        public static SystemExpression Call(SystemExpression instance, string methodName, System.Type[] paramTypes, params object[] values)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            MethodInfo methodInfo = instance.Type.GetMethod(methodName, paramTypes);
            if (methodInfo == null)
                return null;
            if (values == null || values.Length == 0)
                return SystemExpression.Call(instance, methodInfo);
            return SystemExpression.Call(instance, methodInfo, values.Select(SystemExpression.Constant));
        }

        #endregion

        #region Compose  [组合表达式]

        /// <summary>
        /// 组合表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="first">左操作数</param>
        /// <param name="second">右操作数</param>
        /// <param name="merge">合并操作</param>
        internal static System.Linq.Expressions.Expression<T> Compose<T>(
            System.Linq.Expressions.Expression<T> first
            , System.Linq.Expressions.Expression<T> second
            , Func<
                SystemExpression
                , SystemExpression
                , SystemExpression> merge)
        {
            Dictionary<ParameterExpression, ParameterExpression> map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            SystemExpression secondBody = Meow.Expression.ParameterRebind.ReplaceParameters(map, second.Body);
            return SystemExpression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        #endregion

        #region ToLambda  [创建Lambda表达式]

        /// <summary>
        /// 创建Lambda表达式
        /// </summary>
        /// <typeparam name="TDelegate">委托类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数列表</param>
        public static System.Linq.Expressions.Expression<TDelegate> ToLambda<TDelegate>(SystemExpression body, params ParameterExpression[] parameters)
        {
            if (body == null)
                return null;
            return SystemExpression.Lambda<TDelegate>(body, parameters);
        }

        #endregion

        #region ToPredicate  [创建谓词表达式]

        /// <summary>
        /// 创建谓词表达式
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数列表</param>
        public static System.Linq.Expressions.Expression<Func<T, bool>> ToPredicate<T>(SystemExpression body, params ParameterExpression[] parameters)
        {
            return ToLambda<Func<T, bool>>(body, parameters);
        }

        #endregion
    }
}
