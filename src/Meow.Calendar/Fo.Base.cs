namespace Meow.Calendar;

/// <summary>
/// 佛历
/// </summary>
public partial class Fo {

    /// <summary>
    /// 阴历
    /// </summary>
    private readonly Lunar _lunar;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    public Fo( DateTime date ) : this( new Lunar( date ) ) {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="lunar">阴历</param>
    public Fo( Lunar lunar ) {
        _lunar = lunar;
    }

    /// <summary>
    /// 年
    /// </summary>
    public int Year {
        get {
            int sy = _lunar.ToSolar().Year;
            int y = sy - FoUtil.DEAD_YEAR;
            if( sy == _lunar.Year ) {
                y++;
            }
            return y;
        }
    }

    /// <summary>
    /// 月
    /// </summary>
    public int Month => _lunar.Month;

    /// <summary>
    /// 日
    /// </summary>
    public int Day => _lunar.Day;

    /// <summary>
    /// 中文年
    /// </summary>
    public string YearInChinese {
        get {
            char[] y = Year.ToString().ToCharArray();
            StringBuilder s = new StringBuilder();
            for( int i = 0, j = y.Length ; i < j ; i++ ) {
                s.Append( LunarUtil.NUMBER[ y[ i ] - '0' ] );
            }
            return s.ToString();
        }
    }

    /// <summary>
    /// 中文月
    /// </summary>
    public string MonthInChinese => _lunar.MonthInChinese;

    /// <summary>
    /// 中文日
    /// </summary>
    public string DayInChinese => _lunar.DayInChinese;

    /// <summary>
    /// 因果犯忌
    /// </summary>
    public List<FoFestival> Festivals {
        get {
            List<FoFestival> l = new List<FoFestival>();
            try {
                l.AddRange( FoUtil.FESTIVAL[ SystemMath.Abs( Month ) + "-" + Day ] );
            } catch {
                // ignored
            }

            return l;
        }
    }

    /// <summary>
    /// 纪念日
    /// </summary>
    public List<string> OtherFestivals {
        get {
            List<string> l = new List<string>();
            try {
                List<string> fs = FoUtil.OTHER_FESTIVAL[ $"{Month}-{Day}" ];
                l.AddRange( fs );
            } catch {
                // ignored
            }
            return l;
        }
    }

    /// <summary>
    /// 月斋
    /// </summary>
    public bool MonthZhai => Month == 1 || Month == 5 || Month == 9;

    /// <summary>
    /// 杨公忌日
    /// </summary>
    public bool DayYangGong {
        get {
            return Festivals.Any( f => "杨公忌".Equals( f.Name ) );
        }
    }

    /// <summary>
    /// 朔望斋日
    /// </summary>
    public bool DayZhaiShuoWang => Day == 1 || Day == 15;

    /// <summary>
    /// 六斋日
    /// </summary>
    public bool DayZhaiSix {
        get {
            switch( Day ) {
                case 8:
                case 14:
                case 15:
                case 23:
                case 29:
                case 30:
                    return true;
                case 28: {
                    LunarMonth m = LunarMonth.FromYm( _lunar.Year , Month );
                    return null != m && 30 != m.DayCount;
                }
                default:
                    return false;
            }
        }
    }

    /// <summary>
    /// 十斋日
    /// </summary>
    public bool DayZhaiTen => Day == 1 || Day == 8 || Day == 14 || Day == 15 || Day == 18 || Day == 23 || Day == 24 || Day == 28 || Day == 29 || Day == 30;

    /// <summary>
    /// 观音斋日
    /// </summary>
    public bool DayZhaiGuanYin {
        get {
            string k = $"{Month}-{Day}";
            return FoUtil.DAY_ZHAI_GUAN_YIN.Any( d => k.Equals( d ) );
        }
    }

}
