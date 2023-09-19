using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Text;
using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// 字符串操作
/// </summary>
public static class String {
    /// <summary>
    /// 空字符串
    /// </summary>
    public static string Empty => string.Empty;

    /// <summary>
    /// 换行符
    /// </summary>
    public static string Line => System.Environment.NewLine;

    #region Unique  [全局唯一值]

    /// <summary>
    /// 全局唯一值
    /// </summary>
    public static string Unique() {
        return System.Guid.NewGuid().ToString( "N" );
    }

    #endregion

    #region FirstUpperCase  [首字母大写]

    /// <summary>
    /// 首字母大写
    /// </summary>
    /// <param name="value">值</param>
    public static string FirstUpperCase( string value ) {
        if( value.IsEmpty() )
            return Empty;
        OperationStatus result = Rune.DecodeFromUtf16( value , out Rune rune , out int charsConsumed );
        if( result != OperationStatus.Done || Rune.IsUpper( rune ) )
            return value;
        return Rune.ToUpperInvariant( rune ) + value[ charsConsumed.. ];
    }

    #endregion

    #region FirstLowerCase  [首字母小写]

    /// <summary>
    /// 首字母小写
    /// </summary>
    /// <param name="value">值</param>
    public static string FirstLowerCase( string value ) {
        if( value.IsEmpty() )
            return Empty;
        OperationStatus result = Rune.DecodeFromUtf16( value , out Rune rune , out int charsConsumed );
        if( result != OperationStatus.Done || Rune.IsLower( rune ) )
            return value;
        return Rune.ToLowerInvariant( rune ) + value[ charsConsumed.. ];
    }

    #endregion

    #region RemoveStart  [移除起始字符串]

    /// <summary>
    /// 移除起始字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="start">要移除的值</param>
    public static string RemoveStart( string value , string start ) {
        if( string.IsNullOrWhiteSpace( value ) )
            return string.Empty;
        if( string.IsNullOrEmpty( start ) )
            return value;
        if( value.StartsWith( start , StringComparison.Ordinal ) == false )
            return value;
        return value.Substring( start.Length , value.Length - start.Length );
    }

    /// <summary>
    /// 移除起始字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="start">要移除的值</param>
    public static StringBuilder RemoveStart( StringBuilder value , string start ) {
        if( value == null || value.Length == 0 )
            return null;
        if( string.IsNullOrEmpty( start ) )
            return value;
        if( start.Length > value.Length )
            return value;
        char[] chars = start.ToCharArray();
        for( int i = 0 ; i < chars.Length ; i++ ) {
            if( value[ i ] != chars[ i ] )
                return value;
        }
        return value.Remove( 0 , start.Length );
    }

    /// <summary>
    /// 移除起始字符串
    /// </summary>
    /// <param name="writer">字符串写入器</param>
    /// <param name="start">要移除的值</param>
    public static StringWriter RemoveStart( StringWriter writer , string start ) {
        if( writer == null )
            return null;
        var builder = writer.GetStringBuilder();
        RemoveStart( builder , start );
        return writer;
    }

    #endregion

    #region RemoveEnd  [移除末尾字符串]

    /// <summary>
    /// 移除末尾字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="end">要移除的值</param>
    public static string RemoveEnd( string value , string end ) {
        if( string.IsNullOrWhiteSpace( value ) )
            return string.Empty;
        if( string.IsNullOrEmpty( end ) )
            return value;
        if( value.EndsWith( end , StringComparison.Ordinal ) == false )
            return value;
        return value.Substring( 0 , value.LastIndexOf( end , StringComparison.Ordinal ) );
    }

    /// <summary>
    /// 移除末尾字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="end">要移除的值</param>
    public static StringBuilder RemoveEnd( StringBuilder value , string end ) {
        if( value == null || value.Length == 0 )
            return null;
        if( string.IsNullOrEmpty( end ) )
            return value;
        if( end.Length > value.Length )
            return value;
        char[] chars = end.ToCharArray();
        for( int i = chars.Length - 1 ; i >= 0 ; i-- ) {
            int j = value.Length - ( chars.Length - i );
            if( value[ j ] != chars[ i ] )
                return value;
        }
        return value.Remove( value.Length - end.Length , end.Length );
    }

    /// <summary>
    /// 移除末尾字符串
    /// </summary>
    /// <param name="writer">字符串写入器</param>
    /// <param name="end">要移除的值</param>
    public static StringWriter RemoveEnd( this StringWriter writer , string end ) {
        if( writer == null )
            return null;
        var builder = writer.GetStringBuilder();
        RemoveEnd( builder , end );
        return writer;
    }

    #endregion

    #region Truncate  [截断字符串]

    /// <summary>
    /// 截断字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="length">返回长度</param>
    /// <param name="endCharCount">添加结束符号的个数，默认0，不添加</param>
    /// <param name="endChar">结束符号，默认为省略号</param>
    /// <returns>截断字符串</returns>
    public static string Truncate( string value , int length , int endCharCount = 0 , string endChar = "." ) {
        if( value.IsEmpty() )
            return String.Empty;
        if( value.Length < length )
            return value;
        var result = new StringBuilder();
        result.Append( value.Substring( 0 , length ) );
        if( endCharCount < 1 )
            return result.ToString();
        result.Append( Copy( endChar , endCharCount ) );
        return result.ToString();
    }

    #endregion

    #region Copy  [复制]

    /// <summary>
    /// 复制
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="count">复制次数</param>
    /// <returns>复制好的字符串</returns>
    public static string Copy( string value , int count ) {
        if( value.IsEmpty() )
            return String.Empty;
        if( count <= 1 )
            return value;
        StringBuilder result = new StringBuilder();
        for( int i = 0 ; i < count ; i++ )
            result.Append( value );
        return result.ToString();
    }

    #endregion

    #region 获取拼音



    #endregion

    #region Distinct  [去除重复]

    /// <summary>
    /// 去除重复
    /// </summary>
    /// <param name="value">值，范例1："5555",返回"5",范例2："4545",返回"45"</param>
    public static string Distinct( string value ) {
        var array = value.ToCharArray();
        return new string( array.Distinct().ToArray() );
    }

    #endregion

    #region GetSimilarityRate  [计算匹配率/相似度]

    /// <summary>
    /// 计算相似度
    /// </summary>
    /// <param name="firstValue">第一个值</param>
    /// <param name="secondValue">第二个值</param>
    public static SimilarityRateResult GetSimilarityRate( string firstValue , string secondValue ) {
        SimilarityRateResult result = new SimilarityRateResult();
        char[] firstArrChar = firstValue.ToCharArray();
        char[] secondArrChar = secondValue.ToCharArray();
        int computeTimes = 0;
        int row = firstArrChar.Length + 1;
        int column = secondArrChar.Length + 1;
        int[,] matrix = new int[ row , column ];
        //开始时间
        DateTime beginTime = DateTime.Now;
        //初始化矩阵的第一行和第一列
        for( int i = 0 ; i < column ; i++ )
            matrix[ 0 , i ] = i;
        for( int i = 0 ; i < row ; i++ )
            matrix[ i , 0 ] = i;
        for( int i = 1 ; i < row ; i++ ) {
            for( int j = 1 ; j < column ; j++ ) {
                int intCost = 0;
                intCost = firstArrChar[ i - 1 ] == secondArrChar[ j - 1 ] ? 0 : 1;
                //关键步骤，计算当前位置值为左边+1、上面+1、左上角+intCost中的最小值 
                //循环遍历到最后_Matrix[_Row - 1, _Column - 1]即为两个字符串的距离
                matrix[ i , j ] = Number.Minimum( matrix[ i - 1 , j ] + 1 , matrix[ i , j - 1 ] + 1 , matrix[ i - 1 , j - 1 ] + intCost ) ?? 0;
                computeTimes++;
            }
        }
        //结束时间
        DateTime endTime = DateTime.Now;
        //相似率 移动次数小于最长的字符串长度的20%算同一题
        int intLength = row > column ? row : column;
        result.Rate = ( 1 - ( double ) matrix[ row - 1 , column - 1 ] / ( intLength - 1 ) );
        result.ExeTime = ( endTime - beginTime ).TotalMilliseconds;
        result.ComputeTimes = computeTimes.ToString() + " 距离为：" + matrix[ row - 1 , column - 1 ].ToString();
        return result;
    }

    /// <summary>
    /// 计算相似度结果
    /// </summary>
    public struct SimilarityRateResult {
        /// <summary>
        /// 相似度，0.54即54%。
        /// </summary>
        public double Rate;
        /// <summary>
        /// 对比次数
        /// </summary>
        public string ComputeTimes;
        /// <summary>
        /// 执行时间，毫秒
        /// </summary>
        public double ExeTime;
    }

    #endregion

    #region HideSensitiveInfo  [隐藏敏感信息]

    /// <summary>
    /// 隐藏敏感信息
    /// </summary>
    /// <param name="info">信息实体</param>
    /// <param name="left">左边保留的字符数</param>
    /// <param name="right">右边保留的字符数</param>
    /// <param name="placeholderCount">占位符数量</param>
    /// <param name="placeholder">占位符。默认：使用 * 符号</param>
    /// <param name="basedOnLeft">当长度异常时，是否显示左边 ，true显示左边，false显示右边 </param>
    public static string HideSensitiveInfo( string info , int left , int right , int placeholderCount = 4 , char placeholder = '*' , bool basedOnLeft = true ) {
        if( info.IsEmpty() )
            return string.Empty;

        if( right < 0 ) right = 0;
        if( left < 0 ) left = 0;

        if( info.Length - left - right > 0 ) {
            return info.Substring( 0 , left )
                .PadRight( left + placeholderCount , placeholder )
                .Insert( left + placeholderCount , info.Substring( info.Length - right ) );
        }

        if( basedOnLeft ) {
            return info.Length > left && left > 0
                ? info.Remove( left ).Insert( left , new string( placeholder , placeholderCount ) )
                : info.Substring( 0 , 1 ).PadRight( 1 + placeholderCount , placeholder );
        }

        return info.Length > right && right > 0
            ? info.Substring( info.Length - right ).PadLeft( right + placeholderCount , placeholder )
            : info.Substring( 0 , 1 ).PadLeft( 1 + placeholderCount , placeholder );
    }

    /// <summary>
    /// 隐藏手机号细节
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="left">左边保留字符数</param>
    /// <param name="right">右边保留字符数</param>
    public static string HidePhoneDetail( string phone , int left = 3 , int right = 4 ) => HideSensitiveInfo( phone , left , right );

    /// <summary>
    /// 隐藏邮箱地址细节
    /// </summary>
    /// <param name="email">邮箱地址</param>
    /// <param name="left">邮箱地址头保留字符个数，默认值设置为3</param>
    public static string HideEmailDetail( string email , int left = 3 ) {
        if( email.IsEmpty() )
            return string.Empty;
        if( !email.IsEmail() )
            return HideSensitiveInfo( email , left , 0 );
        var suffixLen = email!.Length - email.LastIndexOf( '@' );
        return HideSensitiveInfo( email , left , suffixLen , basedOnLeft: false );
    }

    #endregion

    #region ToSbcCase & ToDbcCase  [全角 & 半角]

    /// <summary>
    /// 转全角(SBC case)
    /// </summary>
    /// <param name="value">值</param>
    public static string ToSbcCase( string value ) {
        char[] c = value.ToCharArray();
        for( int i = 0 ; i < c.Length ; i++ ) {
            if( c[ i ] == 32 ) {
                c[ i ] = ( char ) 12288;
                continue;
            }
            if( c[ i ] < 127 )
                c[ i ] = ( char ) ( c[ i ] + 65248 );
        }
        return new string( c );
    }

    /// <summary>
    /// 转半角
    /// </summary>
    /// <param name="value">值</param>
    public static string ToDbcCase( string value ) {
        char[] c = value.ToCharArray();
        for( int i = 0 ; i < c.Length ; i++ ) {
            if( c[ i ] == 12288 ) {
                c[ i ] = ( char ) 32;
                continue;
            }
            if( c[ i ] > 65280 && c[ i ] < 65375 )
                c[ i ] = ( char ) ( c[ i ] - 65248 );
        }
        return new string( c );
    }

    #endregion

    #region GetRepeatCount  [获取重复次数]

    /// <summary>
    /// 获取重复次数
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="repeatValue">重复值</param>
    /// <param name="isFuzzy">是否模糊大小写</param>
    public static int GetRepeatCount( string value , string repeatValue , bool isFuzzy = false ) {
        if( value.IsEmpty() || repeatValue.IsEmpty() )
            return 0;
        value = isFuzzy ? value.ToLower() : value;
        repeatValue = isFuzzy ? repeatValue.ToLower() : repeatValue;
        if( !value.Contains( repeatValue ) )
            return 0;
        var replace = value.Replace( repeatValue , "" );
        return ( value.Length - replace.Length ) / repeatValue.Length;
    }

    #endregion

    #region Reverse  [翻转]

    /// <summary>
    /// 翻转
    /// </summary>
    /// <param name="value">值</param>
    public static string Reverse( string value ) {
        if( value.IsEmpty() )
            return string.Empty;
        return new string( value.ToCharArray().Reverse().ToArray() );
    }

    #endregion

    #region Extract  [提取字符串中的变量值]

    /// <summary>
    /// 提取字符串中的变量值
    /// </summary>
    /// <param name="value">原始值,范例: Hello,World</param>
    /// <param name="format">字符串格式,范例: 原始值为Hello,World,格式为Hello,{value} ,则value变量的值为World</param>
    public static IDictionary<string , string> Extract( string value , string format ) {
        Dictionary<string , string> result = new Dictionary<string , string>();
        if( value.IsEmpty() )
            return result;
        if( format.IsEmpty() )
            return result;
        if( format.Contains( "{" , StringComparison.Ordinal ) == false )
            return result;
        if( format.Contains( "}" , StringComparison.Ordinal ) == false )
            return result;
        List<string> formatItems = SplitFormat( format.SafeString() );
        return ExtractValue( value.SafeString() , formatItems );
    }

    /// <summary>
    /// 拆分格式字符串
    /// </summary>
    private static List<string> SplitFormat( string format ) {
        List<string> result = new List<string>();
        StringBuilder item = new StringBuilder();
        for( int i = 0 ; i < format.Length ; i++ ) {
            char temp = format[ i ];
            if( temp == '{' ) {
                item.RemoveEnd( "{" );
                if( i == 0 ) {
                    result.Add( string.Empty );
                }
                if( item.Length > 0 ) {
                    result.Add( item.ToString() );
                    item.Clear();
                }
                item.Append( temp );
                continue;
            }
            if( temp == '}' ) {
                if( item.ToString().IsEmpty() )
                    continue;
                item.RemoveEnd( "}" );
                item.Append( temp );
                result.Add( item.ToString() );
                item.Clear();
                if( i == format.Length - 1 )
                    result.Add( string.Empty );
                continue;
            }
            item.Append( temp );
            if( i == format.Length - 1 ) {
                result.Add( item.ToString() );
                item.Clear();
            }
        }
        return result;
    }

    /// <summary>
    /// 提取字符串中变量值
    /// </summary>
    private static IDictionary<string , string> ExtractValue( string value , List<string> formatItems ) {
        Dictionary<string , string> result = new Dictionary<string , string>();
        int leftIndex = 0;
        int length = 0;
        for( int i = 0 ; i < formatItems.Count ; i++ ) {
            string item = formatItems[ i ];
            if( item == string.Empty )
                continue;
            if( item.StartsWith( "{" , StringComparison.Ordinal ) == false ) {
                leftIndex += item.Length;
                continue;
            }
            if( i + 1 < formatItems.Count ) {
                string rightItem = formatItems[ i + 1 ];
                if( rightItem == string.Empty )
                    length = value.Length - leftIndex;
                else
                    length = value.IndexOf( rightItem , leftIndex + 1 , StringComparison.OrdinalIgnoreCase ) - leftIndex;
            }
            string varName = item.Replace( "{" , "" ).Replace( "}" , "" );
            if( length <= 0 ) {
                result.Add( varName , string.Empty );
                continue;
            }
            string variableValue = value.Substring( leftIndex , length );
            result.Add( varName , variableValue );
            leftIndex += length;
        }
        return result;
    }

    #endregion
}