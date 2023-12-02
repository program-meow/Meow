namespace Meow.Calendar;

/// <summary>
/// 农历日期
/// </summary>
public partial class Lunar {

    #region 私有属性

    /// <summary>
    /// 对应阳历
    /// </summary>
    internal readonly Solar _solar;

    #endregion

    #region 构造函数

    /// <summary>
    /// 默认使用当前日期初始化
    /// </summary>
    public Lunar() : this( DateTime.Now ) {
    }

    /// <summary>
    /// 通过农历年月日时分秒初始化
    /// </summary>
    /// <param name="lunarYear">年（农历）</param>
    /// <param name="lunarMonth">月（农历），1到12，闰月为负，即闰2月=-2</param>
    /// <param name="lunarDay">日（农历），1到31</param>
    /// <param name="hour">小时（阳历）</param>
    /// <param name="minute">分钟（阳历）</param>
    /// <param name="second">秒钟（阳历）</param>
    public Lunar( int lunarYear , int lunarMonth , int lunarDay , int hour = 0 , int minute = 0 , int second = 0 ) {
        var y = LunarYear.FromYear( lunarYear );
        var m = y.GetMonth( lunarMonth );
        if( null == m ) {
            throw new ArgumentException( $"wrong lunar year {lunarYear} month {lunarMonth}" );
        }
        if( lunarDay < 1 ) {
            throw new ArgumentException( "lunar day must bigger than 0" );
        }
        var days = m.DayCount;
        if( lunarDay > days ) {
            throw new ArgumentException( $"only {days} days in lunar year {lunarYear} month {lunarMonth}" );
        }
        Year = lunarYear;
        Month = lunarMonth;
        Day = lunarDay;
        Hour = hour;
        Minute = minute;
        Second = second;
        var noon = Solar.FromJulianDay( m.FirstJulianDay + lunarDay - 1 );
        _solar = Solar.FromYmdHms( noon.Year , noon.Month , noon.Day , hour , minute , second );
        if( noon.Year != lunarYear ) {
            y = LunarYear.FromYear( noon.Year );
        }
        Compute( y );
    }

    /// <summary>
    /// 通过阳历初始化
    /// </summary>
    /// <param name="solar">阳历</param>
    public Lunar( Solar solar ) {
        var ly = LunarYear.FromYear( solar.Year );
        foreach( var m in ly.Months ) {
            var days = solar.Subtract( Solar.FromJulianDay( m.FirstJulianDay ) );
            if( days < m.DayCount ) {
                Year = m.Year;
                Month = m.Month;
                Day = days + 1;
                break;
            }
        }
        Hour = solar.Hour;
        Minute = solar.Minute;
        Second = solar.Second;
        _solar = solar;
        Compute( ly );
    }

    /// <summary>
    /// 通过阳历日期初始化
    /// </summary>
    /// <param name="date">阳历日期</param>
    public Lunar( DateTime date ) : this( Solar.FromDate( date ) ) {
    }

    #endregion

    #region 计算

    /// <summary>
    /// 计算
    /// </summary>
    /// <param name="lunarYear">阴历年</param>
    private void Compute( LunarYear lunarYear ) {
        ComputeJieQi( lunarYear );
        ComputeYear();
        ComputeMonth();
        ComputeDay();
        ComputeTime();
        ComputeWeek();
    }

    #endregion

    #region 属性

    /// <summary>
    /// 农历年
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// 农历月，闰月为负，即闰2月=-2
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// 农历日
    /// </summary>
    public int Day { get; }

    /// <summary>
    /// 阳历小时
    /// </summary>
    public int Hour { get; }

    /// <summary>
    /// 阳历分钟
    /// </summary>
    public int Minute { get; }

    /// <summary>
    /// 阳历秒钟
    /// </summary>
    public int Second { get; }

    /// <summary>
    /// 农历季节
    /// </summary>
    public string Season => LunarUtil.SEASON[ SystemMath.Abs( Month ) ];

    #endregion

    #region 扩展属性

    #region 节气

    #region 属性

    /// <summary>
    /// 节气表
    /// </summary>
    public Dictionary<string , Solar> JieQiTable { get; } = new Dictionary<string , Solar>();

    #endregion

    #region 方法

    /// <summary>
    /// 计算节气表
    /// </summary>
    private void ComputeJieQi( LunarYear lunarYear ) {
        var julianDays = lunarYear.JieQiJulianDays;
        for( int i = 0, j = LunarUtil.JIE_QI_IN_USE.Length ; i < j ; i++ ) {
            JieQiTable.Add( LunarUtil.JIE_QI_IN_USE[ i ] , Solar.FromJulianDay( julianDays[ i ] ) );
        }
    }

    #endregion

    #endregion

    #region 天干地支

    #region 干支纪年

    #region 属性 - 国标，以正月初一为起点

    #region 天干

    #region 下标

    /// <summary>
    /// 年对应的天干下标（国标，以正月初一为起点），0-9
    /// </summary>
    private int _yearGanIndex;

    #endregion

    /// <summary>
    /// 年天干（以正月初一作为新年的开始），如辛
    /// </summary>
    public string YearGan => LunarUtil.GAN[ _yearGanIndex + 1 ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 年对应的地支下标（国标，以正月初一为起点），0-11
    /// </summary>
    private int _yearZhiIndex;

    #endregion

    /// <summary>
    /// 年地支（以正月初一作为新年的开始），如亥
    /// </summary>
    public string YearZhi => LunarUtil.ZHI[ _yearZhiIndex + 1 ];

    /// <summary>
    /// 年生肖（以正月初一起算），如虎
    /// </summary>
    public string YearShengXiao => LunarUtil.SHENGXIAO[ _yearZhiIndex + 1 ];

    #endregion

    /// <summary>
    /// 干支纪年（年柱）（以正月初一作为新年的开始），如辛亥
    /// </summary>
    public string YearInGanZhi => $"{YearGan}{YearZhi}";

    /// <summary>
    /// 年纳音，如剑锋金
    /// </summary>
    public string YearNaYin => LunarUtil.NAYIN[ YearInGanZhi ];

    /// <summary>
    /// 年所在旬（以正月初一作为新年的开始）
    /// </summary>
    public string YearXun => LunarUtil.GetXun( YearInGanZhi );

    /// <summary>
    /// 值年空亡(旬空)（以正月初一作为新年的开始）
    /// </summary>
    public string YearXunKong => LunarUtil.GetXunKong( YearInGanZhi );

    #endregion

    #region 属性 - 月计算用，以立春为起点

    #region 天干

    #region 下标

    /// <summary>
    /// 年对应的天干下标（月干计算用，以立春为起点），0-9
    /// </summary>
    private int _yearGanIndexByLiChun;

    #endregion

    /// <summary>
    /// 年天干（以立春当天作为新年的开始），如辛
    /// </summary>
    public string YearGanByLiChun => LunarUtil.GAN[ _yearGanIndexByLiChun + 1 ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 年对应的地支下标（月支计算用，以立春为起点），0-11
    /// </summary>
    private int _yearZhiIndexByLiChun;

    #endregion

    /// <summary>
    /// 年地支（以立春当天作为新年的开始），如亥
    /// </summary>
    public string YearZhiByLiChun => LunarUtil.ZHI[ _yearZhiIndexByLiChun + 1 ];

    /// <summary>
    /// 年生肖（以立春当天起算），如虎
    /// </summary>
    public string YearShengXiaoByLiChun => LunarUtil.SHENGXIAO[ _yearZhiIndexByLiChun + 1 ];

    #endregion

    /// <summary>
    /// 干支纪年（年柱）（以立春当天作为新年的开始），如辛亥
    /// </summary>
    public string YearInGanZhiByLiChun => $"{YearGanByLiChun}{YearZhiByLiChun}";

    /// <summary>
    /// 年所在旬（以立春当天作为新年的开始）
    /// </summary>
    public string YearXunByLiChun => LunarUtil.GetXun( YearInGanZhiByLiChun );

    /// <summary>
    /// 值年空亡(旬空)（以立春当天作为新年的开始）
    /// </summary>
    public string YearXunKongByLiChun => LunarUtil.GetXunKong( YearInGanZhiByLiChun );

    #endregion

    #region 属性 - 最精确的，供八字用，以立春交接时刻为起点

    #region 天干

    #region 下标

    /// <summary>
    /// 年对应的天干下标（最精确的，供八字用，以立春交接时刻为起点），0-9
    /// </summary>
    internal int _yearGanIndexExact;

    #endregion

    /// <summary>
    /// 最精确的年天干（以立春交接的时刻作为新年的开始），如辛
    /// </summary>
    public string YearGanExact => LunarUtil.GAN[ _yearGanIndexExact + 1 ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 年对应的地支下标（最精确的，供八字用，以立春交接时刻为起点），0-11
    /// </summary>
    internal int _yearZhiIndexExact;

    #endregion

    /// <summary>
    /// 最精确的年地支（以立春交接的时刻作为新年的开始），如亥
    /// </summary>
    public string YearZhiExact => LunarUtil.ZHI[ _yearZhiIndexExact + 1 ];

    /// <summary>
    /// 精确的年生肖（以立春交接时刻起算），如虎
    /// </summary>
    public string YearShengXiaoExact => LunarUtil.SHENGXIAO[ _yearZhiIndexExact + 1 ];

    #endregion

    /// <summary>
    /// 干支纪年（年柱）（以立春交接的时刻作为新年的开始），如辛亥
    /// </summary>
    public string YearInGanZhiExact => $"{YearGanExact}{YearZhiExact}";

    /// <summary>
    /// 年所在旬（以立春交接时刻作为新年的开始）
    /// </summary>
    public string YearXunExact => LunarUtil.GetXun( YearInGanZhiExact );

    /// <summary>
    /// 值年空亡(旬空)（以立春交接时刻作为新年的开始）
    /// </summary>
    public string YearXunKongExact => LunarUtil.GetXunKong( YearInGanZhiExact );

    #endregion

    #region 方法

    /// <summary>
    /// 计算干支纪年
    /// </summary>
    private void ComputeYear() {
        // 以正月初一开始
        var offset = Year - 4;
        _yearGanIndex = offset % 10;
        _yearZhiIndex = offset % 12;

        if( _yearGanIndex < 0 ) {
            _yearGanIndex += 10;
        }

        if( _yearZhiIndex < 0 ) {
            _yearZhiIndex += 12;
        }

        // 以立春作为新一年的开始的干支纪年
        var g = _yearGanIndex;
        var z = _yearZhiIndex;

        // 精确的干支纪年，以立春交接时刻为准
        var gExact = _yearGanIndex;
        var zExact = _yearZhiIndex;

        var solarYear = _solar.Year;
        var solarYmd = _solar.Ymd;
        var solarYmdHms = _solar.YmdHms;

        // 获取立春的阳历时刻
        var liChun = JieQiTable[ "立春" ];
        if( liChun.Year != solarYear ) {
            liChun = JieQiTable[ "LI_CHUN" ];
        }
        var liChunYmd = liChun.Ymd;
        var liChunYmdHms = liChun.YmdHms;

        // 阳历和阴历年份相同代表正月初一及以后
        if( Year == solarYear ) {
            // 立春日期判断
            if( string.Compare( solarYmd , liChunYmd , StringComparison.Ordinal ) < 0 ) {
                g--;
                z--;
            }
            // 立春交接时刻判断
            if( string.Compare( solarYmdHms , liChunYmdHms , StringComparison.Ordinal ) < 0 ) {
                gExact--;
                zExact--;
            }
        } else if( Year < solarYear ) {
            if( string.Compare( solarYmd , liChunYmd , StringComparison.Ordinal ) >= 0 ) {
                g++;
                z++;
            }
            if( string.Compare( solarYmdHms , liChunYmdHms , StringComparison.Ordinal ) >= 0 ) {
                gExact++;
                zExact++;
            }
        }

        _yearGanIndexByLiChun = ( g < 0 ? g + 10 : g ) % 10;
        _yearZhiIndexByLiChun = ( z < 0 ? z + 12 : z ) % 12;

        _yearGanIndexExact = ( gExact < 0 ? gExact + 10 : gExact ) % 10;
        _yearZhiIndexExact = ( zExact < 0 ? zExact + 12 : zExact ) % 12;
    }

    #endregion

    #endregion

    #region 干支纪月

    #region 属性 - 默认

    #region 天干

    #region 下标

    /// <summary>
    /// 月对应的天干下标（以节交接当天起算），0-9
    /// </summary>
    internal int _monthGanIndex;

    #endregion

    /// <summary>
    /// 月天干，如己
    /// </summary>
    public string MonthGan => LunarUtil.GAN[ _monthGanIndex + 1 ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 月对应的地支下标（以节交接当天起算），0-11
    /// </summary>
    private int _monthZhiIndex;

    #endregion

    /// <summary>
    /// 月地支，如卯
    /// </summary>
    public string MonthZhi => LunarUtil.ZHI[ _monthZhiIndex + 1 ];

    /// <summary>
    /// 月生肖，如虎
    /// </summary>
    public string MonthShengXiao => LunarUtil.SHENGXIAO[ _monthZhiIndex + 1 ];

    #endregion

    /// <summary>
    /// 干支纪月（月柱）（以节交接当天起算），如己卯，月天干口诀：甲己丙寅首，乙庚戊寅头。丙辛从庚寅，丁壬壬寅求，戊癸甲寅居，周而复始流。月地支：正月起寅。
    /// </summary>
    public string MonthInGanZhi => $"{MonthGan}{MonthZhi}";

    /// <summary>
    /// 月纳音，如剑锋金
    /// </summary>
    public string MonthNaYin => LunarUtil.NAYIN[ MonthInGanZhi ];

    /// <summary>
    /// 月所在旬（以节交接当天起算）
    /// </summary>
    public string MonthXun => LunarUtil.GetXun( MonthInGanZhi );

    /// <summary>
    /// 值月空亡(旬空)（以节交接当天起算）
    /// </summary>
    public string MonthXunKong => LunarUtil.GetXunKong( MonthInGanZhi );

    /// <summary>
    /// 逐月胎神方位，闰月无
    /// </summary>
    public string MonthPositionTai => Month < 0 ? "" : LunarUtil.POSITION_TAI_MONTH[ Month - 1 ];

    /// <summary>
    /// 月相
    /// </summary>
    public string YueXiang => LunarUtil.YUE_XIANG[ Day ];

    #endregion

    #region 属性 - 八字流派1

    #region 天干

    #region 下标

    /// <summary>
    /// 月对应的天干下标（八字流派1，晚子时日柱算明天），0-9
    /// </summary>
    internal int _monthGanIndexExact;

    #endregion

    /// <summary>
    /// 精确的月天干（以节交接时刻起算），如己
    /// </summary>
    public string MonthGanExact => LunarUtil.GAN[ _monthGanIndexExact + 1 ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 月对应的地支下标（八字流派1，晚子时日柱算明天），0-11
    /// </summary>
    internal int _monthZhiIndexExact;

    #endregion

    /// <summary>
    /// 精确的月地支（以节交接时刻起算），如卯
    /// </summary>
    public string MonthZhiExact => LunarUtil.ZHI[ _monthZhiIndexExact + 1 ];

    #endregion

    /// <summary>
    /// 精确的干支纪月（月柱）（以节交接时刻起算），如己卯，月天干口诀：甲己丙寅首，乙庚戊寅头。丙辛从庚寅，丁壬壬寅求，戊癸甲寅居，周而复始流。月地支：正月起寅。
    /// </summary>
    public string MonthInGanZhiExact => $"{MonthGanExact}{MonthZhiExact}";

    /// <summary>
    /// 月所在旬（以节交接时刻起算）
    /// </summary>
    public string MonthXunExact => LunarUtil.GetXun( MonthInGanZhiExact );

    /// <summary>
    /// 值月空亡(旬空)（以节交接时刻起算）
    /// </summary>
    public string MonthXunKongExact => LunarUtil.GetXunKong( MonthInGanZhiExact );

    #endregion

    #region 方法

    /// <summary>
    /// 干支纪月计算
    /// </summary>
    private void ComputeMonth() {
        Solar start = null;
        Solar end;
        var ymd = _solar.Ymd;
        var time = _solar.YmdHms;
        var size = LunarUtil.JIE_QI_IN_USE.Length;

        // 序号：大雪以前-3，大雪到小寒之间-2，小寒到立春之间-1，立春之后0
        var index = -3;
        for( var i = 0 ; i < size ; i += 2 ) {
            end = JieQiTable[ LunarUtil.JIE_QI_IN_USE[ i ] ];
            var symd = null == start ? ymd : start.Ymd;
            if( string.Compare( ymd , symd , StringComparison.Ordinal ) >= 0 && string.Compare( ymd , end.Ymd , StringComparison.Ordinal ) < 0 ) {
                break;
            }
            start = end;
            index++;
        }

        //干偏移值（以立春当天起算）
        var offset = ( ( ( _yearGanIndexByLiChun + ( index < 0 ? 1 : 0 ) ) % 5 + 1 ) * 2 ) % 10;
        _monthGanIndex = ( ( index < 0 ? index + 10 : index ) + offset ) % 10;
        _monthZhiIndex = ( ( index < 0 ? index + 12 : index ) + LunarUtil.BASE_MONTH_ZHI_INDEX ) % 12;

        start = null;
        index = -3;
        for( var i = 0 ; i < size ; i += 2 ) {
            end = JieQiTable[ LunarUtil.JIE_QI_IN_USE[ i ] ];
            var stime = null == start ? time : start.YmdHms;
            if( string.Compare( time , stime , StringComparison.Ordinal ) >= 0 && string.Compare( time , end.YmdHms , StringComparison.Ordinal ) < 0 ) {
                break;
            }
            start = end;
            index++;
        }

        //干偏移值（以立春交接时刻起算）
        offset = ( ( ( _yearGanIndexExact + ( index < 0 ? 1 : 0 ) ) % 5 + 1 ) * 2 ) % 10;
        _monthGanIndexExact = ( ( index < 0 ? index + 10 : index ) + offset ) % 10;
        _monthZhiIndexExact = ( ( index < 0 ? index + 12 : index ) + LunarUtil.BASE_MONTH_ZHI_INDEX ) % 12;
    }

    #endregion

    #endregion

    #region 干支纪日

    #region 属性 - 默认

    #region 天干

    #region 下标

    /// <summary>
    /// 日对应的天干下标，0-9
    /// </summary>
    internal int _dayGanIndex;

    #endregion

    /// <summary>
    /// 日天干，如甲
    /// </summary>
    public string DayGan => LunarUtil.GAN[ _dayGanIndex + 1 ];

    /// <summary>
    /// 彭祖百忌天干
    /// </summary>
    public string PengZuGan => LunarUtil.PENGZU_GAN[ _dayGanIndex + 1 ];

    /// <summary>
    /// 日喜神方位，如艮
    /// </summary>
    public string DayPositionXi => LunarUtil.POSITION_XI[ _dayGanIndex + 1 ];

    /// <summary>
    /// 日喜神方位描述，如东北
    /// </summary>
    public string DayPositionXiDesc => LunarUtil.POSITION_DESC[ DayPositionXi ];

    /// <summary>
    /// 日阳贵神方位，如艮
    /// </summary>
    public string DayPositionYangGui => LunarUtil.POSITION_YANG_GUI[ _dayGanIndex + 1 ];

    /// <summary>
    /// 日阳贵神方位描述，如东北
    /// </summary>
    public string DayPositionYangGuiDesc => LunarUtil.POSITION_DESC[ DayPositionYangGui ];

    /// <summary>
    /// 日阴贵神方位，如艮
    /// </summary>
    public string DayPositionYinGui => LunarUtil.POSITION_YIN_GUI[ _dayGanIndex + 1 ];

    /// <summary>
    /// 日阴贵神方位描述，如东北
    /// </summary>
    public string DayPositionYinGuiDesc => LunarUtil.POSITION_DESC[ DayPositionYinGui ];

    /// <summary>
    /// 日财神方位，如艮
    /// </summary>
    public string DayPositionCai => LunarUtil.POSITION_CAI[ _dayGanIndex + 1 ];

    /// <summary>
    /// 日财神方位描述，如东北
    /// </summary>
    public string DayPositionCaiDesc => LunarUtil.POSITION_DESC[ DayPositionCai ];

    /// <summary>
    /// 无情之克的日冲天干，如甲
    /// </summary>
    public string DayChongGan => LunarUtil.CHONG_GAN[ _dayGanIndex ];

    /// <summary>
    /// 有情之克的日冲天干，如甲
    /// </summary>
    public string DayChongGanTie => LunarUtil.CHONG_GAN_TIE[ _dayGanIndex ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 日对应的地支下标，0-11
    /// </summary>
    internal int _dayZhiIndex;

    #endregion

    /// <summary>
    /// 日地支，如卯
    /// </summary>
    public string DayZhi => LunarUtil.ZHI[ _dayZhiIndex + 1 ];

    /// <summary>
    /// 日煞，如北
    /// </summary>
    public string DaySha => LunarUtil.SHA[ DayZhi ];

    /// <summary>
    /// 日生肖，如虎
    /// </summary>
    public string DayShengXiao => LunarUtil.SHENGXIAO[ _dayZhiIndex + 1 ];

    /// <summary>
    /// 彭祖百忌地支
    /// </summary>
    public string PengZuZhi => LunarUtil.PENGZU_ZHI[ _dayZhiIndex + 1 ];

    /// <summary>
    /// 日冲，如申
    /// </summary>
    public string DayChong => LunarUtil.CHONG[ _dayZhiIndex ];

    /// <summary>
    /// 日冲生肖，如猴
    /// </summary>
    public string DayChongShengXiao {
        get {
            for( int i = 0, j = LunarUtil.ZHI.Length ; i < j ; i++ ) {
                if( LunarUtil.ZHI[ i ].Equals( DayChong ) ) {
                    return LunarUtil.SHENGXIAO[ i ];
                }
            }
            return "";
        }
    }

    /// <summary>
    /// 日冲描述，如(壬申)猴
    /// </summary>
    public string DayChongDesc => $"({DayChongGan}{DayChong}){DayChongShengXiao}";

    /// <summary>
    /// 值日天神
    /// </summary>
    public string DayTianShen => LunarUtil.TIAN_SHEN[ ( _dayZhiIndex + LunarUtil.ZHI_TIAN_SHEN_OFFSET[ MonthZhi ] ) % 12 + 1 ];

    /// <summary>
    /// 值日天神类型：黄道/黑道
    /// </summary>
    public string DayTianShenType => LunarUtil.TIAN_SHEN_TYPE[ DayTianShen ];

    /// <summary>
    /// 值日天神吉凶：吉/凶
    /// </summary>
    public string DayTianShenLuck => LunarUtil.TIAN_SHEN_TYPE_LUCK[ DayTianShenType ];

    /// <summary>
    /// 十二执星：建、除、满、平、定、执、破、危、成、收、开、闭。当月支与日支相同即为建，依次类推
    /// </summary>
    public string ZhiXing {
        get {
            var offset = _dayZhiIndex - _monthZhiIndex;
            if( offset < 0 ) {
                offset += 12;
            }
            return LunarUtil.ZHI_XING[ offset + 1 ];
        }
    }

    #endregion

    /// <summary>
    /// 干支纪日（日柱），如己卯
    /// </summary>
    public string DayInGanZhi => $"{DayGan}{DayZhi}";

    /// <summary>
    /// 日纳音，如剑锋金
    /// </summary>
    public string DayNaYin => LunarUtil.NAYIN[ DayInGanZhi ];

    /// <summary>
    /// 日所在旬（以节交接当天起算）
    /// </summary>
    public string DayXun => LunarUtil.GetXun( DayInGanZhi );

    /// <summary>
    /// 值日空亡(旬空)
    /// </summary>
    public string DayXunKong => LunarUtil.GetXunKong( DayInGanZhi );

    /// <summary>
    /// 逐日胎神方位
    /// </summary>
    public string DayPositionTai => LunarUtil.POSITION_TAI_DAY[ LunarUtil.GetJiaZiIndex( DayInGanZhi ) ];

    /// <summary>
    /// 日宜，如果没有，返回["无"]
    /// </summary>
    public List<string> DayYi => LunarUtil.GetDayYi( MonthInGanZhiExact , DayInGanZhi );

    /// <summary>
    /// 日忌，如果没有，返回["无"]
    /// </summary>
    public List<string> DayJi => LunarUtil.GetDayJi( MonthInGanZhiExact , DayInGanZhi );

    /// <summary>
    /// 日吉神（宜趋），如果没有，返回["无"]
    /// </summary>
    public List<string> DayJiShen => LunarUtil.GetDayJiShen( Month , DayInGanZhi );

    /// <summary>
    /// 日凶煞（宜忌），如果没有，返回["无"]
    /// </summary>
    public List<string> DayXiongSha => LunarUtil.GetDayXiongSha( Month , DayInGanZhi );

    #endregion

    #region 属性 - 八字流派1

    #region 天干

    #region 下标

    /// <summary>
    /// 日对应的天干下标（八字流派1，晚子时日柱算明天），0-9
    /// </summary>
    internal int _dayGanIndexExact;

    #endregion

    /// <summary>
    /// 日天干（八字流派1，晚子时日柱算明天），如甲
    /// </summary>
    public string DayGanExact => LunarUtil.GAN[ _dayGanIndexExact + 1 ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 日对应的地支下标（八字流派1，晚子时日柱算明天），0-11
    /// </summary>
    internal int _dayZhiIndexExact;

    #endregion

    /// <summary>
    /// 日地支（八字流派1，晚子时日柱算明天），如卯
    /// </summary>
    public string DayZhiExact => LunarUtil.ZHI[ _dayZhiIndexExact + 1 ];

    #endregion

    /// <summary>
    /// 干支纪日（日柱，八字流派1，晚子时日柱算明天），如己卯
    /// </summary>
    public string DayInGanZhiExact => $"{DayGanExact}{DayZhiExact}";

    /// <summary>
    /// 日所在旬（八字流派1，晚子时日柱算明天）
    /// </summary>
    public string DayXunExact => LunarUtil.GetXun( DayInGanZhiExact );

    /// <summary>
    /// 值日空亡(旬空)（八字流派1，晚子时日柱算明天）
    /// </summary>
    public string DayXunKongExact => LunarUtil.GetXunKong( DayInGanZhiExact );

    #endregion

    #region 属性 - 八字流派2

    #region 天干

    #region 下标

    /// <summary>
    /// 日对应的天干下标（八字流派2，晚子时日柱算当天），0-9
    /// </summary>
    internal int _dayGanIndexExact2;

    #endregion

    /// <summary>
    /// 日天干（八字流派2，晚子时日柱算当天），如甲
    /// </summary>
    public string DayGanExact2 => LunarUtil.GAN[ _dayGanIndexExact2 + 1 ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 日对应的地支下标（八字流派2，晚子时日柱算当天），0-11
    /// </summary>
    internal int _dayZhiIndexExact2;

    #endregion

    /// <summary>
    /// 日地支（八字流派2，晚子时日柱算当天），如卯
    /// </summary>
    /// <returns>日地支</returns>
    public string DayZhiExact2 => LunarUtil.ZHI[ _dayZhiIndexExact2 + 1 ];

    #endregion

    /// <summary>
    /// 干支纪日（日柱，八字流派2，晚子时日柱算当天），如己卯
    /// </summary>
    public string DayInGanZhiExact2 => $"{DayGanExact2}{DayZhiExact2}";

    /// <summary>
    /// 日所在旬（八字流派2，晚子时日柱算当天）
    /// </summary>
    public string DayXunExact2 => LunarUtil.GetXun( DayInGanZhiExact2 );

    /// <summary>
    /// 值日空亡(旬空)（八字流派2，晚子时日柱算当天）
    /// </summary>
    /// <returns>空亡(旬空)</returns>
    public string DayXunKongExact2 => LunarUtil.GetXunKong( DayInGanZhiExact2 );

    #endregion

    #region 方法

    /// <summary>
    /// 干支纪日计算
    /// </summary>
    private void ComputeDay() {
        var noon = Solar.FromYmdHms( _solar.Year , _solar.Month , _solar.Day , 12 );
        var offset = ( int ) noon.JulianDay - 11;
        _dayGanIndex = offset % 10;
        _dayZhiIndex = offset % 12;

        var dayGanExact = _dayGanIndex;
        var dayZhiExact = _dayZhiIndex;

        // 八字流派2，晚子时（夜子/子夜）日柱算当天
        _dayGanIndexExact2 = dayGanExact;
        _dayZhiIndexExact2 = dayZhiExact;

        // 八字流派1，晚子时（夜子/子夜）日柱算明天
        var hm = Hour.ToString().PadLeft( 2 , '0' ) + ":" + Minute.ToString().PadLeft( 2 , '0' );
        if( string.Compare( hm , "23:00" , StringComparison.Ordinal ) >= 0 && string.Compare( hm , "23:59" , StringComparison.Ordinal ) <= 0 ) {
            dayGanExact++;
            if( dayGanExact >= 10 ) {
                dayGanExact -= 10;
            }
            dayZhiExact++;
            if( dayZhiExact >= 12 ) {
                dayZhiExact -= 12;
            }
        }

        _dayGanIndexExact = dayGanExact;
        _dayZhiIndexExact = dayZhiExact;
    }

    #endregion

    #endregion

    #region 干支纪时

    #region 属性

    #region 天干

    #region 下标

    /// <summary>
    /// 时对应的天干下标，0-9
    /// </summary>
    private int _timeGanIndex;

    #endregion

    /// <summary>
    /// 时辰天干
    /// </summary>
    public string TimeGan => LunarUtil.GAN[ _timeGanIndex + 1 ];

    /// <summary>
    /// 时辰喜神方位，如艮
    /// </summary>
    public string TimePositionXi => LunarUtil.POSITION_XI[ _timeGanIndex + 1 ];

    /// <summary>
    /// 时辰喜神方位描述，如东北
    /// </summary>
    public string TimePositionXiDesc => LunarUtil.POSITION_DESC[ TimePositionXi ];

    /// <summary>
    /// 时辰阳贵神方位，如艮
    /// </summary>
    public string TimePositionYangGui => LunarUtil.POSITION_YANG_GUI[ _timeGanIndex + 1 ];

    /// <summary>
    /// 时辰阳贵神方位描述，如东北
    /// </summary>
    public string TimePositionYangGuiDesc => LunarUtil.POSITION_DESC[ TimePositionYangGui ];

    /// <summary>
    /// 时辰阴贵神方位，如艮
    /// </summary>
    public string TimePositionYinGui => LunarUtil.POSITION_YIN_GUI[ _timeGanIndex + 1 ];

    /// <summary>
    /// 时辰阴贵神方位描述，如东北
    /// </summary>
    public string TimePositionYinGuiDesc => LunarUtil.POSITION_DESC[ TimePositionYinGui ];

    /// <summary>
    /// 时辰财神方位，如艮
    /// </summary>
    public string TimePositionCai => LunarUtil.POSITION_CAI[ _timeGanIndex + 1 ];

    /// <summary>
    /// 无情之克的时冲天干，如甲
    /// </summary>
    public string TimeChongGan => LunarUtil.CHONG_GAN[ _timeGanIndex ];

    /// <summary>
    /// 有情之克的时冲天干，如甲
    /// </summary>
    public string TimeChongGanTie => LunarUtil.CHONG_GAN_TIE[ _timeGanIndex ];

    #endregion

    #region 地支

    #region 下标

    /// <summary>
    /// 时对应的地支下标，0-11
    /// </summary>
    internal int _timeZhiIndex;

    #endregion

    /// <summary>
    /// 时辰生肖，如虎
    /// </summary>
    public string TimeShengXiao => LunarUtil.SHENGXIAO[ _timeZhiIndex + 1 ];

    /// <summary>
    /// 时辰地支
    /// </summary>
    public string TimeZhi => LunarUtil.ZHI[ _timeZhiIndex + 1 ];

    /// <summary>
    /// 时煞，如北
    /// </summary>
    public string TimeSha => LunarUtil.SHA[ TimeZhi ];

    /// <summary>
    /// 时冲，如申
    /// </summary>
    public string TimeChong => LunarUtil.CHONG[ _timeZhiIndex ];

    /// <summary>
    /// 时冲生肖，如猴
    /// </summary>
    public string TimeChongShengXiao {
        get {
            string chong = TimeChong;
            for( int i = 0, j = LunarUtil.ZHI.Length ; i < j ; i++ ) {
                if( LunarUtil.ZHI[ i ].Equals( chong ) ) {
                    return LunarUtil.SHENGXIAO[ i ];
                }
            }
            return "";
        }
    }

    /// <summary>
    /// 时冲描述，如(壬申)猴
    /// </summary>
    public string TimeChongDesc => $"({TimeChongGan}{TimeChong}){TimeChongShengXiao}";

    /// <summary>
    /// 值时天神
    /// </summary>
    public string TimeTianShen => LunarUtil.TIAN_SHEN[ ( _timeZhiIndex + LunarUtil.ZHI_TIAN_SHEN_OFFSET[ DayZhiExact ] ) % 12 + 1 ];

    /// <summary>
    /// 值时天神类型：黄道/黑道
    /// </summary>
    public string TimeTianShenType => LunarUtil.TIAN_SHEN_TYPE[ TimeTianShen ];

    /// <summary>
    /// 值时天神吉凶：吉/凶
    /// </summary>
    public string TimeTianShenLuck => LunarUtil.TIAN_SHEN_TYPE_LUCK[ TimeTianShenType ];

    #endregion

    /// <summary>
    /// 时辰干支（时柱）
    /// </summary>
    public string TimeInGanZhi => $"{TimeGan}{TimeZhi}";

    /// <summary>
    /// 时辰纳音，如剑锋金
    /// </summary>
    public string TimeNaYin => LunarUtil.NAYIN[ TimeInGanZhi ];

    /// <summary>
    /// 时辰所在旬
    /// </summary>
    public string TimeXun => LunarUtil.GetXun( TimeInGanZhi );

    /// <summary>
    /// 值时空亡(旬空)
    /// </summary>
    public string TimeXunKong => LunarUtil.GetXunKong( TimeInGanZhi );

    /// <summary>
    /// 时辰宜，如果没有，返回["无"]
    /// </summary>
    /// <returns>宜</returns>
    public List<string> TimeYi => LunarUtil.GetTimeYi( DayInGanZhiExact , TimeInGanZhi );

    /// <summary>
    /// 时辰忌，如果没有，返回["无"]
    /// </summary>
    public List<string> TimeJi => LunarUtil.GetTimeJi( DayInGanZhiExact , TimeInGanZhi );

    #endregion

    #region 方法

    /// <summary>
    /// 干支纪时计算
    /// </summary>
    private void ComputeTime() {
        var hm = Hour.ToString().PadLeft( 2 , '0' ) + ":" + Minute.ToString().PadLeft( 2 , '0' );
        _timeZhiIndex = LunarUtil.GetTimeZhiIndex( hm );
        _timeGanIndex = ( _dayGanIndexExact % 5 * 2 + _timeZhiIndex ) % 10;
    }

    #endregion

    #endregion

    #region 干支纪周

    #region 属性

    #region 下标

    /// <summary>
    /// 周下标，0-6
    /// </summary>
    private int _weekIndex;

    #endregion

    /// <summary>
    /// 星期，0123456，0代表周日，1代表周一
    /// </summary>
    public int Week => _weekIndex;

    #endregion

    #region 方法

    /// <summary>
    /// 干支纪周计算
    /// </summary>
    private void ComputeWeek() {
        _weekIndex = _solar.Week;
    }

    #endregion

    #endregion

    #endregion

    #region 中文

    /// <summary>
    /// 中文年，如二〇〇一
    /// </summary>
    public string YearInChinese {
        get {
            var y = ( Year + "" ).ToCharArray();
            var s = new StringBuilder();
            for( int i = 0, j = y.Length ; i < j ; i++ ) {
                s.Append( LunarUtil.NUMBER[ y[ i ] - '0' ] );
            }
            return s.ToString();
        }
    }

    /// <summary>
    /// 中文月，如正
    /// </summary>
    public string MonthInChinese => ( Month < 0 ? "闰" : "" ) + LunarUtil.MONTH[ SystemMath.Abs( Month ) ];

    /// <summary>
    /// 中文日，如初一
    /// </summary>
    public string DayInChinese => LunarUtil.DAY[ Day ];

    /// <summary>
    /// 星期中文，日一二三四五六
    /// </summary>
    public string WeekInChinese => SolarUtil.WEEK[ Week ];

    #endregion

    #region 宿

    /// <summary>
    /// 宿
    /// </summary>
    public string Xiu => LunarUtil.XIU[ DayZhi + Week ];

    /// <summary>
    /// 宿吉凶，吉/凶
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

    #endregion

    #region 节日

    /// <summary>
    /// 节日列表，有可能一天会有多个节日
    /// </summary>
    public List<string> Festivals {
        get {
            var l = new List<string>();
            try {
                l.Add( LunarUtil.FESTIVAL[ $"{Month}-{Day}" ] );
            } catch {
                // ignored
            }

            if( SystemMath.Abs( Month ) == 12 && Day >= 29 && Year != Next( 1 ).Year ) {
                l.Add( "除夕" );
            }
            return l;
        }
    }

    /// <summary>
    /// 非正式的节日列表，有可能一天会有多个节日，如中元节
    /// </summary>
    public List<string> OtherFestivals {
        get {
            var l = new List<string>();
            try {
                l.AddRange( LunarUtil.OTHER_FESTIVAL[ $"{Month}-{Day}" ] );
            } catch {
                // ignored
            }

            var solarYmd = _solar.Ymd;
            var jq = JieQiTable[ "清明" ];
            if( solarYmd.Equals( jq.Next( -1 ).Ymd ) ) {
                l.Add( "寒食节" );
            }

            jq = JieQiTable[ "立春" ];
            var offset = 4 - jq.ToLunar()._dayGanIndex;
            if( offset < 0 ) {
                offset += 10;
            }
            if( solarYmd.Equals( jq.Next( offset + 40 ).Ymd ) ) {
                l.Add( "春社" );
            }

            jq = JieQiTable[ "立秋" ];
            offset = 4 - jq.ToLunar()._dayGanIndex;
            if( offset < 0 ) {
                offset += 10;
            }
            if( solarYmd.Equals( jq.Next( offset + 40 ).Ymd ) ) {
                l.Add( "秋社" );
            }
            return l;
        }
    }

    #endregion

    #endregion

    /// <summary>
    /// 六曜
    /// </summary>
    public string LiuYao => LunarUtil.LIU_YAO[ ( SystemMath.Abs( Month ) + Day - 2 ) % 6 ];

    /// <summary>
    /// 物候
    /// </summary>
    public string WuHou {
        get {
            var jieQi = GetPrevJieQi( true );
            var offset = 0;
            for( int i = 0, j = LunarUtil.JIE_QI.Length ; i < j ; i++ ) {
                if( jieQi.Name.Equals( LunarUtil.JIE_QI[ i ] ) ) {
                    offset = i;
                    break;
                }
            }
            var index = _solar.Subtract( jieQi.Solar ) / 5;
            if( index > 2 ) {
                index = 2;
            }
            return LunarUtil.WU_HOU[ ( offset * 3 + index ) % LunarUtil.WU_HOU.Length ];
        }
    }

    /// <summary>
    /// 候
    /// </summary>
    public string Hou {
        get {
            var jieQi = GetPrevJieQi( true );
            var max = LunarUtil.HOU.Length - 1;
            var offset = _solar.Subtract( jieQi.Solar ) / 5;
            if( offset > max ) {
                offset = max;
            }
            return $"{jieQi.Name} {LunarUtil.HOU[ offset ]}";
        }
    }

    /// <summary>
    /// 日禄
    /// </summary>
    public string DayLu {
        get {
            var gan = LunarUtil.LU[ DayGan ];
            string zhi = null;
            try {
                zhi = LunarUtil.LU[ DayZhi ];
            } catch {
                // ignored
            }

            var lu = $"{gan}命互禄";
            if( null != zhi ) {
                lu = $"{lu} {zhi}命进禄";
            }
            return lu;
        }
    }
}
