﻿using Meow.Converter;
using Meow.Domain.Extending;
using Meow.Extension;

namespace Meow.Data.Dapper.TypeHandler;

/// <summary>
/// 扩展属性类型转换器
/// </summary>
public class ExtraPropertiesTypeHandler : SqlMapper.TypeHandler<ExtraPropertyDictionary> {
    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="parameter">参数</param>
    /// <param name="value">扩展属性值</param>
    public override void SetValue( IDbDataParameter parameter , ExtraPropertyDictionary value ) {
        if( parameter == null )
            return;
        if( value == null )
            return;
        parameter.Value = PropertiesToJson( value );
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
    /// 转换值
    /// </summary>
    /// <param name="value">json字符串</param>
    public override ExtraPropertyDictionary Parse( object value ) {
        return JsonToProperties( value.SafeString() );
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