using Meow.Extension;

namespace Meow.Domain.Compare;

/// <summary>
/// 键列表比较器
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
public class KeyListComparator<TKey> {
    /// <summary>
    /// 比较
    /// </summary>
    /// <param name="newList">新实体集合</param>
    /// <param name="originalList">旧实体集合</param>
    public KeyListCompareResult<TKey> Compare( IEnumerable<TKey> newList , IEnumerable<TKey> originalList ) {
        newList.CheckNull( nameof( newList ) );
        originalList.CheckNull( nameof( originalList ) );
        List<TKey> newEntities = newList.ToList();
        List<TKey> originalEntities = originalList.ToList();
        List<TKey> createList = GetCreateList( newEntities , originalEntities );
        List<TKey> updateList = GetUpdateList( newEntities , originalEntities );
        List<TKey> deleteList = GetDeleteList( newEntities , originalEntities );
        return new KeyListCompareResult<TKey>( createList , updateList , deleteList );
    }

    /// <summary>
    /// 获取创建列表
    /// </summary>
    private List<TKey> GetCreateList( List<TKey> newList , List<TKey> originalList ) {
        IEnumerable<TKey> result = newList.Except( originalList );
        return result.ToList();
    }

    /// <summary>
    /// 获取更新列表
    /// </summary>
    private List<TKey> GetUpdateList( List<TKey> newList , List<TKey> originalList ) {
        return newList.FindAll( id => originalList.Exists( t => t.Equals( id ) ) );
    }

    /// <summary>
    /// 获取删除列表
    /// </summary>
    private List<TKey> GetDeleteList( List<TKey> newList , List<TKey> originalList ) {
        IEnumerable<TKey> result = originalList.Except( newList );
        return result.ToList();
    }
}