using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meow.Data.Extension;
using Meow.Extension;
using Meow.Query;
using Microsoft.EntityFrameworkCore;

namespace Meow.Data.EntityFramework.Extension;

/// <summary>
/// 查询对象扩展
/// </summary>
public static class QueryableExtensions
{
    #region ToPageList  [获取分页列表]

    /// <summary>
    /// 获取分页列表
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="source">源</param>
    /// <param name="parameter">分页参数</param>
    public static PageList<TEntity> ToPageList<TEntity>(this IQueryable<TEntity> source, IPage parameter) where TEntity : class
    {
        source.CheckNull(nameof(source));
        parameter.CheckNull(nameof(parameter));
        if (parameter.Total <= 0)
            parameter.Total = source.Count();
        List<TEntity> list = source.OrderBy(parameter, "Id").Skip(parameter.GetSkipCount()).Take(parameter.PageSize).ToList();
        return new PageList<TEntity>(parameter, list);
    }

    #endregion

    #region ToPageListAsync  [获取分页列表]

    /// <summary>
    /// 获取分页列表
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="source">源</param>
    /// <param name="parameter">分页参数</param>
    public static async Task<PageList<TEntity>> ToPageListAsync<TEntity>(this IQueryable<TEntity> source, IPage parameter) where TEntity : class
    {
        source.CheckNull(nameof(source));
        parameter.CheckNull(nameof(parameter));
        if (parameter.Total <= 0)
            parameter.Total = await source.CountAsync();
        List<TEntity> list = await source.OrderBy(parameter, "Id").Skip(parameter.GetSkipCount()).Take(parameter.PageSize).ToListAsync();
        return new PageList<TEntity>(parameter, list);
    }

    #endregion
}