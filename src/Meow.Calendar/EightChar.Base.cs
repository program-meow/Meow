namespace Meow.Calendar;

/// <summary>
/// 八字
/// </summary>
public partial class EightChar {

    private int _sect = 2;
    /// <summary>
    /// 流派，2晚子时日柱按当天，1晚子时日柱按明天
    /// </summary>
    public int Sect {
        set => _sect = ( 1 == value ) ? 1 : 2;
        get => _sect;
    }

    /// <summary>
    /// 阴历
    /// </summary>
    internal readonly Lunar _lunar;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    public EightChar( DateTime date ) : this( new Lunar( date ) ) {
    }

    /// <summary>
    /// 从阴历初始化
    /// </summary>
    /// <param name="lunar">阴历</param>
    public EightChar( Lunar lunar ) {
        _lunar = lunar;
    }

    /// <summary>
    /// 年柱
    /// </summary>
    public string Year => _lunar.YearInGanZhiExact;

    /// <summary>
    /// 年天干
    /// </summary>
    public string YearGan => _lunar.YearGanExact;

    /// <summary>
    /// 年地支
    /// </summary>
    public string YearZhi => _lunar.YearZhiExact;

    /// <summary>
    /// 年藏干
    /// </summary>
    public List<string> YearHideGan => LunarUtil.ZHI_HIDE_GAN[ YearZhi ];

    /// <summary>
    /// 年五行
    /// </summary>
    public string YearWuXing => $"{LunarUtil.WU_XING_GAN[ YearGan ]}{LunarUtil.WU_XING_ZHI[ YearZhi ]}";

    /// <summary>
    /// 年纳音
    /// </summary>
    public string YearNaYin => LunarUtil.NAYIN[ Year ];

    /// <summary>
    /// 年天干十神
    /// </summary>
    public string YearShiShenGan => LunarUtil.SHI_SHEN_GAN[ $"{DayGan}{YearGan}" ];

    private List<string> GetShiShenZhi( string zhi ) {
        List<string> hideGan = LunarUtil.ZHI_HIDE_GAN[ zhi ];
        List<string> l = new List<string>( hideGan.Count );
        l.AddRange( hideGan.Select( gan => LunarUtil.SHI_SHEN_ZHI[ $"{DayGan}{gan}" ] ) );
        return l;
    }

    /// <summary>
    /// 年十神支
    /// </summary>
    public List<string> YearShiShenZhi => GetShiShenZhi( YearZhi );

    /// <summary>
    /// 日干序号
    /// </summary>
    public int DayGanIndex => ( 2 == Sect ) ? _lunar._dayGanIndexExact2 : _lunar._dayGanIndexExact;

    /// <summary>
    /// 日支序号
    /// </summary>
    public int DayZhiIndex => ( 2 == Sect ) ? _lunar._dayZhiIndexExact2 : _lunar._dayZhiIndexExact;

    private string GetDiShi( int zhiIndex ) {
        int index = EightCharUtil.CHANG_SHENG_OFFSET[ DayGan ] + ( DayGanIndex % 2 == 0 ? zhiIndex : -zhiIndex );
        if( index >= 12 ) {
            index -= 12;
        }
        if( index < 0 ) {
            index += 12;
        }
        return EightCharUtil.CHANG_SHENG[ index ];
    }

    /// <summary>
    /// 年地势
    /// </summary>
    public string YearDiShi => GetDiShi( _lunar._yearZhiIndexExact );

    /// <summary>
    /// 月柱
    /// </summary>
    public string Month => _lunar.MonthInGanZhiExact;

    /// <summary>
    /// 月干
    /// </summary>
    public string MonthGan => _lunar.MonthGanExact;

    /// <summary>
    /// 月支
    /// </summary>
    public string MonthZhi => _lunar.MonthZhiExact;

    /// <summary>
    /// 月藏干
    /// </summary>
    public List<string> MonthHideGan => LunarUtil.ZHI_HIDE_GAN[ MonthZhi ];

    /// <summary>
    /// 月五行
    /// </summary>
    public string MonthWuXing => $"{LunarUtil.WU_XING_GAN[ MonthGan ]}{LunarUtil.WU_XING_ZHI[ MonthZhi ]}";

    /// <summary>
    /// 月纳音
    /// </summary>
    public string MonthNaYin => LunarUtil.NAYIN[ Month ];

    /// <summary>
    /// 月十神干
    /// </summary>
    public string MonthShiShenGan => LunarUtil.SHI_SHEN_GAN[ $"{DayGan}{MonthGan}" ];

    /// <summary>
    /// 月十神支
    /// </summary>
    public List<string> MonthShiShenZhi => GetShiShenZhi( MonthZhi );

    /// <summary>
    /// 月地势
    /// </summary>
    public string MonthDiShi => GetDiShi( _lunar._monthZhiIndexExact );

    /// <summary>
    /// 日柱
    /// </summary>
    public string Day => ( 2 == Sect ) ? _lunar.DayInGanZhiExact2 : _lunar.DayInGanZhiExact;

    /// <summary>
    /// 日干
    /// </summary>
    public string DayGan => ( 2 == Sect ) ? _lunar.DayGanExact2 : _lunar.DayGanExact;

    /// <summary>
    /// 日支
    /// </summary>
    public string DayZhi => ( 2 == Sect ) ? _lunar.DayZhiExact2 : _lunar.DayZhiExact;

    /// <summary>
    /// 日支藏干
    /// </summary>
    public List<string> DayHideGan => LunarUtil.ZHI_HIDE_GAN[ DayZhi ];

    /// <summary>
    /// 日五行
    /// </summary>
    public string DayWuXing => $"{LunarUtil.WU_XING_GAN[ DayGan ]}{LunarUtil.WU_XING_ZHI[ DayZhi ]}";

    /// <summary>
    /// 日纳音
    /// </summary>
    public string DayNaYin => LunarUtil.NAYIN[ Day ];

    /// <summary>
    /// 日十神干
    /// </summary>
    public string DayShiShenGan => "日主";

    /// <summary>
    /// 日十神支
    /// </summary>
    public List<string> DayShiShenZhi => GetShiShenZhi( DayZhi );

    /// <summary>
    /// 日地势
    /// </summary>
    public string DayDiShi => GetDiShi( DayZhiIndex );

    /// <summary>
    /// 时柱
    /// </summary>
    public string Time => _lunar.TimeInGanZhi;

    /// <summary>
    /// 时干
    /// </summary>
    public string TimeGan => _lunar.TimeGan;

    /// <summary>
    /// 时支
    /// </summary>
    public string TimeZhi => _lunar.TimeZhi;

    /// <summary>
    /// 时支藏干
    /// </summary>
    public List<string> TimeHideGan => LunarUtil.ZHI_HIDE_GAN[ TimeZhi ];

    /// <summary>
    /// 时五行
    /// </summary>
    public string TimeWuXing => $"{LunarUtil.WU_XING_GAN[ _lunar.TimeGan ]}{LunarUtil.WU_XING_ZHI[ _lunar.TimeZhi ]}";

    /// <summary>
    /// 时纳音
    /// </summary>
    public string TimeNaYin => LunarUtil.NAYIN[ Time ];

    /// <summary>
    /// 时十神干
    /// </summary>
    public string TimeShiShenGan => LunarUtil.SHI_SHEN_GAN[ $"{DayGan}{TimeGan}" ];

    /// <summary>
    /// 时十神支
    /// </summary>
    public List<string> TimeShiShenZhi => GetShiShenZhi( TimeZhi );

    /// <summary>
    /// 时地势
    /// </summary>
    public string TimeDiShi => GetDiShi( _lunar._timeZhiIndex );

    /// <summary>
    /// 胎元
    /// </summary>
    public string TaiYuan {
        get {
            int ganIndex = _lunar._monthGanIndexExact + 1;
            if( ganIndex >= 10 ) {
                ganIndex -= 10;
            }
            int zhiIndex = _lunar._monthZhiIndexExact + 3;
            if( zhiIndex >= 12 ) {
                zhiIndex -= 12;
            }
            return $"{LunarUtil.GAN[ ganIndex + 1 ]}{LunarUtil.ZHI[ zhiIndex + 1 ]}";
        }
    }

    /// <summary>
    /// 胎元纳音
    /// </summary>
    public string TaiYuanNaYin => LunarUtil.NAYIN[ TaiYuan ];

    /// <summary>
    /// 胎息
    /// </summary>
    public string TaiXi {
        get {
            int ganIndex = 2 == Sect ? _lunar._dayGanIndexExact2 : _lunar._dayGanIndexExact;
            int zhiIndex = 2 == Sect ? _lunar._dayZhiIndexExact2 : _lunar._dayZhiIndexExact;
            return $"{LunarUtil.HE_GAN_5[ ganIndex ]}{LunarUtil.HE_ZHI_6[ zhiIndex ]}";
        }
    }

    /// <summary>
    /// 胎息纳音
    /// </summary>
    public string TaiXiNaYin => LunarUtil.NAYIN[ TaiXi ];

    /// <summary>
    /// 命宫
    /// </summary>
    public string MingGong {
        get {
            int monthZhiIndex = 0;
            int timeZhiIndex = 0;
            for( int i = 0, j = EightCharUtil.MONTH_ZHI.Length ; i < j ; i++ ) {
                string zhi = EightCharUtil.MONTH_ZHI[ i ];
                if( _lunar.MonthZhiExact.Equals( zhi ) ) {
                    monthZhiIndex = i;
                }
                if( _lunar.TimeZhi.Equals( zhi ) ) {
                    timeZhiIndex = i;
                }
            }
            int zhiIndex = 26 - ( monthZhiIndex + timeZhiIndex );
            if( zhiIndex > 12 ) {
                zhiIndex -= 12;
            }
            int jiaZiIndex = LunarUtil.GetJiaZiIndex( _lunar.MonthInGanZhiExact ) - ( monthZhiIndex - zhiIndex );
            if( jiaZiIndex >= 60 ) {
                jiaZiIndex -= 60;
            }
            if( jiaZiIndex < 0 ) {
                jiaZiIndex += 60;
            }
            return LunarUtil.JIA_ZI[ jiaZiIndex ];
        }
    }

    /// <summary>
    /// 命宫纳音
    /// </summary>
    public string MingGongNaYin => LunarUtil.NAYIN[ MingGong ];

    /// <summary>
    /// 身宫
    /// </summary>
    public string ShenGong {
        get {
            int monthZhiIndex = 0;
            int timeZhiIndex = 0;
            for( int i = 0, j = EightCharUtil.MONTH_ZHI.Length ; i < j ; i++ ) {
                string zhi = EightCharUtil.MONTH_ZHI[ i ];
                if( _lunar.MonthZhiExact.Equals( zhi ) ) {
                    monthZhiIndex = i;
                }
                if( _lunar.TimeZhi.Equals( zhi ) ) {
                    timeZhiIndex = i;
                }
            }
            int zhiIndex = 2 + monthZhiIndex + timeZhiIndex;
            if( zhiIndex > 12 ) {
                zhiIndex -= 12;
            }
            int jiaZiIndex = LunarUtil.GetJiaZiIndex( _lunar.MonthInGanZhiExact ) - ( monthZhiIndex - zhiIndex );
            if( jiaZiIndex >= 60 ) {
                jiaZiIndex -= 60;
            }
            if( jiaZiIndex < 0 ) {
                jiaZiIndex += 60;
            }
            return LunarUtil.JIA_ZI[ jiaZiIndex ];
        }
    }

    /// <summary>
    /// 身宫纳音
    /// </summary>
    public string ShenGongNaYin => LunarUtil.NAYIN[ ShenGong ];

    /// <summary>
    /// 年柱所在旬
    /// </summary>
    public string YearXun => _lunar.YearXunExact;

    /// <summary>
    /// 年柱旬空(空亡)
    /// </summary>
    public string YearXunKong => _lunar.YearXunKongExact;

    /// <summary>
    /// 月柱所在旬
    /// </summary>
    public string MonthXun => _lunar.MonthXunExact;

    /// <summary>
    /// 月柱旬空(空亡)
    /// </summary>
    public string MonthXunKong => _lunar.MonthXunKongExact;

    /// <summary>
    /// 日柱所在旬
    /// </summary>
    public string DayXun => ( 2 == Sect ) ? _lunar.DayXunExact2 : _lunar.DayXunExact;

    /// <summary>
    /// 日柱旬空(空亡)
    /// </summary>
    public string DayXunKong => ( 2 == Sect ) ? _lunar.DayXunKongExact2 : _lunar.DayXunKongExact;

    /// <summary>
    /// 时柱所在旬
    /// </summary>
    public string TimeXun => _lunar.TimeXun;

    /// <summary>
    /// 时柱旬空(空亡)
    /// </summary>
    public string TimeXunKong => _lunar.TimeXunKong;

}
