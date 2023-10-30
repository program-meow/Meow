namespace Meow.Json.Converter;

/// <summary>
/// 枚举Json转换器
/// </summary>
public class EnumJsonConverter<T> : JsonConverter<T> where T : struct, System.Enum
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public override T Read(ref Utf8JsonReader reader, SystemType type, JsonSerializerOptions options)
    {
        bool isSuccess = reader.TryGetSingle(out float floatValue);
        if (isSuccess)
            return (T)System.Enum.Parse(type, floatValue.ToString(CultureInfo.InvariantCulture), true);
        isSuccess = reader.TryGetInt32(out int intValue);
        if (isSuccess)
            return (T)System.Enum.Parse(type, intValue.ToString(CultureInfo.InvariantCulture), true);
        return default;
    }


    /// <summary>
    /// 写入数据
    /// </summary>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        TypeCode code = SystemType.GetTypeCode(typeof(T));
        switch (code)
        {
            case TypeCode.Byte:
                writer.WriteNumberValue(Unsafe.As<T, byte>(ref value));
                break;
            case TypeCode.Int32:
                writer.WriteNumberValue(Unsafe.As<T, int>(ref value));
                break;
        }
    }
}