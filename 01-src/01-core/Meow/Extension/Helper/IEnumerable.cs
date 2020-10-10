using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Meow.Mathematics.Enum;
using Meow.Query.Core.Criteria;
using Meow.Query.Pager;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 集合类型扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            if (value.IsNull())
                return true;
            return !value.Any();
        }

        /// <summary>
        /// 转换为用分隔符连接的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>(this IEnumerable<T> value, string quotes = "", string separator = ",")
        {
            return Meow.Helper.IEnumerable.Join(value, quotes, separator);
        }

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<T> SafeValue<T>(this IEnumerable<T> value)
        {
            return value == null
                 ? new List<T>()
                 : value.ToList();
        }

        /// <summary>
        /// 转换为不可空值，当项值为null时，则排除
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<T> ToNotNull<T>(this IEnumerable<T?> value) where T : struct
        {
            if (value == null)
                return new List<T>();
            return value
                  .Where(t => t != null)
                  .Select(t => t.SafeValue())
                  .ToList();
        }

        /// <summary>
        /// 转换为可空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<T?> ToOrNull<T>(this IEnumerable<T> value) where T : struct
        {
            if (value == null)
                return new List<T?>();
            return value.Select(item => (T?)item).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<T> FilterNull<T>(this IEnumerable<T> value)
        {
            return value == null
                 ? new List<T>()
                 : value.Where(t => t != null).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <param name="value">值</param>
        public static List<string> FilterEmpty(this IEnumerable<string> value)
        {
            return value == null
                 ? new List<string>()
                 : value.Where(t => !t.IsEmpty()).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <param name="value">值</param>
        public static List<Guid> FilterEmpty(this IEnumerable<Guid> value)
        {
            return value == null
                 ? new List<Guid>()
                 : value.Where(t => !t.IsEmpty()).ToList();
        }

        /// <summary>
        /// 过滤空值
        /// </summary>
        /// <param name="value">值</param>
        public static List<Guid?> FilterEmpty(this IEnumerable<Guid?> value)
        {
            return value == null
                 ? new List<Guid?>()
                 : value.Where(t => !t.IsEmpty()).ToList();
        }

        /// <summary>
        /// 过滤重复值
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <typeparam name="TKey">标识类型</typeparam>
        /// <param name="source">源数据</param>
        /// <param name="keySelector">委托方法</param>
        public static List<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var result = source.DistinctByFactor(keySelector);
            return result.ToList();
        }

        /// <summary>
        /// 根据条件过滤重复值
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <typeparam name="TKey">标识类型</typeparam>
        /// <param name="source">源数据</param>
        /// <param name="keySelector">委托方法</param>
        private static IEnumerable<TSource> DistinctByFactor<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="condition">该值为true时添加查询条件，否则忽略</param>
        public static List<TEntity> WhereIf<TEntity>(this IEnumerable<TEntity> source, Expression<Func<TEntity, bool>> predicate, bool condition)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (condition == false)
                return source.ToList();
            return source.AsQueryable().Where(predicate).ToList();
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">查询条件,如果参数值为空，则忽略该查询条件，范例：t => t.Name == ""，该查询条件被忽略。
        /// 注意：一次仅能添加一个条件，范例：t => t.Name == "a" &amp;&amp; t.Mobile == "123"，不支持，将抛出异常</param>
        public static List<TEntity> WhereIfNotEmpty<TEntity>(this IEnumerable<TEntity> source, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var queryable = source.AsQueryable();
            predicate = Meow.Query.Core.Helper.CommonHelper.GetWhereIfNotEmptyExpression(predicate);
            if (predicate == null)
                return queryable.ToList();
            return queryable.Where(predicate).ToList();
        }

        /// <summary>
        /// 添加范围查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public static List<TEntity> Between<TEntity, TProperty>(this IEnumerable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max, Boundary boundary = Boundary.Both)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var queryable = source.AsQueryable();
            return queryable.Where(new IntSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max, boundary)).ToList();
        }

        /// <summary>
        /// 添加范围查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public static List<TEntity> Between<TEntity, TProperty>(this IEnumerable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max, Boundary boundary = Boundary.Both)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var queryable = source.AsQueryable();
            return queryable.Where(new DoubleSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max, boundary)).ToList();
        }

        /// <summary>
        /// 添加范围查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public static List<TEntity> Between<TEntity, TProperty>(this IEnumerable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max, Boundary boundary = Boundary.Both)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var queryable = source.AsQueryable();
            return queryable.Where(new DecimalSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max, boundary)).ToList();
        }

        /// <summary>
        /// 添加范围查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Time</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="includeTime">是否包含时间</param>
        /// <param name="boundary">包含边界</param>
        public static List<TEntity> Between<TEntity, TProperty>(this IEnumerable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max, bool includeTime = true, Boundary? boundary = null)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var queryable = source.AsQueryable();
            if (includeTime)
                return queryable.Where(new DateTimeSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max, boundary ?? Boundary.Both)).ToList();
            return queryable.Where(new DateSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max, boundary ?? Boundary.Left)).ToList();
        }

        /// <summary>
        /// 分页，包含排序
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pager">分页对象</param>
        public static List<TEntity> Page<TEntity>(this IEnumerable<TEntity> source, IPager pager)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var queryable = source.AsQueryable();
            if (pager == null)
                throw new ArgumentNullException(nameof(pager));
            if (pager.TotalCount <= 0)
                pager.TotalCount = queryable.Count();
            return queryable.Skip(pager.GetSkipCount()).Take(pager.PageSize).ToList();
        }

        /// <summary>
        /// 转换为分页列表，包含排序分页操作
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pager">分页对象</param>
        public static PagerList<TEntity> ToPagerList<TEntity>(this IEnumerable<TEntity> source, IPager pager)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (pager == null)
                throw new ArgumentNullException(nameof(pager));
            return new PagerList<TEntity>(pager, source.Page(pager).ToList());
        }
    }
}