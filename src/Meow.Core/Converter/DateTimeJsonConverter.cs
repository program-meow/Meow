using Meow.Helper;
using SystemType = System.Type;

namespace Meow.Converter;

/// <summary>
/// 日期格式Json转换器
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime> {
    /// <summary>
    /// 日期格式
    /// </summary>
    private readonly string _format;

    /// <summary>
    /// 初始化日期格式Json转换器
    /// </summary>
    public DateTimeJsonConverter() : this( "yyyy-MM-dd HH:mm:ss" ) {
    }

    /// <summary>
    /// 初始化日期格式Json转换器
    /// </summary>
    /// <param name="format">日期格式,默认值: yyyy-MM-dd HH:mm:ss</param>
    public DateTimeJsonConverter( string format ) {
        _format = format;
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public override DateTime Read( ref Utf8JsonReader reader , SystemType typeToConvert , JsonSerializerOptions options ) {
        if( reader.TokenType == JsonTokenType.String )
            return Time.ToLocalTime( Meow.Helper.Convert.ToDateTime( reader.GetString() ) );
        if( reader.TryGetDateTime( out var date ) )
            return Time.ToLocalTime( date );
        return DateTime.MinValue;
    }

    /// <summary>
    /// 写入数据
    /// </summary>
    public override void Write( Utf8JsonWriter writer , DateTime value , JsonSerializerOptions options ) {
        string date = Time.ToLocalTime( value ).ToString( _format );
        writer.WriteStringValue( date );
    }
}