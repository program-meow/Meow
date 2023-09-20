using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// 正则操作
/// </summary>
public static class Regex {
    /// <summary>
    /// 获取匹配值集合
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="resultPatterns">结果模式字符串数组,范例：new[]{"$1","$2"}</param>
    /// <param name="options">选项</param>
    public static Dictionary<string , string> GetValues( string value , string pattern , string[] resultPatterns , RegexOptions options = RegexOptions.IgnoreCase ) {
        Dictionary<string , string> result = new Dictionary<string , string>();
        if( value.IsEmpty() )
            return result;
        Match match = System.Text.RegularExpressions.Regex.Match( value , pattern , options );
        if( match.Success == false )
            return result;
        AddResults( result , match , resultPatterns );
        return result;
    }

    /// <summary>
    /// 添加匹配结果
    /// </summary>
    private static void AddResults( Dictionary<string , string> result , Match match , string[] resultPatterns ) {
        if( resultPatterns == null ) {
            result.Add( string.Empty , match.Value );
            return;
        }
        foreach( string resultPattern in resultPatterns )
            result.Add( resultPattern , match.Result( resultPattern ) );
    }

    /// <summary>
    /// 获取匹配值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="resultPattern">结果模式字符串,范例："$1"用来获取第一个()内的值</param>
    /// <param name="options">选项</param>
    public static string GetValue( string value , string pattern , string resultPattern = "" , RegexOptions options = RegexOptions.IgnoreCase ) {
        if( value.IsEmpty() )
            return string.Empty;
        Match match = System.Text.RegularExpressions.Regex.Match( value , pattern , options );
        if( match.Success == false )
            return string.Empty;
        return resultPattern.IsEmpty() ? match.Value : match.Result( resultPattern );
    }

    /// <summary>
    /// 分割成字符串数组
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="options">选项</param>
    public static string[] Split( string value , string pattern , RegexOptions options = RegexOptions.IgnoreCase ) {
        if( value.IsEmpty() )
            return new string[] { };
        return System.Text.RegularExpressions.Regex.Split( value , pattern , options );
    }

    /// <summary>
    /// 替换
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="replacement">替换字符串</param>
    /// <param name="options">选项</param>
    public static string Replace( string value , string pattern , string replacement , RegexOptions options = RegexOptions.IgnoreCase ) {
        if( value.IsEmpty() )
            return string.Empty;
        return System.Text.RegularExpressions.Regex.Replace( value , pattern , replacement , options );
    }

    /// <summary>
    /// 验证输入与模式是否匹配
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>        
    public static bool IsMatch( string value , string pattern ) {
        return System.Text.RegularExpressions.Regex.IsMatch( value , pattern );
    }

    /// <summary>
    /// 验证输入与模式是否匹配
    /// </summary>
    /// <param name="value">输入的字符串</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="options">选项</param>
    public static bool IsMatch( string value , string pattern , RegexOptions options ) {
        return System.Text.RegularExpressions.Regex.IsMatch( value , pattern , options );
    }

    /// <summary>
    ///获取匹配结果
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    public static Match Match( string value , string pattern ) {
        return System.Text.RegularExpressions.Regex.Match( value , pattern );
    }

    /// <summary>
    ///获取匹配结果
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="options">选项</param>
    public static Match Match( string value , string pattern , RegexOptions options ) {
        return System.Text.RegularExpressions.Regex.Match( value , pattern , options );
    }

    /// <summary>
    ///获取匹配结果集合
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    public static MatchCollection Matches( string value , string pattern ) {
        return System.Text.RegularExpressions.Regex.Matches( value , pattern );
    }

    /// <summary>
    ///获取匹配结果集合
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="options">选项</param>
    public static MatchCollection Matches( string value , string pattern , RegexOptions options ) {
        return System.Text.RegularExpressions.Regex.Matches( value , pattern , options );
    }

    /// <summary>
    ///获取匹配结果集合
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="options">选项</param>
    /// <param name="matchTimeout">匹配超时间隔</param>
    public static MatchCollection Matches( string value , string pattern , RegexOptions options , TimeSpan matchTimeout ) {
        return System.Text.RegularExpressions.Regex.Matches( value , pattern , options , matchTimeout );
    }
}