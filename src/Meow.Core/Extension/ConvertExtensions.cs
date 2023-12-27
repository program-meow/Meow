using Meow.Enum;

namespace Meow.Extension;

/// <summary>
/// 类型扩展
/// </summary>
public static class ConvertExtensions {

    #region To  [通用泛型转换]

    /// <summary>
    /// 通用泛型转换
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="value">值</param>
    public static T To<T>( this object value ) {
        return Meow.Helper.Convert.To<T>( value );
    }

    #endregion

    #region ToList  [泛型集合转换]

    /// <summary>
    /// 通用泛型转换 - 字符串集合
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="array">集合</param>
    public static List<string> ToListString<T>( this IEnumerable<T> array ) {
        return Meow.Helper.Convert.ToListString( array );
    }

    /// <summary>
    /// 通用泛型转换
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="array">集合</param>
    public static List<T> ToList<T>( this IEnumerable<object> array ) {
        return Meow.Helper.Convert.ToList<T>( array );
    }

    /// <summary>
    /// 泛型集合转换
    /// </summary>
    /// <typeparam name="T">目标元素类型</typeparam>
    /// <param name="value">以字符分隔的元素集合字符串</param>
    /// <param name="separator">分隔符，默认逗号作为分隔符</param>
    public static List<T> ToList<T>( this string value , string separator = "," ) {
        return Meow.Helper.Convert.ToList<T>( value , separator );
    }

    /// <summary>
    /// 提取集合
    /// </summary>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <typeparam name="TKey">键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<TKey> ToListBy<TSource, TKey>( this IEnumerable<TSource> array , Func<TSource , TKey> keySelector ) {
        return Meow.Helper.Convert.ToListBy( array , keySelector );
    }

    /// <summary>
    /// 提取并合并集合
    /// </summary>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <typeparam name="TKey">键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<TKey> ToListRangeBy<TSource, TKey>( this IEnumerable<TSource> array , Func<TSource , IEnumerable<TKey>> keySelector ) {
        return Meow.Helper.Convert.ToListRangeBy( array , keySelector );
    }

    #endregion

    #region ToInt & ToIntOrNull  [转换为32位整型&可空整型] 

    /// <summary>
    /// 转换为32位整型
    /// </summary>
    /// <param name="value">值</param>
    public static int ToInt( this object value ) {
        return Meow.Helper.Convert.ToInt( value );
    }

    /// <summary>
    /// 转换为32位可空整型
    /// </summary>
    /// <param name="value">值</param>
    public static int? ToIntOrNull( this object value ) {
        return Meow.Helper.Convert.ToIntOrNull( value );
    }

    #endregion

    #region ToFloat & ToFloatOrNull  [转换为32位浮点型 & 可空浮点型]

    /// <summary>
    /// 转换为32位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static float ToFloat( this object value , int? digits = null ) {
        return Meow.Helper.Convert.ToFloat( value , digits );
    }

    /// <summary>
    /// 转换为32位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static float? ToFloatOrNull( this object value , int? digits = null ) {
        return Meow.Helper.Convert.ToFloatOrNull( value , digits );
    }

    #endregion

    #region ToDouble & ToDoubleOrNull  [转换为64位浮点型 & 可空浮点型]

    /// <summary>
    /// 转换为64位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static double ToDouble( this object value , int? digits = null ) {
        return Meow.Helper.Convert.ToDouble( value , digits );
    }

    /// <summary>
    /// 转换为64位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static double? ToDoubleOrNull( this object value , int? digits = null ) {
        return Meow.Helper.Convert.ToDoubleOrNull( value , digits );
    }

    #endregion

    #region ToLong & ToLongOrNull  [转换为64位整型 & 可空整型]

    /// <summary>
    /// 转换为64位整型
    /// </summary>
    /// <param name="value">值</param>
    public static long ToLong( this object value ) {
        return Meow.Helper.Convert.ToLong( value );
    }

    /// <summary>
    /// 转换为64位可空整型
    /// </summary>
    /// <param name="value">值</param>
    public static long? ToLongOrNull( this object value ) {
        return Meow.Helper.Convert.ToLongOrNull( value );
    }

    #endregion

    #region ToDecimal & ToDecimalOrNull  [转换为128位浮点型 & 可空浮点型] 

    /// <summary>
    /// 转换为128位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static decimal ToDecimal( this object value , int? digits = null ) {
        return Meow.Helper.Convert.ToDecimal( value , digits );
    }

    /// <summary>
    /// 转换为128位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static decimal? ToDecimalOrNull( this object value , int? digits = null ) {
        return Meow.Helper.Convert.ToDecimalOrNull( value , digits );
    }

    #endregion

    #region ToDateTime & ToDateTimeOrNull  [转换为日期 & 可空日期] 

    /// <summary>
    /// 转换为日期
    /// </summary>
    /// <param name="value">值</param>
    public static DateTime ToDateTime( this object value ) {
        return Meow.Helper.Convert.ToDateTime( value );
    }

    /// <summary>
    /// 转换为可空日期
    /// </summary>
    /// <param name="value">值</param>
    public static DateTime? ToDateTimeOrNull( this object value ) {
        return Meow.Helper.Convert.ToDateTimeOrNull( value );
    }

    #endregion

    #region ToBool & ToBoolOrNull  [转换为布尔值 & 可空布尔值] 

    /// <summary>
    /// 转换为布尔值
    /// </summary>
    /// <param name="value">值</param>
    public static bool ToBool( this object value ) {
        return Meow.Helper.Convert.ToBool( value );
    }

    /// <summary>
    /// 转换为可空布尔值
    /// </summary>
    /// <param name="value">输入值</param>
    public static bool? ToBoolOrNull( this object value ) {
        return Meow.Helper.Convert.ToBoolOrNull( value );
    }

    #endregion

    #region ToGuid & ToGuidOrNull  [转换为Guid & 可空Guid]

    /// <summary>
    /// 转换为Guid
    /// </summary>
    /// <param name="value">值</param>
    public static Guid ToGuid( this object value ) {
        return Meow.Helper.Convert.ToGuid( value );
    }

    /// <summary>
    /// 转换为可空Guid
    /// </summary>
    /// <param name="value">值</param>
    public static Guid? ToGuidOrNull( this object value ) {
        return Meow.Helper.Convert.ToGuidOrNull( value );
    }

    #endregion

    #region ToGuidList & ToGuidOrNullList  [转换为Guid集合 & 可空Guid集合]

    /// <summary>
    /// 转换为Guid集合
    /// </summary>
    /// <param name="value">以逗号分隔的Guid集合字符串</param>
    /// <param name="separator">分隔符，默认逗号作为分隔符</param>
    public static List<Guid> ToGuidList( this string value , string separator = "," ) {
        return Meow.Helper.Convert.ToGuidList( value , separator );
    }

    /// <summary>
    /// 转换为Guid集合
    /// </summary>
    /// <param name="array">字符串集合</param>
    public static List<Guid> ToGuidList( this IEnumerable<string> array ) {
        return Meow.Helper.Convert.ToGuidList( array );
    }

    /// <summary>
    /// 转换为可空Guid集合
    /// </summary>
    /// <param name="value">以逗号分隔的Guid集合字符串</param>
    /// <param name="separator">分隔符，默认逗号作为分隔符</param>
    public static List<Guid?> ToGuidOrNullList( this string value , string separator = "," ) {
        return Meow.Helper.Convert.ToGuidOrNullList( value , separator );
    }

    /// <summary>
    /// 转换为可空Guid集合
    /// </summary>
    /// <param name="array">字符串集合</param>
    public static List<Guid?> ToGuidOrNullList( this IEnumerable<string> array ) {
        return Meow.Helper.Convert.ToGuidOrNullList( array );
    }

    #endregion

    #region ToBytes  [转换为字节数组] 

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="value">值</param>        
    public static byte[] ToBytes( this string value ) {
        return Meow.Helper.Convert.ToBytes( value );
    }

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="encoding">字符编码</param>
    public static byte[] ToBytes( this string value , Encoding encoding ) {
        return Meow.Helper.Convert.ToBytes( value , encoding );
    }

    #endregion

    #region ToBase64  [转换为base64字符串]

    /// <summary>
    /// 转换为base64字符串
    /// </summary>
    /// <param name="value">值</param>        
    public static string ToBase64( this string value ) {
        return Meow.Helper.Convert.ToBase64( value );
    }

    #endregion

    #region ToDictionary [对象转换为属性名值对] 

    /// <summary>
    /// 对象转换为属性名值对
    /// </summary>
    /// <param name="data">对象</param>
    public static IDictionary<string , object> ToDictionary( this object data ) {
        return Meow.Helper.Convert.ToDictionary( data );
    }

    /// <summary>
    /// 对象转换为属性名值对
    /// </summary>
    /// <param name="data">对象</param>
    /// <param name="useDisplayName">是否使用显示名称,可使用[Description] 或 [DisplayName]特性设置</param>
    public static IDictionary<string , object> ToDictionary( this object data , bool useDisplayName ) {
        return Meow.Helper.Convert.ToDictionary( data , useDisplayName );
    }

    /// <summary>
    /// 对象转换为属性名值对
    /// </summary>
    /// <typeparam name="TValue">键值对值元素类型</typeparam>
    /// <param name="data">对象</param>
    public static IDictionary<string , TValue> ToDictionary<TValue>( this object data ) {
        return Meow.Helper.Convert.ToDictionary<TValue>( data );
    }

    #endregion

    #region ToMoneyFromRmbCn  [转换为大写人民币]

    /// <summary>
    /// 转换为大写人民币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyFromRmbCn( decimal? money , bool isIgnoreSgn = true ) {
        return Meow.Helper.Convert.ToMoneyFromRmbCn( money , isIgnoreSgn );
    }

    /// <summary>
    /// 转换为大写人民币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyFromRmbCn( this decimal money , bool isIgnoreSgn = true ) {
        return Meow.Helper.Convert.ToMoneyFromRmbCn( money , isIgnoreSgn );
    }

    #endregion

    #region ToMoneyFromNum  [转换为数字货币]

    /// <summary>
    /// 转换为数字货币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="moneyType">币种。不设置则无货币符号前缀</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyFromNum( this decimal? money , MoneyEnum? moneyType = null , bool isIgnoreSgn = false ) {
        return Meow.Helper.Convert.ToMoneyFromNum( money , moneyType , isIgnoreSgn );
    }

    /// <summary>
    /// 转换为数字货币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="moneyType">币种。不设置则无货币符号前缀</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyFromNum( this decimal money , MoneyEnum? moneyType = null , bool isIgnoreSgn = false ) {
        return Meow.Helper.Convert.ToMoneyFromNum( money , moneyType , isIgnoreSgn );
    }

    #endregion
}