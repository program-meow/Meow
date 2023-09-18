namespace Meow.Model;

/// <summary>
/// 键值对
/// </summary>
public class KeyValue : KeyValue<string , string> {
    /// <summary>
    /// 初始化键值对
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public KeyValue( string key , string value ) : base( key , value ) {
    }
}

/// <summary>
/// 键值对
/// </summary>
/// <typeparam name="TKey">键元素类型</typeparam>
/// <typeparam name="TValue">值元素类型</typeparam>
public class KeyValue<TKey, TValue> {
    /// <summary>
    /// 初始化键值对
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public KeyValue( TKey key , TValue value ) {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// 标识
    /// </summary>
    public TKey Key { get; }
    /// <summary>
    /// 值
    /// </summary>
    public TValue Value { get; }
}