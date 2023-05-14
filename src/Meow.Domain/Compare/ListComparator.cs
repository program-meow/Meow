using System.Collections.Generic;
using System.Linq;
using Meow.Extension;
using Meow.Model;

namespace Meow.Domain.Compare;

/// <summary>
/// 实体列表比较器
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">标识类型</typeparam>
public class ListComparator<TEntity, TKey> where TEntity : IKey<TKey>
{
    /// <summary>
    /// 比较
    /// </summary>
    /// <param name="newList">新实体集合</param>
    /// <param name="originalList">旧实体集合</param>
    public ListCompareResult<TEntity, TKey> Compare(IEnumerable<TEntity> newList, IEnumerable<TEntity> originalList)
    {
        newList.CheckNull(nameof(newList));
        originalList.CheckNull(nameof(originalList));
        List<TEntity> newEntities = newList.ToList();
        List<TEntity> originalEntities = originalList.ToList();
        List<TEntity> createList = GetCreateList(newEntities, originalEntities);
        List<TEntity> updateList = GetUpdateList(newEntities, originalEntities);
        List<TEntity> deleteList = GetDeleteList(newEntities, originalEntities);
        return new ListCompareResult<TEntity, TKey>(createList, updateList, deleteList);
    }

    /// <summary>
    /// 获取创建列表
    /// </summary>
    private List<TEntity> GetCreateList(List<TEntity> newList, List<TEntity> originalList)
    {
        IEnumerable<TEntity> result = newList.Except(originalList);
        return result.ToList();
    }

    /// <summary>
    /// 获取更新列表
    /// </summary>
    private List<TEntity> GetUpdateList(List<TEntity> newList, List<TEntity> originalList)
    {
        return newList.FindAll(entity => originalList.Exists(t => t.Id.Equals(entity.Id)));
    }

    /// <summary>
    /// 获取删除列表
    /// </summary>
    private List<TEntity> GetDeleteList(List<TEntity> newList, List<TEntity> originalList)
    {
        IEnumerable<TEntity> result = originalList.Except(newList);
        return result.ToList();
    }
}