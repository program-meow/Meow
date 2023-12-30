namespace Meow.Calendar;

/// <summary>
/// 阳历日期
/// </summary>
public partial class Solar {

    #region 创建

    /// <summary>
    /// 通过指定年月日时分获取阳历
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月，1到12</param>
    /// <param name="day">日，1到31</param>
    /// <param name="hour">小时，0到23</param>
    /// <param name="minute">分钟，0到59</param>
    /// <param name="second">秒钟，0到59</param>
    /// <returns>阳历</returns>
    public static Solar FromYmdHms( int year , int month , int day , int hour = 0 , int minute = 0 , int second = 0 ) {
        return new Solar( year , month , day , hour , minute , second );
    }

    /// <summary>
    /// 通过指定日期获取阳历
    /// </summary>
    /// <param name="date">日期字符串，2023-12-02、20231202、20231202101010、2023-12-02 12:12:12</param>
    /// <returns>阳历</returns>
    public static Solar FromDate( string date ) {
        return new Solar( date );
    }

    /// <summary>
    /// 通过指定日期获取阳历
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>阳历</returns>
    public static Solar FromDate( DateTime date ) {
        return new Solar( date );
    }

    /// <summary>
    /// 通过指定儒略日获取阳历
    /// </summary>
    /// <param name="julianDay">儒略日</param>
    /// <returns>阳历</returns>
    public static Solar FromJulianDay( double julianDay ) {
        return new Solar( julianDay );
    }

    /// <summary>
    /// 通过八字获取阳历列表
    /// </summary>
    /// <param name="yearGanZhi">年柱</param>
    /// <param name="monthGanZhi">月柱</param>
    /// <param name="dayGanZhi">日柱</param>
    /// <param name="timeGanZhi">时柱</param>
    /// <param name="sect">流派，2晚子时日柱按当天，1晚子时日柱按明天</param>
    /// <param name="baseYear">起始年</param>
    /// <returns>符合的阳历列表</returns>
    public static List<Solar> FromBaZi( string yearGanZhi , string monthGanZhi , string dayGanZhi , string timeGanZhi , int sect = 2 , int baseYear = 1900 ) {
        sect = ( 1 == sect ) ? 1 : 2;
        List<Solar> l = new List<Solar>();
        List<int> years = new List<int>();
        Solar today = new Solar();
        int offsetYear = LunarUtil.GetJiaZiIndex( today.ToLunar().YearInGanZhiExact ) - LunarUtil.GetJiaZiIndex( yearGanZhi );
        if( offsetYear < 0 ) {
            offsetYear += 60;
        }
        int startYear = today.Year - offsetYear - 1;
        int minYear = baseYear - 2;
        while( startYear >= minYear ) {
            years.Add( startYear );
            startYear -= 60;
        }
        List<int> hours = new List<int>();
        string timeZhi = timeGanZhi.Substring( 1 );
        for( int i = 1, j = LunarUtil.ZHI.Length ; i < j ; i++ ) {
            if( LunarUtil.ZHI[ i ].Equals( timeZhi ) ) {
                hours.Add( ( i - 1 ) * 2 );
            }
        }

        if( "子".Equals( timeZhi ) ) {
            hours.Add( 23 );
        }

        foreach( int hour in hours ) {
            foreach( int y in years ) {
                int maxYear = y + 3;
                int year = y;
                int month = 11;
                if( year < baseYear ) {
                    year = baseYear;
                    month = 1;
                }
                Solar solar = new Solar( year , month , 1 , hour );
                while( solar.Year <= maxYear ) {
                    Lunar lunar = solar.ToLunar();
                    string dgz = ( 2 == sect ) ? lunar.DayInGanZhiExact2 : lunar.DayInGanZhiExact;
                    if( lunar.YearInGanZhiExact.Equals( yearGanZhi ) && lunar.MonthInGanZhiExact.Equals( monthGanZhi ) && dgz.Equals( dayGanZhi ) && lunar.TimeInGanZhi.Equals( timeGanZhi ) ) {
                        l.Add( solar );
                        break;
                    }
                    solar = solar.Next( 1 );
                }
            }
        }
        return l;
    }

    #endregion

    #region 方法

    /// <summary>
    /// 阳历日期相减，获得相差天数
    /// </summary>
    /// <param name="solar">阳历</param>
    /// <returns>天数</returns>
    public int Subtract( Solar solar ) {
        return SolarUtil.GetDaysBetween( solar.Year , solar.Month , solar.Day , Year , Month , Day );
    }

    /// <summary>
    /// 阳历日期相减，获得相差分钟数
    /// </summary>
    /// <param name="solar">阳历</param>
    /// <returns>分钟数</returns>
    public int SubtractMinute( Solar solar ) {
        int days = Subtract( solar );
        int cm = Hour * 60 + Minute;
        int sm = solar.Hour * 60 + solar.Minute;
        int m = cm - sm;
        if( m < 0 ) {
            m += 1440;
            days--;
        }
        m += days * 1440;
        return m;
    }

    /// <summary>
    /// 是否在指定日期之后
    /// </summary>
    /// <param name="solar">阳历</param>
    /// <returns>true/false</returns>
    public bool IsAfter( Solar solar ) {
        if( Year > solar.Year ) {
            return true;
        }
        if( Year < solar.Year ) {
            return false;
        }
        if( Month > solar.Month ) {
            return true;
        }
        if( Month < solar.Month ) {
            return false;
        }
        if( Day > solar.Day ) {
            return true;
        }
        if( Day < solar.Day ) {
            return false;
        }
        if( Hour > solar.Hour ) {
            return true;
        }
        if( Hour < solar.Hour ) {
            return false;
        }
        if( Minute > solar.Minute ) {
            return true;
        }
        if( Minute < solar.Minute ) {
            return false;
        }
        return Second > solar.Second;
    }

    /// <summary>
    /// 是否在指定日期之前
    /// </summary>
    /// <param name="solar">阳历</param>
    /// <returns>true/false</returns>
    public bool IsBefore( Solar solar ) {
        if( Year > solar.Year ) {
            return false;
        }
        if( Year < solar.Year ) {
            return true;
        }
        if( Month > solar.Month ) {
            return false;
        }
        if( Month < solar.Month ) {
            return true;
        }
        if( Day > solar.Day ) {
            return false;
        }
        if( Day < solar.Day ) {
            return true;
        }
        if( Hour > solar.Hour ) {
            return false;
        }
        if( Hour < solar.Hour ) {
            return true;
        }
        if( Minute > solar.Minute ) {
            return false;
        }
        if( Minute < solar.Minute ) {
            return true;
        }
        return Second < solar.Second;
    }

    /// <summary>
    /// 年推移
    /// </summary>
    /// <param name="years">年数</param>
    /// <returns>阳历</returns>
    public Solar NextYear( int years ) {
        int y = Year + years;
        int m = Month;
        int d = Day;
        if( 1582 == y && 10 == m ) {
            if( d > 4 && d < 15 ) {
                d += 10;
            }
        } else if( 2 == m ) {
            if( d > 28 ) {
                if( !SolarUtil.IsLeapYear( y ) ) {
                    d = 28;
                }
            }
        }
        return new Solar( y , m , d , Hour , Minute , Second );
    }

    /// <summary>
    /// 月推移
    /// </summary>
    /// <param name="months">月数</param>
    /// <returns>阳历</returns>
    public Solar NextMonth( int months ) {
        SolarMonth month = SolarMonth.FromYm( Year , Month ).Next( months );
        int y = month.Year;
        int m = month.Month;
        int d = Day;
        if( 1582 == y && 10 == m ) {
            if( d > 4 && d < 15 ) {
                d += 10;
            }
        } else {
            int maxDay = SolarUtil.GetDaysOfMonth( y , m );
            if( d > maxDay ) {
                d = maxDay;
            }
        }
        return new Solar( y , m , d , Hour , Minute , Second );
    }

    /// <summary>
    /// 日推移
    /// </summary>
    /// <param name="days">天数</param>
    /// <returns>阳历</returns>
    public Solar NextDay( int days ) {
        int y = Year;
        int m = Month;
        int d = Day;
        if( 1582 == y && 10 == m ) {
            if( d > 4 ) {
                d -= 10;
            }
        }
        if( days > 0 ) {
            d += days;
            int daysInMonth = SolarUtil.GetDaysOfMonth( y , m );
            while( d > daysInMonth ) {
                d -= daysInMonth;
                m++;
                if( m > 12 ) {
                    m = 1;
                    y++;
                }
                daysInMonth = SolarUtil.GetDaysOfMonth( y , m );
            }
        } else if( days < 0 ) {
            while( d + days <= 0 ) {
                m--;
                if( m < 1 ) {
                    m = 12;
                    y--;
                }
                d += SolarUtil.GetDaysOfMonth( y , m );
            }
            d += days;
        }
        if( 1582 == y && 10 == m ) {
            if( d > 4 ) {
                d += 10;
            }
        }
        return new Solar( y , m , d , Hour , Minute , Second );
    }

    /// <summary>
    /// 获取往后推几天的阳历日期，如果要往前推，则天数用负数
    /// </summary>
    /// <param name="days">天数</param>
    /// <param name="onlyWorkday">是否仅工作日</param>
    /// <returns>阳历日期</returns>
    public Solar Next( int days , bool onlyWorkday = false ) {
        if( !onlyWorkday ) {
            return NextDay( days );
        }
        Solar solar = new Solar( Year , Month , Day , Hour , Minute , Second );
        if( days != 0 ) {
            int rest = SystemMath.Abs( days );
            int add = days < 0 ? -1 : 1;
            while( rest > 0 ) {
                solar = solar.Next( add );
                bool work = true;
                Holiday holiday = Holiday.Get( solar.Year , solar.Month , solar.Day );
                if( null == holiday ) {
                    int week = solar.Week;
                    if( 0 == week || 6 == week ) {
                        work = false;
                    }
                } else {
                    work = holiday.Work;
                }
                if( work ) {
                    rest -= 1;
                }
            }
        }
        return solar;
    }

    /// <summary>
    /// 小时推移
    /// </summary>
    /// <param name="hours">小时数</param>
    /// <returns>阳历</returns>
    public Solar NextHour( int hours ) {
        int h = Hour + hours;
        int n = h < 0 ? -1 : 1;
        int hour = SystemMath.Abs( h );
        int days = hour / 24 * n;
        hour = ( hour % 24 ) * n;
        if( hour < 0 ) {
            hour += 24;
            days--;
        }
        Solar solar = Next( days );
        return new Solar( solar.Year , solar.Month , solar.Day , hour , solar.Minute , solar.Second );
    }

    /// <summary>
    /// 获取薪资比例(感谢 https://gitee.com/smr1987)
    /// </summary>
    /// <returns>薪资比例：1/2/3</returns>
    public int GetSalaryRate() {
        switch( Month ) {
            // 元旦节
            case 1 when Day == 1:
            // 劳动节
            case 5 when Day == 1:
            // 国庆
            case 10 when Day >= 1 && Day <= 3:
                return 3;
        }

        Lunar lunar = this.ToLunar();
        int lunarMonth = lunar.Month;
        int lunarDay = lunar.Day;
        switch( lunarMonth ) {
            // 春节
            case 1 when lunarDay >= 1 && lunarDay <= 3:
            // 端午
            case 5 when lunarDay == 5:
            // 中秋
            case 8 when lunarDay == 15:
                return 3;
        }

        // 清明
        if( "清明".Equals( lunar.JieQi ) ) {
            return 3;
        }
        Holiday holiday = Holiday.Get( Year , Month , Day );
        if( null != holiday ) {
            // 法定假日非上班
            if( !holiday.Work ) {
                return 2;
            }
        } else {
            // 周末
            int week = Week;
            if( week == 6 || week == 0 ) {
                return 2;
            }
        }
        // 工作日
        return 1;
    }

    #endregion

    #region 格式化输出

    /// <summary>
    /// yyyy-MM-dd
    /// </summary>
    public string Ymd {
        get {
            string y = Year + "";
            while( y.Length < 4 ) {
                y = "0" + y;
            }
            return y + "-" + ( Month < 10 ? "0" : "" ) + Month + "-" + ( Day < 10 ? "0" : "" ) + Day;
        }
    }

    /// <summary>
    /// yyyy-MM-dd HH:mm:ss
    /// </summary>
    public string YmdHms => Ymd + " " + ( Hour < 10 ? "0" : "" ) + Hour + ":" + ( Minute < 10 ? "0" : "" ) + Minute + ":" + ( Second < 10 ? "0" : "" ) + Second;

    /// <inheritdoc />
    public override string ToString() {
        return Ymd;
    }

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString {
        get {
            StringBuilder s = new StringBuilder();
            s.Append( YmdHms );
            if( LeapYear ) {
                s.Append( ' ' );
                s.Append( "闰年" );
            }
            s.Append( ' ' );
            s.Append( "星期" );
            s.Append( WeekInChinese );

            s.Append( ' ' );
            s.Append( XingZuo );
            s.Append( '座' );
            return s.ToString();
        }
    }

    #endregion
}
