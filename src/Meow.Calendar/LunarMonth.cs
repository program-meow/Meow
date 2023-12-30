// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Meow.Calendar;

/// <summary>
/// 农历月
/// </summary>
public class LunarMonth {

    /// <summary>
    /// 序号
    /// </summary>
    private readonly int _index;

    /// <summary>
    /// 地支序号
    /// </summary>
    private readonly int _zhiIndex;

    /// <summary>
    /// 天干序号
    /// </summary>
    private int _ganIndex {
        get {
            int offset = ( LunarYear.FromYear( Year )._ganIndex + 1 ) % 5 * 2;
            return ( _index - 1 + offset ) % 10;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="lunarYear">农历年</param>
    /// <param name="lunarMonth">农历月：1-12，闰月为负数，如闰2月为-2</param>
    /// <param name="dayCount">天数</param>
    /// <param name="firstJulianDay">初一的儒略日</param>
    /// <param name="index">序号</param>
    public LunarMonth( int lunarYear , int lunarMonth , int dayCount , double firstJulianDay , int index ) {
        Year = lunarYear;
        Month = lunarMonth;
        DayCount = dayCount;
        FirstJulianDay = firstJulianDay;
        _index = index;
        _zhiIndex = ( index - 1 + LunarUtil.BASE_MONTH_ZHI_INDEX ) % 12;
    }

    /// <summary>
    /// 农历年
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// 农历月：1-12，闰月为负数，如闰2月为-2
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// 天数，大月30天，小月29天
    /// </summary>
    public int DayCount { get; }

    /// <summary>
    /// 初一的儒略日
    /// </summary>
    public double FirstJulianDay { get; }

    /// <summary>
    /// 通过农历年月初始化
    /// </summary>
    /// <param name="lunarYear">农历年</param>
    /// <param name="lunarMonth">农历月：1-12，闰月为负数，如闰2月为-2</param>
    /// <returns>农历月</returns>
    public static LunarMonth FromYm( int lunarYear , int lunarMonth ) {
        return LunarYear.FromYear( lunarYear ).GetMonth( lunarMonth );
    }

    /// <summary>
    /// 是否闰月
    /// </summary>
    public bool Leap => Month < 0;

    /// <summary>
    /// 天干
    /// </summary>
    public string Gan => LunarUtil.GAN[ _ganIndex + 1 ];

    /// <summary>
    /// 地支
    /// </summary>
    public string Zhi => LunarUtil.ZHI[ _zhiIndex + 1 ];

    /// <summary>
    /// 干支
    /// </summary>
    public string GanZhi => $"{Gan}{Zhi}";

    /// <summary>
    /// 喜神方位
    /// </summary>
    public string PositionXi => LunarUtil.POSITION_XI[ _ganIndex + 1 ];

    /// <summary>
    /// 喜神方位描述
    /// </summary>
    public string PositionXiDesc => LunarUtil.POSITION_DESC[ PositionXi ];

    /// <summary>
    /// 阳贵神方位
    /// </summary>
    public string PositionYangGui => LunarUtil.POSITION_YANG_GUI[ _ganIndex + 1 ];

    /// <summary>
    /// 阳贵神方位描述
    /// </summary>
    public string PositionYangGuiDesc => LunarUtil.POSITION_DESC[ PositionYangGui ];

    /// <summary>
    /// 阴贵神方位
    /// </summary>
    public string PositionYinGui => LunarUtil.POSITION_YIN_GUI[ _ganIndex + 1 ];

    /// <summary>
    /// 阴贵神方位描述
    /// </summary>
    public string PositionYinGuiDesc => LunarUtil.POSITION_DESC[ PositionYinGui ];

    /// <summary>
    /// 福神方位
    /// </summary>
    public string PositionFu => GetPositionFu();

    /// <summary>
    /// 福神方位
    /// </summary>
    /// <param name="sect">流派</param>
    /// <returns>福神方位</returns>
    public string GetPositionFu( int sect = 2 ) {
        return ( 1 == sect ? LunarUtil.POSITION_FU : LunarUtil.POSITION_FU_2 )[ _ganIndex + 1 ];
    }

    /// <summary>
    /// 福神方位描述
    /// </summary>
    public string PositionFuDesc => GetPositionFuDesc();

    /// <summary>
    /// 福神方位描述
    /// </summary>
    /// <param name="sect">流派</param>
    /// <returns>方位描述</returns>
    public string GetPositionFuDesc( int sect = 2 ) {
        return LunarUtil.POSITION_DESC[ GetPositionFu( sect ) ];
    }

    /// <summary>
    /// 财神方位
    /// </summary>
    public string PositionCai => LunarUtil.POSITION_CAI[ _ganIndex + 1 ];

    /// <summary>
    /// 财神方位描述
    /// </summary>
    public string PositionCaiDesc => LunarUtil.POSITION_DESC[ PositionCai ];

    /// <summary>
    /// 太岁方位，如艮
    /// </summary>
    public string PositionTaiSui {
        get {
            string p;
            int m = SystemMath.Abs( Month );
            switch( m ) {
                case 1:
                case 5:
                case 9:
                    p = "艮";
                    break;
                case 3:
                case 7:
                case 11:
                    p = "坤";
                    break;
                case 4:
                case 8:
                case 12:
                    p = "巽";
                    break;
                default:
                    p = LunarUtil.POSITION_GAN[ Solar.FromJulianDay( FirstJulianDay ).ToLunar()._monthGanIndex ];
                    break;
            }
            return p;
        }
    }

    /// <summary>
    /// 太岁方位描述，如东北
    /// </summary>
    public string PositionTaiSuiDesc => LunarUtil.POSITION_DESC[ PositionTaiSui ];

    /// <summary>
    /// 九星
    /// </summary>
    public NineStar NineStar {
        get {
            int index = LunarYear.FromYear( Year )._zhiIndex % 3;
            int m = SystemMath.Abs( Month );
            int monthZhiIndex = ( 13 + m ) % 12;
            int n = 27 - ( index * 3 );
            if( monthZhiIndex < LunarUtil.BASE_MONTH_ZHI_INDEX ) {
                n -= 3;
            }
            int offset = ( n - monthZhiIndex ) % 9;
            return NineStar.FromIndex( offset );
        }
    }

    /// <inheritdoc />
    public override string ToString() {
        return Year + "年" + ( Leap ? "闰" : "" ) + LunarUtil.MONTH[ SystemMath.Abs( Month ) ] + "月(" + DayCount + "天)";
    }

    /// <summary>
    /// 推移
    /// </summary>
    /// <param name="n">月数</param>
    /// <returns>农历月</returns>
    public LunarMonth Next( int n ) {
        if( 0 == n ) {
            return FromYm( Year , Month );
        }
        if( n > 0 ) {
            int rest = n;
            int ny = Year;
            int iy = ny;
            int im = Month;
            int index = 0;
            List<LunarMonth> months = LunarYear.FromYear( ny ).Months;
            while( true ) {
                int size = months.Count;
                for( int i = 0 ; i < size ; i++ ) {
                    LunarMonth m = months[ i ];
                    if( m.Year != iy || m.Month != im ) continue;
                    index = i;
                    break;
                }

                int more = size - index - 1;
                if( rest < more ) {
                    break;
                }

                rest -= more;
                LunarMonth lastMonth = months[ size - 1 ];
                iy = lastMonth.Year;
                im = lastMonth.Month;
                ny++;
                months = LunarYear.FromYear( ny ).Months;
            }

            return months[ index + rest ];
        } else {
            int rest = -n;
            int ny = Year;
            int iy = ny;
            int im = Month;
            int index = 0;
            List<LunarMonth> months = LunarYear.FromYear( ny ).Months;
            while( true ) {
                int size = months.Count;
                for( int i = 0 ; i < size ; i++ ) {
                    LunarMonth m = months[ i ];
                    if( m.Year != iy || m.Month != im ) continue;
                    index = i;
                    break;
                }

                if( rest <= index ) {
                    break;
                }

                rest -= index;
                LunarMonth firstMonth = months[ 0 ];
                iy = firstMonth.Year;
                im = firstMonth.Month;
                ny--;
                months = LunarYear.FromYear( ny ).Months;
            }

            return months[ index - rest ];
        }
    }
}
