using Meow.Extension;
using System.Collections.Specialized;

namespace Meow.Helper;

/// <summary>
/// Url操作
/// </summary>
public static class Url {

    #region JoinPath  [连接路径]

    /// <summary>
    /// 连接路径
    /// </summary>
    /// <param name="paths">路径列表</param>
    public static string JoinPath( params string[] paths ) {
        if( paths == null )
            return string.Empty;
        paths = paths.Where( path => !path.IsEmpty() ).Select( t => t.Replace( @"\" , "/" ) ).ToArray();
        if( paths.Length == 0 )
            return string.Empty;
        string firstPath = paths.First();
        string lastPath = paths.Last();
        paths = paths.Select( t => t.Trim( '/' ) ).ToArray();
        string result = Path.Combine( paths ).Replace( @"\" , "/" );
        if( firstPath.StartsWith( '/' ) )
            result = $"/{result}";
        if( lastPath.EndsWith( '/' ) )
            result = $"{result}/";
        return result;
    }

    #endregion

    #region AddQueryString  [将给定的查询键和值附加到URI]

    /// <summary>
    /// 将给定的查询键和值附加到URI
    /// </summary>
    /// <param name="uri">基本URI</param>
    /// <param name="query">要追加的名称值查询对的集合</param>
    public static string AddQueryString( string uri , IEnumerable<KeyValuePair<string , string>> query ) {
        return AddQueryString( uri , query.Where( t => !t.Value.IsEmpty() ).Select( t => new KeyValuePair<string , object>( t.Key , t.Value ) ) );
    }

    /// <summary>
    /// 将给定的查询键和值附加到URI
    /// </summary>
    /// <param name="uri">基本URI</param>
    /// <param name="query">要追加的名称值查询对的集合</param>
    public static string AddQueryString( string uri , IEnumerable<KeyValuePair<string , object>> query ) {
        if( uri.IsEmpty() )
            throw new ArgumentNullException( nameof( uri ) );
        if( query.IsEmpty() )
            return uri;
        int num = uri.IndexOf( '#' );
        string str1 = uri;
        string str2 = "";
        if( num != -1 ) {
            str2 = uri.Substring( num );
            str1 = uri.Substring( 0 , num );
        }
        bool flag = str1.IndexOf( '?' ) != -1;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append( str1 );
        foreach( KeyValuePair<string , object> keyValuePair in query ) {
            string value = keyValuePair.Value.SafeString();
            if( value.IsEmpty() )
                continue;
            stringBuilder.Append( flag ? '&' : '?' );
            stringBuilder.Append( UrlEncoder.Default.Encode( keyValuePair.Key ) );
            stringBuilder.Append( '=' );
            stringBuilder.Append( UrlEncoder.Default.Encode( value ) );
            flag = true;
        }
        stringBuilder.Append( str2 );
        return stringBuilder.ToString();
    }

    #endregion

    #region Encode  [Url编码]

    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
    public static string Encode( string url , bool isUpper = false ) {
        return Encode( url , Encoding.UTF8 , isUpper );
    }

    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="encoding">字符编码</param>
    /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
    public static string Encode( string url , string encoding , bool isUpper = false ) {
        encoding = string.IsNullOrWhiteSpace( encoding ) ? "UTF-8" : encoding;
        return Encode( url , Encoding.GetEncoding( encoding ) , isUpper );
    }

    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="encoding">字符编码</param>
    /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
    public static string Encode( string url , Encoding encoding , bool isUpper = false ) {
        string result = HttpUtility.UrlEncode( url , encoding );
        if( isUpper == false )
            return result;
        return GetUpperEncode( result );
    }

    /// <summary>
    /// 获取大写编码字符串
    /// </summary>
    private static string GetUpperEncode( string encode ) {
        StringBuilder result = new StringBuilder();
        int index = int.MinValue;
        for( int i = 0 ; i < encode.Length ; i++ ) {
            string character = encode[ i ].ToString();
            if( character == "%" )
                index = i;
            if( i - index == 1 || i - index == 2 )
                character = character.ToUpper();
            result.Append( character );
        }
        return result.ToString();
    }

    #endregion

    #region Decode  [Url解码]

    /// <summary>
    /// Url解码
    /// </summary>
    /// <param name="url">url</param>
    public static string Decode( string url ) {
        return HttpUtility.UrlDecode( url );
    }

    /// <summary>
    /// Url解码
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="encoding">字符编码</param>
    public static string Decode( string url , Encoding encoding ) {
        return HttpUtility.UrlDecode( url , encoding );
    }

    #endregion

    #region ParseQueryString  [将查询字符串转换为名值对集合]

    /// <summary>
    /// 将查询字符串转换为名值对集合
    /// </summary>
    /// <param name="query">查询字符串</param>
    public static NameValueCollection ParseQueryString( string query ) {
        return HttpUtility.ParseQueryString( query );
    }

    /// <summary>
    /// 将查询字符串转换为名值对集合
    /// </summary>
    /// <param name="query">查询字符串</param>
    /// <param name="encoding">字符编码</param>
    public static NameValueCollection ParseQueryString( string query , Encoding encoding ) {
        return HttpUtility.ParseQueryString( query , encoding );
    }

    #endregion

}