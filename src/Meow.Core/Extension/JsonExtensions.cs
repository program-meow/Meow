using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Meow.Extension;

/// <summary>
/// JSON扩展
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// 将对象转换为Json字符串
    /// </summary>
    /// <param name="value">目标对象</param>
    /// <param name="options">Json配置</param>
    public static string ToJson<T>(this T value, Meow.Option.JsonOptions options)
    {
        return Meow.Helper.Json.ToJson<T>(value, options);
    }

    /// <summary>
    /// 将对象转换为Json字符串
    /// </summary>
    /// <param name="value">目标对象</param>
    /// <param name="options">序列化配置</param>
    /// <param name="removeQuotationMarks">是否移除双引号</param>
    /// <param name="toSingleQuotes">是否将双引号转成单引号</param>
    public static string ToJson<T>(this T value, JsonSerializerOptions options = null, bool removeQuotationMarks = false, bool toSingleQuotes = false)
    {
        return Meow.Helper.Json.ToJson<T>(value, options, removeQuotationMarks, toSingleQuotes);
    }

    /// <summary>
    /// 将对象转换为Json字符串
    /// </summary>
    /// <param name="value">目标对象</param>
    /// <param name="options">序列化配置</param>
    /// <param name="cancellationToken">取消令牌</param>
    public static async Task<string> ToJsonAsync<T>(this T value, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        return await Meow.Helper.Json.ToJsonAsync<T>(value, options, cancellationToken);
    }

    /// <summary>
    /// 将Json字符串转换为对象
    /// </summary>
    /// <param name="json">Json字符串</param>
    /// <param name="options">序列化配置</param>
    public static T ToJsonObject<T>(this string json, JsonSerializerOptions options = null)
    {
        return Meow.Helper.Json.ToObject<T>(json, options);
    }

    /// <summary>
    /// 将Json字符串转换为对象
    /// </summary>
    /// <param name="json">Json字符串</param>
    /// <param name="options">序列化配置</param>
    /// <param name="returnType">序列化配置</param>
    public static object ToJsonObject(this string json, System.Type returnType, JsonSerializerOptions options = null)
    {
        return Meow.Helper.Json.ToObject(json, returnType, options);
    }

    /// <summary>
    /// 将Json字符串转换为对象
    /// </summary>
    /// <param name="json">Json字符串</param>
    /// <param name="options">序列化配置</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="encoding">Json字符编码,默认UTF8</param>
    public static async Task<T> ToJsonObjectAsync<T>(this string json, JsonSerializerOptions options = null, CancellationToken cancellationToken = default, Encoding encoding = null)
    {
        return await Meow.Helper.Json.ToObjectAsync<T>(json, options, cancellationToken, encoding);
    }

    /// <summary>
    /// 将Json流转换为对象
    /// </summary>
    /// <param name="json">Json流</param>
    /// <param name="options">序列化配置</param>
    /// <param name="cancellationToken">取消令牌</param>
    public static async Task<T> ToJsonObjectAsync<T>(this System.IO.Stream json, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        return await Meow.Helper.Json.ToObjectAsync<T>(json, options, cancellationToken);
    }
}