namespace Meow.Calendar;

/// <summary>
/// 农历日期
/// </summary>
public partial class Lunar {

    #region 初始化

    /// <summary>
    /// 通过指定阳历日期获取农历
    /// </summary>
    /// <param name="date">阳历日期字符串，2023-12-02、20231202、20231202101010、2023-12-02 12:12:12</param>
    /// <returns>农历</returns>
    public static Lunar FromDate( string date ) {
        return new Lunar( date );
    }

    /// <summary>
    /// 通过指定阳历日期获取农历
    /// </summary>
    /// <param name="date">阳历日期</param>
    /// <returns>农历</returns>
    public static Lunar FromDate( DateTime date ) {
        return new Lunar( date );
    }

    /// <summary>
    /// 通过指定农历年月日时分秒获取农历
    /// </summary>
    /// <param name="lunarYear">年（农历）</param>
    /// <param name="lunarMonth">月（农历），1到12，闰月为负，即闰2月=-2</param>
    /// <param name="lunarDay">日（农历），1到31</param>
    /// <param name="hour">小时（阳历）</param>
    /// <param name="minute">分钟（阳历）</param>
    /// <param name="second">秒钟（阳历）</param>
    /// <returns>农历</returns>
    public static Lunar FromYmdHms( int lunarYear , int lunarMonth , int lunarDay , int hour = 0 , int minute = 0 , int second = 0 ) {
        return new Lunar( lunarYear , lunarMonth , lunarDay , hour , minute , second );
    }

    #endregion

    #region 太岁方位

    #region 年太岁方位

    /// <summary>
    /// 年太岁方位（流派2新年以立春零点起算）
    /// </summary>
    public string YearPositionTaiSui => GetYearPositionTaiSui();

    /// <summary>
    /// 获取年太岁方位
    /// </summary>
    /// <param name="sect">流派：2为新年以立春零点起算；1为新年以正月初一起算；3为新年以立春节气交接的时刻起算</param>
    /// <returns>方位</returns>
    public string GetYearPositionTaiSui( int sect = 2 ) {
        int yearZhiIndex;
        switch( sect ) {
            case 1:
                yearZhiIndex = _yearZhiIndex;
                break;
            case 3:
                yearZhiIndex = _yearZhiIndexExact;
                break;
            default:
                yearZhiIndex = _yearZhiIndexByLiChun;
                break;
        }
        return LunarUtil.POSITION_TAI_SUI_YEAR[ yearZhiIndex ];
    }

    /// <summary>
    /// 年太岁方位描述（流派2新年以立春零点起算），如东北
    /// </summary>
    public string YearPositionTaiSuiDesc => GetYearPositionTaiSuiDesc();

    /// <summary>
    /// 年太岁方位描述
    /// </summary>
    /// <param name="sect">流派：2为新年以立春零点起算；1为新年以正月初一起算；3为新年以立春节气交接的时刻起算</param>
    /// <returns>方位描述，如东北</returns>
    public string GetYearPositionTaiSuiDesc( int sect = 2 ) {
        return LunarUtil.POSITION_DESC[ GetYearPositionTaiSui( sect ) ];
    }

    #endregion

    #region 月太岁方位

    /// <summary>
    /// 月太岁方位（流派2新的一月以节交接当天零点起算），如艮
    /// </summary>
    public string MonthPositionTaiSui => GetMonthPositionTaiSui();

    /// <summary>
    /// 获取月太岁方位
    /// </summary>
    /// <param name="sect">流派：2为新的一月以节交接当天零点起算；3为新的一月以节交接准确时刻起算</param>
    /// <returns>方位，如艮</returns>
    public string GetMonthPositionTaiSui( int sect = 2 ) {
        int monthZhiIndex;
        int monthGanIndex;
        switch( sect ) {
            case 3:
                monthZhiIndex = _monthZhiIndexExact;
                monthGanIndex = _monthGanIndexExact;
                break;
            default:
                monthZhiIndex = _monthZhiIndex;
                monthGanIndex = _monthGanIndex;
                break;
        }
        return GetMonthPositionTaiSui( monthZhiIndex , monthGanIndex );
    }

    /// <summary>
    /// 计算月太岁方
    /// </summary>
    /// <param name="monthZhiIndex">月支序号</param>
    /// <param name="monthGanIndex">月干序号</param>
    /// <returns>太岁方</returns>
    private string GetMonthPositionTaiSui( int monthZhiIndex , int monthGanIndex ) {
        string p;
        var m = monthZhiIndex - LunarUtil.BASE_MONTH_ZHI_INDEX;
        if( m < 0 ) {
            m += 12;
        }
        switch( m ) {
            case 0:
            case 4:
            case 8:
                p = "艮";
                break;
            case 2:
            case 6:
            case 10:
                p = "坤";
                break;
            case 3:
            case 7:
            case 11:
                p = "巽";
                break;
            default:
                p = LunarUtil.POSITION_GAN[ monthGanIndex ];
                break;
        }
        return p;
    }

    /// <summary>
    /// 月太岁方位描述（流派2新的一月以节交接当天零点起算），如东北
    /// </summary>
    public string MonthPositionTaiSuiDesc => GetMonthPositionTaiSuiDesc();

    /// <summary>
    /// 获取月太岁方位描述
    /// </summary>
    /// <param name="sect">流派：2为新的一月以节交接当天零点起算；3为新的一月以节交接准确时刻起算</param>
    /// <returns>方位描述，如东北</returns>
    public string GetMonthPositionTaiSuiDesc( int sect = 2 ) {
        return LunarUtil.POSITION_DESC[ GetMonthPositionTaiSui( sect ) ];
    }

    #endregion

    #region 日太岁方位

    /// <summary>
    /// 日太岁方位（流派2新年以立春零点起算），如艮
    /// </summary>
    public string DayPositionTaiSui => GetDayPositionTaiSui();

    /// <summary>
    /// 获取日太岁方位
    /// </summary>
    /// <param name="sect">流派：2新年以立春零点起算；1新年以正月初一起算；3新年以立春节气交接的时刻起算</param>
    /// <returns>方位，如艮</returns>
    public string GetDayPositionTaiSui( int sect = 2 ) {
        string dayInGanZhi;
        int yearZhiIndex;
        switch( sect ) {
            case 1:
                dayInGanZhi = DayInGanZhi;
                yearZhiIndex = _yearZhiIndex;
                break;
            case 3:
                dayInGanZhi = DayInGanZhi;
                yearZhiIndex = _yearZhiIndexExact;
                break;
            default:
                dayInGanZhi = DayInGanZhiExact2;
                yearZhiIndex = _yearZhiIndexByLiChun;
                break;
        }
        return GetDayPositionTaiSui( dayInGanZhi , yearZhiIndex );
    }

    /// <summary>
    /// 计算日太岁方
    /// </summary>
    /// <param name="dayInGanZhi">日干序号</param>
    /// <param name="yearZhiIndex">年支序号</param>
    /// <returns>太岁方</returns>
    private string GetDayPositionTaiSui( string dayInGanZhi , int yearZhiIndex ) {
        string p;
        if( "甲子,乙丑,丙寅,丁卯,戊辰,已巳".Contains( dayInGanZhi ) ) {
            p = "震";
        } else if( "丙子,丁丑,戊寅,已卯,庚辰,辛巳".Contains( dayInGanZhi ) ) {
            p = "离";
        } else if( "戊子,已丑,庚寅,辛卯,壬辰,癸巳".Contains( dayInGanZhi ) ) {
            p = "中";
        } else if( "庚子,辛丑,壬寅,癸卯,甲辰,乙巳".Contains( dayInGanZhi ) ) {
            p = "兑";
        } else if( "壬子,癸丑,甲寅,乙卯,丙辰,丁巳".Contains( dayInGanZhi ) ) {
            p = "坎";
        } else {
            p = LunarUtil.POSITION_TAI_SUI_YEAR[ yearZhiIndex ];
        }
        return p;
    }

    /// <summary>
    /// 日太岁方位描述（流派2新年以立春零点起算），如东北
    /// </summary>
    public string DayPositionTaiSuiDesc => GetDayPositionTaiSuiDesc();

    /// <summary>
    /// 获取日太岁方位描述
    /// </summary>
    /// <param name="sect">流派：2新年以立春零点起算；1新年以正月初一起算；3新年以立春节气交接的时刻起算</param>
    /// <returns>方位描述，如东北</returns>
    public string GetDayPositionTaiSuiDesc( int sect = 2 ) {
        return LunarUtil.POSITION_DESC[ GetDayPositionTaiSui( sect ) ];
    }

    #endregion

    #endregion

    #region 福神方位

    #region 日福神方位

    /// <summary>
    /// 日福神方位，流派2，如艮
    /// </summary>
    public string DayPositionFu => GetDayPositionFu();

    /// <summary>
    /// 获取日福神方位
    /// </summary>
    /// <param name="sect">流派，可选1或2</param>
    /// <returns>福神方位，如艮</returns>
    public string GetDayPositionFu( int sect = 2 ) {
        return ( 1 == sect ? LunarUtil.POSITION_FU : LunarUtil.POSITION_FU_2 )[ _dayGanIndex + 1 ];
    }

    /// <summary>
    /// 日福神方位描述，如东北
    /// </summary>
    public string DayPositionFuDesc => GetDayPositionFuDesc();

    /// <summary>
    /// 获取日福神方位描述
    /// </summary>
    /// <param name="sect">流派，可选1或2</param>
    /// <returns>方位描述，如东北</returns>
    public string GetDayPositionFuDesc( int sect = 2 ) {
        return LunarUtil.POSITION_DESC[ GetDayPositionFu( sect ) ];
    }

    #endregion

    #region 时辰福神方位

    /// <summary>
    /// 时辰福神方位（流派2），如艮
    /// </summary>
    public string TimePositionFu => GetTimePositionFu();

    /// <summary>
    /// 获取时辰福神方位
    /// </summary>
    /// <param name="sect">流派，可选1或2</param>
    /// <returns>福神方位，如艮</returns>
    public string GetTimePositionFu( int sect = 2 ) {
        return ( 1 == sect ? LunarUtil.POSITION_FU : LunarUtil.POSITION_FU_2 )[ _timeGanIndex + 1 ];
    }

    /// <summary>
    /// 时辰福神方位描述（流派2），如东北
    /// </summary>
    public string TimePositionFuDesc => GetTimePositionFuDesc();

    /// <summary>
    /// 获取时辰福神方位描述
    /// </summary>
    /// <param name="sect">流派，可选1或2</param>
    /// <returns>福神方位描述，如东北</returns>
    public string GetTimePositionFuDesc( int sect = 2 ) {
        return LunarUtil.POSITION_DESC[ GetTimePositionFu( sect ) ];
    }

    /// <summary>
    /// 时辰财神方位描述，如东北
    /// </summary>
    public string TimePositionCaiDesc => LunarUtil.POSITION_DESC[ TimePositionCai ];

    #endregion

    #endregion

    #region 九星

    #region 值年九星

    /// <summary>
    /// 值年九星（流派2新年以立春零点起算。流年紫白星起例歌诀：年上吉星论甲子，逐年星逆中宫起；上中下作三元汇，一上四中七下兑。）
    /// </summary>
    public NineStar YearNineStar => GetYearNineStar();

    /// <summary>
    /// 获取值年九星（流年紫白星起例歌诀：年上吉星论甲子，逐年星逆中宫起；上中下作三元汇，一上四中七下兑。）
    /// </summary>
    /// <param name="sect">流派：2为新年以立春零点起算；1为新年以正月初一起算；3为新年以立春节气交接的时刻起算</param>
    /// <returns>九星</returns>
    public NineStar GetYearNineStar( int sect = 2 ) {
        string yearInGanZhi;
        switch( sect ) {
            case 1:
                yearInGanZhi = this.YearInGanZhi;
                break;
            case 3:
                yearInGanZhi = this.YearInGanZhiExact;
                break;
            default:
                yearInGanZhi = this.YearInGanZhiByLiChun;
                break;
        }
        return GetYearNineStar( yearInGanZhi );
    }

    /// <summary>
    /// 计算年九星
    /// </summary>
    /// <param name="yearInGanZhi">年干支</param>
    /// <returns>九星</returns>
    private NineStar GetYearNineStar( string yearInGanZhi ) {
        var indexExact = LunarUtil.GetJiaZiIndex( yearInGanZhi ) + 1;
        var index = LunarUtil.GetJiaZiIndex( YearInGanZhi ) + 1;
        var yearOffset = indexExact - index;
        if( yearOffset > 1 ) {
            yearOffset -= 60;
        } else if( yearOffset < -1 ) {
            yearOffset += 60;
        }
        var yuan = ( Year + yearOffset + 2696 ) / 60 % 3;
        var offset = ( 62 + yuan * 3 - indexExact ) % 9;
        if( 0 == offset ) {
            offset = 9;
        }
        return NineStar.FromIndex( offset - 1 );
    }


    #endregion

    #region 值月九星

    /// <summary>
    /// 值月九星（流派2新的一月以节交接当天零点起算。月紫白星歌诀：子午卯酉八白起，寅申巳亥二黑求，辰戌丑未五黄中。）
    /// </summary>
    public NineStar MonthNineStar => GetMonthNineStar();

    /// <summary>
    /// 获取值月九星（月紫白星歌诀：子午卯酉八白起，寅申巳亥二黑求，辰戌丑未五黄中。）
    /// </summary>
    /// <param name="sect">流派：2为新的一月以节交接当天零点起算；3为新的一月以节交接准确时刻起算</param>
    /// <returns>九星</returns>
    public NineStar GetMonthNineStar( int sect = 2 ) {
        int yearZhiIndex;
        int monthZhiIndex;
        switch( sect ) {
            case 1:
                yearZhiIndex = _yearZhiIndex;
                monthZhiIndex = _monthZhiIndex;
                break;
            case 3:
                yearZhiIndex = _yearZhiIndexExact;
                monthZhiIndex = _monthZhiIndexExact;
                break;
            default:
                yearZhiIndex = _yearZhiIndexByLiChun;
                monthZhiIndex = _monthZhiIndex;
                break;
        }
        return GetMonthNineStar( yearZhiIndex , monthZhiIndex );
    }

    /// <summary>
    /// 计算月九星
    /// </summary>
    /// <param name="yearZhiIndex">年支序号</param>
    /// <param name="monthZhiIndex">月支序号</param>
    /// <returns>九星</returns>
    protected NineStar GetMonthNineStar( int yearZhiIndex , int monthZhiIndex ) {
        var n = 27 - yearZhiIndex % 3 * 3;
        if( monthZhiIndex < LunarUtil.BASE_MONTH_ZHI_INDEX ) {
            n -= 3;
        }
        var offset = ( n - monthZhiIndex ) % 9;
        return NineStar.FromIndex( offset );
    }

    #endregion

    #region 值时九星

    /// <summary>
    /// 值时九星（时家紫白星歌诀：三元时白最为佳，冬至阳生顺莫差，孟日七宫仲一白，季日四绿发萌芽，每把时辰起甲子，本时星耀照光华，时星移入中宫去，顺飞八方逐细查。夏至阴生逆回首，孟归三碧季加六，仲在九宫时起甲，依然掌中逆轮跨。）
    /// </summary>
    public NineStar TimeNineStar {
        get {
            // 顺逆
            var solarYmd = _solar.Ymd;
            var asc = false;
            if( string.Compare( solarYmd , JieQiTable[ "冬至" ].Ymd , StringComparison.Ordinal ) >= 0 && string.Compare( solarYmd , JieQiTable[ "夏至" ].Ymd , StringComparison.Ordinal ) < 0 ) {
                asc = true;
            } else if( string.Compare( solarYmd , JieQiTable[ "DONG_ZHI" ].Ymd , StringComparison.Ordinal ) >= 0 ) {
                asc = true;
            }

            var start = asc ? 6 : 2;
            var dayZhi = DayZhi;
            if( "子午卯酉".Contains( dayZhi ) ) {
                start = asc ? 0 : 8;
            } else if( "辰戌丑未".Contains( dayZhi ) ) {
                start = asc ? 3 : 5;
            }

            var index = asc ? start + _timeZhiIndex : start + 9 - _timeZhiIndex;
            return new NineStar( index % 9 );
        }
    }

    #endregion

    #region 值日九星

    /// <summary>
    /// 值日九星（日家紫白星歌诀：日家白法不难求，二十四气六宫周；冬至雨水及谷雨，阳顺一七四中游；夏至处暑霜降后，九三六星逆行求。）
    /// </summary>
    public NineStar DayNineStar {
        get {
            var solarYmd = _solar.Ymd;
            var dongZhi = JieQiTable[ "冬至" ];
            var dongZhi2 = JieQiTable[ "DONG_ZHI" ];
            var xiaZhi = JieQiTable[ "夏至" ];
            var dongZhiIndex = LunarUtil.GetJiaZiIndex( dongZhi.ToLunar().DayInGanZhi );
            var dongZhiIndex2 = LunarUtil.GetJiaZiIndex( dongZhi2.ToLunar().DayInGanZhi );
            var xiaZhiIndex = LunarUtil.GetJiaZiIndex( xiaZhi.ToLunar().DayInGanZhi );
            var solarShunBai = dongZhiIndex > 29 ? dongZhi.Next( 60 - dongZhiIndex ) : dongZhi.Next( -dongZhiIndex );

            var solarShunBaiYmd = solarShunBai.Ymd;
            var solarShunBai2 = dongZhiIndex2 > 29 ? dongZhi2.Next( 60 - dongZhiIndex2 ) : dongZhi2.Next( -dongZhiIndex2 );

            var solarShunBaiYmd2 = solarShunBai2.Ymd;
            var solarNiZi = xiaZhiIndex > 29 ? xiaZhi.Next( 60 - xiaZhiIndex ) : xiaZhi.Next( -xiaZhiIndex );

            var solarNiZiYmd = solarNiZi.Ymd;
            var offset = 0;
            if( string.Compare( solarYmd , solarShunBaiYmd , StringComparison.Ordinal ) >= 0 && string.Compare( solarYmd , solarNiZiYmd , StringComparison.Ordinal ) < 0 ) {
                offset = _solar.Subtract( solarShunBai ) % 9;
            } else if( string.Compare( solarYmd , solarNiZiYmd , StringComparison.Ordinal ) >= 0 && string.Compare( solarYmd , solarShunBaiYmd2 , StringComparison.Ordinal ) < 0 ) {
                offset = 8 - ( _solar.Subtract( solarNiZi ) % 9 );
            } else if( string.Compare( solarYmd , solarShunBaiYmd2 , StringComparison.Ordinal ) >= 0 ) {
                offset = _solar.Subtract( solarShunBai2 ) % 9;
            } else if( string.Compare( solarYmd , solarShunBaiYmd , StringComparison.Ordinal ) < 0 ) {
                offset = ( 8 + solarShunBai.Subtract( _solar ) ) % 9;
            }

            return NineStar.FromIndex( offset );
        }
    }


    #endregion

    #endregion

    #region 节气

    /// <summary>
    /// 节令
    /// </summary>
    public string Jie {
        get {
            for( int i = 0, j = LunarUtil.JIE_QI_IN_USE.Length ; i < j ; i += 2 ) {
                var key = LunarUtil.JIE_QI_IN_USE[ i ];
                var d = JieQiTable[ key ];
                if( d.Year == _solar.Year && d.Month == _solar.Month && d.Day == _solar.Day ) {
                    return ConvertJieQi( key );
                }
            }
            return "";
        }
    }

    /// <summary>
    /// 气令
    /// </summary>
    public string Qi {
        get {
            for( int i = 1, j = LunarUtil.JIE_QI_IN_USE.Length ; i < j ; i += 2 ) {
                var key = LunarUtil.JIE_QI_IN_USE[ i ];
                var d = JieQiTable[ key ];
                if( d.Year == _solar.Year && d.Month == _solar.Month && d.Day == _solar.Day ) {
                    return ConvertJieQi( key );
                }
            }
            return "";
        }
    }

    /// <summary>
    /// 节气名称，如果无节气，返回空字符串
    /// </summary>
    public string JieQi {
        get {
            foreach( var entry in JieQiTable ) {
                var d = entry.Value;
                if( d.Year == _solar.Year && d.Month == _solar.Month && d.Day == _solar.Day ) {
                    return ConvertJieQi( entry.Key );
                }
            }
            return "";
        }
    }

    /// <summary>
    /// 转节气名
    /// </summary>
    /// <param name="name">节气名</param>
    /// <returns>正式的节气名</returns>
    private string ConvertJieQi( string name ) {
        var jq = name;
        switch( jq ) {
            case "DONG_ZHI":
                jq = "冬至";
                break;
            case "DA_HAN":
                jq = "大寒";
                break;
            case "XIAO_HAN":
                jq = "小寒";
                break;
            case "LI_CHUN":
                jq = "立春";
                break;
            case "DA_XUE":
                jq = "大雪";
                break;
            case "YU_SHUI":
                jq = "雨水";
                break;
            case "JING_ZHE":
                jq = "惊蛰";
                break;
        }
        return jq;
    }

    /// <summary>
    /// 获取下一节（顺推的第一个节）
    /// </summary>
    /// <param name="wholeDay">是否按天计</param>
    /// <returns>节气</returns>
    public JieQi GetNextJie( bool wholeDay = false ) {
        var l = LunarUtil.JIE_QI_IN_USE.Length / 2;
        var conditions = new string[ l ];
        for( var i = 0 ; i < l ; i++ ) {
            conditions[ i ] = LunarUtil.JIE_QI_IN_USE[ i * 2 ];
        }
        return GetNearJieQi( true , conditions , wholeDay );
    }

    /// <summary>
    /// 获取上一节（逆推的第一个节）
    /// </summary>
    /// <param name="wholeDay">是否按天计</param>
    /// <returns>节气</returns>
    public JieQi GetPrevJie( bool wholeDay = false ) {
        var l = LunarUtil.JIE_QI_IN_USE.Length / 2;
        var conditions = new string[ l ];
        for( var i = 0 ; i < l ; i++ ) {
            conditions[ i ] = LunarUtil.JIE_QI_IN_USE[ i * 2 ];
        }
        return GetNearJieQi( false , conditions , wholeDay );
    }

    /// <summary>
    /// 获取下一气令（顺推的第一个气令）
    /// </summary>
    /// <param name="wholeDay">是否按天计</param>
    /// <returns>节气</returns>
    public JieQi GetNextQi( bool wholeDay = false ) {
        var l = LunarUtil.JIE_QI_IN_USE.Length / 2;
        var conditions = new string[ l ];
        for( var i = 0 ; i < l ; i++ ) {
            conditions[ i ] = LunarUtil.JIE_QI_IN_USE[ i * 2 + 1 ];
        }
        return GetNearJieQi( true , conditions , wholeDay );
    }

    /// <summary>
    /// 获取上一气令（逆推的第一个气令）
    /// </summary>
    /// <param name="wholeDay">是否按天计</param>
    /// <returns>节气</returns>
    public JieQi GetPrevQi( bool wholeDay = false ) {
        var l = LunarUtil.JIE_QI_IN_USE.Length / 2;
        var conditions = new string[ l ];
        for( var i = 0 ; i < l ; i++ ) {
            conditions[ i ] = LunarUtil.JIE_QI_IN_USE[ i * 2 + 1 ];
        }
        return GetNearJieQi( false , conditions , wholeDay );
    }

    /// <summary>
    /// 获取下一节气（顺推的第一个节气）
    /// </summary>
    /// <param name="wholeDay">是否按天计</param>
    /// <returns>节气</returns>
    public JieQi GetNextJieQi( bool wholeDay = false ) {
        return GetNearJieQi( true , null , wholeDay );
    }

    /// <summary>
    /// 获取上一节气（逆推的第一个节气）
    /// </summary>
    /// <param name="wholeDay">是否按天计</param>
    /// <returns>节气</returns>
    public JieQi GetPrevJieQi( bool wholeDay = false ) {
        return GetNearJieQi( false , null , wholeDay );
    }

    /// <summary>
    /// 获取最近的节气，如果未找到匹配的，返回null
    /// </summary>
    /// <param name="forward">是否顺推，true为顺推，false为逆推</param>
    /// <param name="conditions">过滤条件，如果设置过滤条件，仅返回匹配该名称的</param>
    /// <param name="wholeDay">是否按天计</param>
    /// <returns>节气</returns>
    private JieQi GetNearJieQi( bool forward , string[] conditions , bool wholeDay ) {
        string name = null;
        Solar near = null;
        var filters = new List<string>();
        if( null != conditions ) {
            foreach( var cond in conditions ) {
                if( !filters.Contains( cond ) ) {
                    filters.Add( cond );
                }
            }
        }
        var filter = filters.Count > 0;
        var today = wholeDay ? _solar.Ymd : _solar.YmdHms;
        foreach( var entry in JieQiTable ) {
            var jq = ConvertJieQi( entry.Key );
            if( filter ) {
                if( !filters.Contains( jq ) ) {
                    continue;
                }
            }

            var current = entry.Value;
            var day = wholeDay ? current.Ymd : current.YmdHms;
            if( forward ) {
                if( string.Compare( day , today , StringComparison.Ordinal ) < 0 ) {
                    continue;
                }
                if( null == near ) {
                    name = jq;
                    near = current;
                } else {
                    var nearDay = wholeDay ? near.Ymd : near.YmdHms;
                    if( string.Compare( day , nearDay , StringComparison.Ordinal ) < 0 ) {
                        name = jq;
                        near = current;
                    }
                }
            } else {
                if( string.Compare( day , today , StringComparison.Ordinal ) > 0 ) {
                    continue;
                }
                if( null == near ) {
                    name = jq;
                    near = current;
                } else {
                    var nearDay = wholeDay ? near.Ymd : near.YmdHms;
                    if( string.Compare( day , nearDay , StringComparison.Ordinal ) > 0 ) {
                        name = jq;
                        near = current;
                    }
                }
            }

        }
        return null == near ? null : new JieQi( name , near );
    }

    /// <summary>
    /// 当天节气对象，如果无节气，返回null
    /// </summary>
    public JieQi CurrentJieQi => ( from jq in JieQiTable let d = jq.Value where d.Year == _solar.Year && d.Month == _solar.Month && d.Day == _solar.Day select new JieQi( ConvertJieQi( jq.Key ) , d ) ).FirstOrDefault();

    /// <summary>
    /// 当天节令对象，如果无节令，返回null
    /// </summary>
    public JieQi CurrentJie {
        get {
            for( int i = 0, j = LunarUtil.JIE_QI_IN_USE.Length ; i < j ; i += 2 ) {
                var key = LunarUtil.JIE_QI_IN_USE[ i ];
                var d = JieQiTable[ key ];
                if( d.Year == _solar.Year && d.Month == _solar.Month && d.Day == _solar.Day ) {
                    return new JieQi( ConvertJieQi( key ) , d );
                }
            }
            return null;
        }
    }

    /// <summary>
    /// 当天气令对象，如果无气令，返回null
    /// </summary>
    public JieQi CurrentQi {
        get {
            for( int i = 1, j = LunarUtil.JIE_QI_IN_USE.Length ; i < j ; i += 2 ) {
                var key = LunarUtil.JIE_QI_IN_USE[ i ];
                var d = JieQiTable[ key ];
                if( d.Year == _solar.Year && d.Month == _solar.Month && d.Day == _solar.Day ) {
                    return new JieQi( ConvertJieQi( key ) , d );
                }
            }
            return null;
        }
    }

    #endregion

    #region 数九

    /// <summary>
    /// 数九，如果不是数九天，返回null
    /// </summary>
    public ShuJiu ShuJiu {
        get {
            var current = new Solar( _solar.Year , _solar.Month , _solar.Day );
            var start = JieQiTable[ "DONG_ZHI" ];
            start = new Solar( start.Year , start.Month , start.Day );
            if( current.IsBefore( start ) ) {
                start = JieQiTable[ "冬至" ];
                start = new Solar( start.Year , start.Month , start.Day );
            }

            var end = new Solar( start.Year , start.Month , start.Day ).Next( 81 );

            if( current.IsBefore( start ) || !current.IsBefore( end ) ) {
                return null;
            }

            var days = current.Subtract( start );
            return new ShuJiu( $"{LunarUtil.NUMBER[ days / 9 + 1 ]}九" , days % 9 + 1 );
        }
    }

    #endregion

    #region 三伏

    /// <summary>
    /// 三伏，如果不是伏天，返回null
    /// </summary>
    public Fu Fu {
        get {
            var current = new Solar( _solar.Year , _solar.Month , _solar.Day );
            var xiaZhi = JieQiTable[ "夏至" ];
            var liQiu = JieQiTable[ "立秋" ];
            var start = new Solar( xiaZhi.Year , xiaZhi.Month , xiaZhi.Day );
            var add = 6 - xiaZhi.ToLunar()._dayGanIndex;
            if( add < 0 ) {
                add += 10;
            }
            add += 20;
            start = start.Next( add );
            if( current.IsBefore( start ) ) {
                return null;
            }

            var days = current.Subtract( start );
            if( days < 10 ) {
                return new Fu( "初伏" , days + 1 );
            }

            start = start.Next( 10 );
            days = current.Subtract( start );
            if( days < 10 ) {
                return new Fu( "中伏" , days + 1 );
            }
            start = start.Next( 10 );
            days = current.Subtract( start );
            var liQiuSolar = new Solar( liQiu.Year , liQiu.Month , liQiu.Day );
            if( liQiuSolar.IsAfter( start ) ) {
                if( days < 10 ) {
                    return new Fu( "中伏" , days + 11 );
                }
                start = start.Next( 10 );
                days = current.Subtract( start );
            }
            if( days < 10 ) {
                return new Fu( "末伏" , days + 1 );
            }
            return null;
        }
    }

    #endregion

    #region 时辰

    /// <summary>
    /// 时辰
    /// </summary>
    public LunarTime Time => new LunarTime( Year , Month , Day , Hour , Minute , Second );

    /// <summary>
    /// 当天的时辰列表
    /// </summary>
    public List<LunarTime> Times {
        get {
            var l = new List<LunarTime> { new LunarTime( Year , Month , Day , 0 , 0 , 0 ) };
            for( var i = 0 ; i < 12 ; i++ ) {
                l.Add( new LunarTime( Year , Month , Day , ( i + 1 ) * 2 - 1 , 0 , 0 ) );
            }
            return l;
        }
    }

    #endregion

    /// <summary>
    /// 获取往后推几天的农历日期，如果要往前推，则天数用负数
    /// </summary>
    /// <param name="days">天数</param>
    /// <returns>农历日期</returns>
    public Lunar Next( int days ) {
        return _solar.Next( days ).ToLunar();
    }

    #region 格式化输出

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString {
        get {
            StringBuilder s = new StringBuilder();
            s.Append( ToString() );
            s.Append( ' ' );
            s.Append( YearInGanZhi );
            s.Append( '(' );
            s.Append( YearShengXiao );
            s.Append( ")年 " );
            s.Append( MonthInGanZhi );
            s.Append( '(' );
            s.Append( MonthShengXiao );
            s.Append( ")月 " );
            s.Append( DayInGanZhi );
            s.Append( '(' );
            s.Append( DayShengXiao );
            s.Append( ")日 " );
            s.Append( TimeZhi );
            s.Append( '(' );
            s.Append( TimeShengXiao );
            s.Append( ")时 纳音[" );
            s.Append( YearNaYin );
            s.Append( ' ' );
            s.Append( MonthNaYin );
            s.Append( ' ' );
            s.Append( DayNaYin );
            s.Append( ' ' );
            s.Append( TimeNaYin );
            s.Append( "] 星期" );
            s.Append( WeekInChinese );
            foreach( var f in Festivals ) {
                s.Append( " (" );
                s.Append( f );
                s.Append( ')' );
            }

            foreach( var f in OtherFestivals ) {
                s.Append( " (" );
                s.Append( f );
                s.Append( ')' );
            }

            var jq = JieQi;
            if( jq.Length > 0 ) {
                s.Append( " [" );
                s.Append( jq );
                s.Append( ']' );
            }

            s.Append( ' ' );
            s.Append( Gong );
            s.Append( '方' );
            s.Append( Shou );
            s.Append( " 星宿[" );
            s.Append( Xiu );
            s.Append( Zheng );
            s.Append( Animal );
            s.Append( "](" );
            s.Append( XiuLuck );
            s.Append( ") 彭祖百忌[" );
            s.Append( PengZuGan );
            s.Append( ' ' );
            s.Append( PengZuZhi );
            s.Append( "] 喜神方位[" );
            s.Append( DayPositionXi );
            s.Append( "](" );
            s.Append( DayPositionXiDesc );
            s.Append( ") 阳贵神方位[" );
            s.Append( DayPositionYangGui );
            s.Append( "](" );
            s.Append( DayPositionYangGuiDesc );
            s.Append( ") 阴贵神方位[" );
            s.Append( DayPositionYinGui );
            s.Append( "](" );
            s.Append( DayPositionYinGuiDesc );
            s.Append( ") 福神方位[" );
            s.Append( DayPositionFu );
            s.Append( "](" );
            s.Append( DayPositionFuDesc );
            s.Append( ") 财神方位[" );
            s.Append( DayPositionCai );
            s.Append( "](" );
            s.Append( DayPositionCaiDesc );
            s.Append( ") 冲[" );
            s.Append( DayChongDesc );
            s.Append( "] 煞[" );
            s.Append( DaySha );
            s.Append( ']' );
            return s.ToString();
        }
    }

    /// <inheritdoc />
    public override string ToString() {
        return $"{YearInChinese}年{MonthInChinese}月{DayInChinese}";
    }

    #endregion

}
