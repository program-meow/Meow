// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Meow.Calendar.Almanac.EightChar;

/// <summary>
/// 大运
/// </summary>
public class DaYun {

    /// <summary>
    /// 序数，0-9
    /// </summary>
    internal readonly int _index;

    /// <summary>
    /// 运
    /// </summary>
    private readonly Yun _yun;

    /// <summary>
    /// 阴历
    /// </summary>
    internal readonly Lunar _lunar;

    /// <summary>
    /// 从运和序数初始化
    /// </summary>
    /// <param name="yun">运</param>
    /// <param name="index">序数</param>
    public DaYun( Yun yun , int index ) {
        _yun = yun;
        _lunar = yun._lunar;
        _index = index;
        var birthYear = _lunar._solar.Year;
        var year = yun.StartSolar.Year;
        if( index < 1 ) {
            StartYear = birthYear;
            StartAge = 1;
            EndYear = year - 1;
            EndAge = year - birthYear;
        } else {
            var add = ( index - 1 ) * 10;
            StartYear = year + add;
            StartAge = StartYear - birthYear + 1;
            EndYear = StartYear + 9;
            EndAge = StartAge + 9;
        }
    }

    /// <summary>
    /// 开始年(含)
    /// </summary>
    public int StartYear { get; }

    /// <summary>
    /// 结束年(含)
    /// </summary>
    public int EndYear { get; }

    /// <summary>
    /// 开始年龄(含)
    /// </summary>
    public int StartAge { get; }

    /// <summary>
    /// 结束年龄(含)
    /// </summary>
    public int EndAge { get; }

    /// <summary>
    /// 获取干支
    /// </summary>
    /// <returns>干支</returns>
    public string GanZhi {
        get {
            if( _index < 1 ) {
                return "";
            }
            var offset = LunarUtil.GetJiaZiIndex( _lunar.MonthInGanZhiExact );
            offset += _yun.Forward ? _index : -_index;
            var size = LunarUtil.JIA_ZI.Length;
            if( offset >= size ) {
                offset -= size;
            }
            if( offset < 0 ) {
                offset += size;
            }
            return LunarUtil.JIA_ZI[ offset ];
        }
    }

    /// <summary>
    /// 旬
    /// </summary>
    public string Xun => LunarUtil.GetXun( GanZhi );

    /// <summary>
    /// 旬空(空亡)
    /// </summary>
    public string XunKong => LunarUtil.GetXunKong( GanZhi );

    /// <summary>
    /// 获取流年
    /// </summary>
    /// <param name="n">轮数</param>
    /// <returns>流年</returns>
    public LiuNian[] GetLiuNian( int n = 10 ) {
        if( _index < 1 ) {
            n = EndYear - StartYear + 1;
        }
        var l = new LiuNian[ n ];
        for( var i = 0 ; i < n ; ++i ) {
            l[ i ] = new LiuNian( this , i );
        }
        return l;
    }

    /// <summary>
    /// 获取小运
    /// </summary>
    /// <param name="n">轮数</param>
    /// <returns>小运</returns>
    public XiaoYun[] GetXiaoYun( int n = 10 ) {
        if( _index < 1 ) {
            n = EndYear - StartYear + 1;
        }
        var l = new XiaoYun[ n ];
        for( var i = 0 ; i < n ; ++i ) {
            l[ i ] = new XiaoYun( this , i , _yun.Forward );
        }
        return l;
    }
}