namespace Meow.Helper;

/// <summary>
/// 数组操作
/// </summary>
public static class Array
{
    /// <summary>
    /// 空数组
    /// </summary>
    /// <typeparam name="T">空值元素类型</typeparam>
    public static T[] Empty<T>() => System.Array.Empty<T>();

}