using System.ComponentModel.DataAnnotations;

namespace Meow.Model;

/// <summary>
/// 标识
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
public class Key<TKey> : IKey<TKey>
{
    /// <summary>
    /// 初始化领域实体
    /// </summary>
    /// <param name="id">标识</param>
    public Key(TKey id)
    {
        Id = id;
    }

    /// <inheritdoc />
    [Key]
    public TKey Id { get; private set; }
}