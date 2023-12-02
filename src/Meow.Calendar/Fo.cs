namespace Meow.Calendar;

/// <summary>
/// 佛历
/// </summary>
public partial class Fo {

    #region 初始化

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>道历</returns>
    public static Fo FromDate( DateTime date ) {
        return new Fo( date );
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="lunar">阴历</param>
    /// <returns>佛历</returns>
    public static Fo FromLunar( Lunar lunar ) {
        return new Fo( lunar );
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="lunarYear">阴历年</param>
    /// <param name="lunarMonth">阴历月</param>
    /// <param name="lunarDay">阴历日</param>
    /// <param name="hour">小时</param>
    /// <param name="minute">分钟</param>
    /// <param name="second">秒钟</param>
    /// <returns>佛历</returns>
    public static Fo FromYmdHms( int lunarYear , int lunarMonth , int lunarDay , int hour = 0 , int minute = 0 , int second = 0 ) {
        return FromLunar( Lunar.FromYmdHms( lunarYear + FoUtil.DEAD_YEAR - 1 , lunarMonth , lunarDay , hour , minute , second ) );
    }

    #endregion

    /// <summary>
    /// 宿
    /// </summary>
    public string Xiu => FoUtil.GetXiu( Month , Day );

    /// <summary>
    /// 宿吉凶
    /// </summary>
    public string XiuLuck => LunarUtil.XIU_LUCK[ Xiu ];

    /// <summary>
    /// 宿歌诀
    /// </summary>
    public string XiuSong => LunarUtil.XIU_SONG[ Xiu ];

    /// <summary>
    /// 政
    /// </summary>
    public string Zheng => LunarUtil.ZHENG[ Xiu ];

    /// <summary>
    /// 动物
    /// </summary>
    public string Animal => LunarUtil.ANIMAL[ Xiu ];

    /// <summary>
    /// 宫
    /// </summary>
    public string Gong => LunarUtil.GONG[ Xiu ];

    /// <summary>
    /// 兽
    /// </summary>
    public string Shou => LunarUtil.SHOU[ Gong ];

    #region 输出

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString {
        get {
            var s = new StringBuilder();
            s.Append( ToString() );
            foreach( var f in Festivals ) {
                s.Append( " (" );
                s.Append( f );
                s.Append( ')' );
            }
            return s.ToString();
        }
    }

    /// <inheritdoc />
    public override string ToString() {
        return $"{YearInChinese}年{MonthInChinese}月{DayInChinese}";
    }

    #endregion

}
