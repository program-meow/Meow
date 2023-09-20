namespace Meow.Extension;

/// <summary>
/// 集合扩展
/// </summary>
public static class ListExtensions {
    /// <summary>
    /// 驼峰式命
    /// </summary>
    /// <param name="array">集合</param>
    /// <returns>驼峰形式字符串</returns>
    public static string CamelCase( this IEnumerable<string> array ) {
        return Meow.Helper.List.CamelCase( array );
    }

    /// <summary>
    /// 将集合连接为带分隔符的字符串
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
    /// <param name="separator">分隔符，默认使用逗号分隔</param>
    public static string Join<T>( this IEnumerable<T> array , string quotes = "" , string separator = "," ) {
        return Meow.Helper.List.Join( array , quotes , separator );
    }

    /// <summary>
    /// 转换为小写字符串集合
    /// </summary>
    /// <param name="array">集合</param>
    /// <returns>小写字符串集合</returns>
    public static List<string> ToLower( this IEnumerable<string> array ) {
        return Meow.Helper.List.ToLower( array );
    }

    /// <summary>
    /// 转换为大写字符串集合
    /// </summary>
    /// <param name="array">集合</param>
    /// <returns>大写字符串集合</returns>
    public static List<string> ToUpper( this IEnumerable<string> array ) {
        return Meow.Helper.List.ToUpper( array );
    }

    #region Add 和 AddRange 扩展

    /// <summary>
    /// 添加不为null的值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<T> AddNotNull<T>( this List<T> array , T value ) {
        return Meow.Helper.List.AddNotNull( array , value );
    }

    /// <summary>
    /// 添加不为null的值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<T> AddRangeNotNull<T>( this List<T> array , List<T> value ) {
        return Meow.Helper.List.AddRangeNotNull( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<string> AddNotEmpty( this List<string> array , string value ) {
        return Meow.Helper.List.AddNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<string> AddRangeNotEmpty( this List<string> array , List<string> value ) {
        return Meow.Helper.List.AddRangeNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<Guid> AddNotEmpty( this List<Guid> array , Guid value ) {
        return Meow.Helper.List.AddNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<Guid> AddRangeNotEmpty( this List<Guid> array , List<Guid> value ) {
        return Meow.Helper.List.AddRangeNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<Guid?> AddNotEmpty( this List<Guid?> array , Guid? value ) {
        return Meow.Helper.List.AddNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<Guid?> AddRangeNotEmpty( this List<Guid?> array , List<Guid?> value ) {
        return Meow.Helper.List.AddRangeNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<DateTime> AddNotEmpty( this List<DateTime> array , DateTime value ) {
        return Meow.Helper.List.AddNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<DateTime> AddRangeNotEmpty( this List<DateTime> array , List<DateTime> value ) {
        return Meow.Helper.List.AddRangeNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<DateTime?> AddNotEmpty( this List<DateTime?> array , DateTime? value ) {
        return Meow.Helper.List.AddNotEmpty( array , value );
    }

    /// <summary>
    /// 添加有效值
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="value">值</param>
    public static List<DateTime?> AddRangeNotEmpty( this List<DateTime?> array , List<DateTime?> value ) {
        return Meow.Helper.List.AddRangeNotEmpty( array , value );
    }

    #endregion

    #region IsSequence  [是否不间断连续 判断类型是Int]

    /// <summary>
    /// 是否不间断连续 判断类型是Int
    /// </summary>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <typeparam name="TKey">键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    /// <param name="startNo">起始数</param>
    public static bool IsSequenceBy<TSource, TKey>( this IEnumerable<TSource> array , Func<TSource , TKey> keySelector , int startNo = 1 ) {
        return Meow.Helper.List.IsSequenceBy( array , keySelector , startNo );
    }

    /// <summary>
    /// 是否不间断连续
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="startNo">起始数</param>
    public static bool IsSequence( this IEnumerable<int?> array , int startNo = 1 ) {
        return Meow.Helper.List.IsSequence( array , startNo );
    }

    /// <summary>
    /// 是否不间断连续
    /// </summary>
    /// <param name="array">集合</param>
    /// <param name="startNo">起始数</param>
    public static bool IsSequence( this IEnumerable<int> array , int startNo = 1 ) {
        return Meow.Helper.List.IsSequence( array , startNo );
    }

    /// <summary>
    /// 值是否相等
    /// </summary>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <typeparam name="TKey">键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static bool IsEqualsBy<TSource, TKey>( this IEnumerable<TSource> array , Func<TSource , TKey> keySelector ) {
        return Meow.Helper.List.IsEqualsBy( array , keySelector );
    }

    #endregion

    /// <summary>
    /// 值是否相等
    /// </summary>
    /// <param name="array">集合</param>
    public static bool IsEquals<T>( this IEnumerable<T> array ) {
        return Meow.Helper.List.IsEquals( array );
    }

    #region Remove  扩展

    /// <summary>
    /// 移除null值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    public static List<T> RemoveNull<T>( this List<T> array ) {
        return Meow.Helper.List.RemoveNull( array );
    }

    /// <summary>
    /// 根据条件移除null值
    /// </summary>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <typeparam name="TKey">键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<TSource> RemoveNullBy<TSource, TKey>( this List<TSource> array , Func<TSource , TKey> keySelector ) {
        return Meow.Helper.List.RemoveNullBy( array , keySelector );
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static List<string> RemoveEmpty( this List<string> array ) {
        return Meow.Helper.List.RemoveEmpty( array );
    }

    /// <summary>
    /// 根据条件移除空值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<T> RemoveEmptyBy<T>( this List<T> array , Func<T , string> keySelector ) {
        return Meow.Helper.List.RemoveEmptyBy( array , keySelector );
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static List<Guid> RemoveEmpty( this List<Guid> array ) {
        return Meow.Helper.List.RemoveEmpty( array );
    }

    /// <summary>
    /// 根据条件移除空值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<T> RemoveEmptyBy<T>( this List<T> array , Func<T , Guid> keySelector ) {
        return Meow.Helper.List.RemoveEmptyBy( array , keySelector );
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static List<Guid?> RemoveEmpty( this List<Guid?> array ) {
        return Meow.Helper.List.RemoveEmpty( array );
    }

    /// <summary>
    /// 根据条件移除空值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<T> RemoveEmptyBy<T>( this List<T> array , Func<T , Guid?> keySelector ) {
        return Meow.Helper.List.RemoveEmptyBy( array , keySelector );
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static List<DateTime> RemoveEmpty( this List<DateTime> array ) {
        return Meow.Helper.List.RemoveEmpty( array );
    }

    /// <summary>
    /// 根据条件移除空值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<T> RemoveEmptyBy<T>( this List<T> array , Func<T , DateTime> keySelector ) {
        return Meow.Helper.List.RemoveEmptyBy( array , keySelector );
    }

    /// <summary>
    /// 移除空值
    /// </summary>
    /// <param name="array">集合</param>
    public static List<DateTime?> RemoveEmpty( this List<DateTime?> array ) {
        return Meow.Helper.List.RemoveEmpty( array );
    }

    /// <summary>
    /// 根据条件移除空值
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<T> RemoveEmptyBy<T>( this List<T> array , Func<T , DateTime?> keySelector ) {
        return Meow.Helper.List.RemoveEmptyBy( array , keySelector );
    }

    #endregion
}