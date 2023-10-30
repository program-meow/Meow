namespace Meow.Extension;

/// <summary>
/// 公共扩展
/// </summary>
public static class CommonExtensions {
    /// <summary>
    /// 安全转换为字符串，去除两端空格，当值为null时返回""
    /// </summary>
    /// <param name="value">值</param>
    public static string SafeString( this object value ) {
        return Meow.Helper.Common.SafeString( value );
    }

    /// <summary>
    /// 安全获取值，当值为null时，不会抛出异常
    /// </summary>
    /// <param name="value">可空值</param>
    public static string SafeValue( this string value ) {
        return Meow.Helper.Common.SafeValue( value );
    }

    /// <summary>
    /// 安全获取值，当值为null时，不会抛出异常
    /// </summary>
    /// <param name="array">集合</param>
    public static List<string> SafeValue( this IEnumerable<string> array ) {
        return Meow.Helper.Common.SafeValue( array );
    }

    /// <summary>
    /// 安全获取值，当值为null时，不会抛出异常
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="value">可空值</param>
    public static T SafeValue<T>( this T? value ) where T : struct {
        return Meow.Helper.Common.SafeValue( value );
    }

    /// <summary>
    /// 安全获取值，当值为null时，不会抛出异常
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="array">集合</param>
    public static List<T> SafeValue<T>( this IEnumerable<T?> array ) where T : struct {
        return Meow.Helper.Common.SafeValue( array );
    }

    /// <summary>
    /// 转换可空集合
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="array">集合</param>
    public static List<T?> ToOrNull<T>( this IEnumerable<T> array ) where T : struct {
        return Meow.Helper.Common.ToOrNull( array );
    }

    /// <summary>
    /// 安全获取值，当值为null时，不会抛出异常
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="value">可空值</param>
    public static T SafeValue<T>( this T value ) where T : new() {
        return Meow.Helper.Common.SafeValue( value );
    }

    /// <summary>
    /// 不为null及赋值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="target">目标值</param>
    /// <param name="value">空值</param>
    public static T AssignNotNull<T>( this T target , T value ) where T : class {
        return Meow.Helper.Common.AssignNotNull( target , value );
    }

    /// <summary>
    /// 克隆副本，解除引用类型
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="value">值</param>
    public static T Clone<T>( this T value ) {
        return Meow.Helper.Common.Clone( value );
    }
}