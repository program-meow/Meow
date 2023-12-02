namespace Meow.Calendar;

/// <summary>
/// 道历
/// </summary>
public partial class Dao {

    #region 初始化

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>道历</returns>
    public static Dao FromDate( DateTime date ) {
        return new Dao( date );
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="lunar">阴历</param>
    /// <returns>道历</returns>
    public static Dao FromLunar( Lunar lunar ) {
        return new Dao( lunar );
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="year">阴历年</param>
    /// <param name="month">阴历月</param>
    /// <param name="day">阴历日</param>
    /// <param name="hour">小时</param>
    /// <param name="minute">分钟</param>
    /// <param name="second">秒钟</param>
    /// <returns>道历</returns>
    public static Dao FromYmdHms( int year , int month , int day , int hour = 0 , int minute = 0 , int second = 0 ) {
        return FromLunar( Lunar.FromYmdHms( year + DaoUtil.BIRTH_YEAR , month , day , hour , minute , second ) );
    }

    #endregion

    #region 判断

    private bool IsDayIn( string[] days ) {
        var md = Month + "-" + Day;
        return days.Any( d => md.Equals( d ) );
    }

    /// <summary>
    /// 是否三会日
    /// </summary>
    public bool DaySanHui => IsDayIn( DaoUtil.SAN_HUI );

    /// <summary>
    /// 是否三元日
    /// </summary>
    public bool DaySanYuan => IsDayIn( DaoUtil.SAN_YUAN );

    /// <summary>
    /// 是否五腊日
    /// </summary>
    public bool DayWuLa => IsDayIn( DaoUtil.WU_LA );

    /// <summary>
    /// 是否八节日
    /// </summary>
    public bool DayBaJie => DaoUtil.BA_JIE.ContainsKey( _lunar.JieQi );

    /// <summary>
    /// 是否八会日
    /// </summary>
    public bool DayBaHui => DaoUtil.BA_HUI.ContainsKey( _lunar.DayInGanZhi );

    /// <summary>
    /// 是否明戊日
    /// </summary>
    public bool DayMingWu => "戊".Equals( _lunar.DayGan );

    /// <summary>
    /// 是否暗戊日
    /// </summary>
    public bool DayAnWu => _lunar.DayZhi.Equals( DaoUtil.AN_WU[ SystemMath.Abs( Month ) - 1 ] );

    /// <summary>
    /// 是否戊日
    /// </summary>
    public bool DayWu => DayMingWu || DayAnWu;

    /// <summary>
    /// 是否天赦日
    /// </summary>
    public bool DayTianShe {
        get {
            var ret = false;
            var mz = _lunar.MonthZhi;
            var dgz = _lunar.DayInGanZhi;
            if( "寅卯辰".Contains( mz ) ) {
                if( "戊寅".Equals( dgz ) ) {
                    ret = true;
                }
            } else if( "巳午未".Contains( mz ) ) {
                if( "甲午".Equals( dgz ) ) {
                    ret = true;
                }
            } else if( "申酉戌".Contains( mz ) ) {
                if( "戊申".Equals( dgz ) ) {
                    ret = true;
                }
            } else if( "亥子丑".Contains( mz ) ) {
                if( "甲子".Equals( dgz ) ) {
                    ret = true;
                }
            }

            return ret;
        }
    }

    #endregion

    #region 输出

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString => "道歷" + YearInChinese + "年，天運" + _lunar.YearInGanZhi + "年，" + _lunar.MonthInGanZhi + "月，" + _lunar.DayInGanZhi + "日。" + MonthInChinese + "月" + DayInChinese + "日，" + _lunar.TimeZhi + "時。";

    /// <inheritdoc />
    public override string ToString() {
        return YearInChinese + "年" + MonthInChinese + "月" + DayInChinese;
    }

    #endregion
}
