namespace Meow.Extension;

/// <summary>
/// 字典扩展
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// 将字典连接为带分隔符的字符串
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
    /// <param name="separator">分隔符，默认使用逗号分隔</param>
    public static string Join<TKey, TValue>(this IDictionary<TKey, TValue> array, string quotes = "", string separator = ",")
    {
        return Meow.Helper.Dictionary.Join(array, quotes, separator);
    }

    #region Remove  [移除null值]

    /// <summary>
    /// 移除null值
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    public static IDictionary<TKey, TValue> RemoveNull<TKey, TValue>(this IDictionary<TKey, TValue> array)
    {
        return Meow.Helper.Dictionary.RemoveNull(array);
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static IDictionary<TKey, string> RemoveEmpty<TKey>(this IDictionary<TKey, string> array)
    {
        return Meow.Helper.Dictionary.RemoveEmpty(array);
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static IDictionary<TKey, Guid> RemoveEmpty<TKey>(this IDictionary<TKey, Guid> array)
    {
        return Meow.Helper.Dictionary.RemoveEmpty(array);
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static IDictionary<TKey, Guid?> RemoveEmpty<TKey>(this IDictionary<TKey, Guid?> array)
    {
        return Meow.Helper.Dictionary.RemoveEmpty(array);
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static IDictionary<TKey, DateTime> RemoveEmpty<TKey>(this IDictionary<TKey, DateTime> array)
    {
        return Meow.Helper.Dictionary.RemoveEmpty(array);
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static IDictionary<TKey, DateTime?> RemoveEmpty<TKey>(this IDictionary<TKey, DateTime?> array)
    {
        return Meow.Helper.Dictionary.RemoveEmpty(array);
    }

    #endregion

    #region Add 和 AddRange 扩展

    /// <summary>
    /// 添加字典
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, TValue> Add<TKey, TValue>(this IDictionary<TKey, TValue> array, TKey key, TValue value, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.Add(array, key, value, isReplace);
    }

    /// <summary>
    /// 添加字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> array, IDictionary<TKey, TValue> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRange(array, values, isReplace);
    }

    /// <summary>
    /// 添加字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> array, IEnumerable<KeyValuePair<TKey, TValue>> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRange(array, values, isReplace);
    }

    /// <summary>
    /// 添加值不为null字典
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, TValue> AddNotNull<TKey, TValue>(this IDictionary<TKey, TValue> array, TKey key, TValue value, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddNotNull(array, key, value, isReplace);
    }

    /// <summary>
    /// 添加值不为null字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, TValue> AddRangeNotNull<TKey, TValue>(this IDictionary<TKey, TValue> array, IDictionary<TKey, TValue> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotNull(array, values, isReplace);
    }

    /// <summary>
    /// 添加值不为null字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <typeparam name="TValue">字典值元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, TValue> AddRangeNotNull<TKey, TValue>(this IDictionary<TKey, TValue> array, IEnumerable<KeyValuePair<TKey, TValue>> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotNull(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, string> AddNotEmpty<TKey>(this IDictionary<TKey, string> array, TKey key, string value, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddNotEmpty(array, key, value, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, string> AddRangeNotEmpty<TKey>(this IDictionary<TKey, string> array, IDictionary<TKey, string> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, string> AddRangeNotEmpty<TKey>(this IDictionary<TKey, string> array, IEnumerable<KeyValuePair<TKey, string>> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, Guid> AddNotEmpty<TKey>(this IDictionary<TKey, Guid> array, TKey key, Guid value, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddNotEmpty(array, key, value, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, Guid> AddRangeNotEmpty<TKey>(this IDictionary<TKey, Guid> array, IDictionary<TKey, Guid> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, Guid> AddRangeNotEmpty<TKey>(this IDictionary<TKey, Guid> array, IEnumerable<KeyValuePair<TKey, Guid>> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, Guid?> AddNotEmpty<TKey>(this IDictionary<TKey, Guid?> array, TKey key, Guid? value, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddNotEmpty(array, key, value, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, Guid?> AddRangeNotEmpty<TKey>(this IDictionary<TKey, Guid?> array, IDictionary<TKey, Guid?> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, Guid?> AddRangeNotEmpty<TKey>(this IDictionary<TKey, Guid?> array, IEnumerable<KeyValuePair<TKey, Guid?>> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, DateTime> AddNotEmpty<TKey>(this IDictionary<TKey, DateTime> array, TKey key, DateTime value, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddNotEmpty(array, key, value, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, DateTime> AddRangeNotEmpty<TKey>(this IDictionary<TKey, DateTime> array, IDictionary<TKey, DateTime> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, DateTime> AddRangeNotEmpty<TKey>(this IDictionary<TKey, DateTime> array, IEnumerable<KeyValuePair<TKey, DateTime>> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, DateTime?> AddNotEmpty<TKey>(this IDictionary<TKey, DateTime?> array, TKey key, DateTime? value, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddNotEmpty(array, key, value, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, DateTime?> AddRangeNotEmpty<TKey>(this IDictionary<TKey, DateTime?> array, IDictionary<TKey, DateTime?> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    /// <summary>
    /// 添加值有效字典集合
    /// </summary>
    /// <typeparam name="TKey">字典键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="values">值集合</param>
    /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
    public static IDictionary<TKey, DateTime?> AddRangeNotEmpty<TKey>(this IDictionary<TKey, DateTime?> array, IEnumerable<KeyValuePair<TKey, DateTime?>> values, bool isReplace = true)
    {
        return Meow.Helper.Dictionary.AddRangeNotEmpty(array, values, isReplace);
    }

    #endregion

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="source">字典数据</param>
    /// <param name="key">键</param>
    public static TValue GetValue<TKey, TValue>( this IDictionary<TKey , TValue> source , TKey key ) {
        if( source == null )
            return default;
        return source.TryGetValue( key , out var obj ) ? obj : default;
    }
}