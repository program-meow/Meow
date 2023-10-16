namespace Meow.Model;

/// <summary>
/// 标识、名
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
public class IdName<TKey> : Key<TKey> {
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="name">名称</param>
    public IdName( TKey id , string name ) : base( id ) {
        Name = name;
    }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}

/// <summary>
/// 标识、名、描述
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class IdNameDescription<TKey> : IdName<TKey> {
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="name">名称</param>
    /// <param name="description">描述</param>
    public IdNameDescription( TKey id , string name , string description ) : base( id , name ) {
        Description = description;
    }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

}

/// <summary>
/// 标识、名
/// </summary>
public class IdName : IdName<Guid> {
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="name">名称</param>
    protected IdName( Guid id , string name ) : base( id , name ) {
    }
}

/// <summary>
/// 标识、名、描述
/// </summary>
public class IdNameDescription : IdNameDescription<Guid> {
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="name">名称</param>
    /// <param name="description">描述</param>
    protected IdNameDescription( Guid id , string name , string description ) : base( id , name , description ) {
    }
}