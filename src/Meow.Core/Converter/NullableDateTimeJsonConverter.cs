using Meow.Extension;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SystemType = System.Type;

namespace Meow.Converter;

/// <summary>
/// 可空日期格式Json转换器
/// </summary>
public class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
{
    /// <summary>
    /// 日期格式
    /// </summary>
    private readonly string _format;

    /// <summary>
    /// 初始化可空日期格式Json转换器
    /// </summary>
    public NullableDateTimeJsonConverter() : this("yyyy-MM-dd HH:mm:ss")
    {
    }

    /// <summary>
    /// 初始化可空日期格式Json转换器
    /// </summary>
    /// <param name="format">日期格式,默认值: yyyy-MM-dd HH:mm:ss</param>
    public NullableDateTimeJsonConverter(string format)
    {
        _format = format;
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public override DateTime? Read(ref Utf8JsonReader reader, SystemType typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return reader.GetString().SafeString().ToDateTime().ToLocalTime();
        if (reader.TryGetDateTime(out DateTime date))
            return date.ToLocalTime();
        return DateTime.MinValue;
    }

    /// <summary>
    /// 写入数据
    /// </summary>
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }
        string date = value.Value.ToLocalTime().ToString(_format);
        writer.WriteStringValue(date);
    }
}