namespace Meow.Data.EntityFrameworkCore.ValueConverter;

/// <summary>
/// 扩展属性值转换器
/// </summary>
public class ExtraPropertiesValueConverter : ValueConverter<ExtraPropertyDictionary , string> {
    /// <summary>
    /// 初始化扩展属性值转换器
    /// </summary>
    public ExtraPropertiesValueConverter()
        : base( extraProperties => PropertiesToJson( extraProperties ) , json => JsonToProperties( json ) ) {
    }

    /// <summary>
    /// 扩展属性转换为json
    /// </summary>
    private static string PropertiesToJson( ExtraPropertyDictionary extraProperties ) {
        JsonSerializerOptions options = new JsonSerializerOptions {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull ,
            Encoder = JavaScriptEncoder.Create( UnicodeRanges.All ) ,
            Converters = {
                new UtcDateTimeJsonConverter(),
                new UtcNullableDateTimeJsonConverter()
            }
        };
        return Meow.Helper.Json.ToJson( extraProperties , options );
    }

    /// <summary>
    /// json转换为扩展属性
    /// </summary>
    private static ExtraPropertyDictionary JsonToProperties( string json ) {
        if( json.IsEmpty() || json == "{}" )
            return new ExtraPropertyDictionary();
        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return Meow.Helper.Json.ToObject<ExtraPropertyDictionary>( json , options ) ?? new ExtraPropertyDictionary();
    }
}