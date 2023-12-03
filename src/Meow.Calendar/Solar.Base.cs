// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Meow.Calendar;

/// <summary>
/// 阳历日期
/// </summary>
public partial class Solar {

    #region 构造函数

    /// <summary>
    /// 默认使用当前日期初始化
    /// </summary>
    public Solar() : this( DateTime.Now ) {
    }

    /// <summary>
    /// 通过年月日时分初始化
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月，1到12</param>
    /// <param name="day">日，1到31</param>
    /// <param name="hour">小时，0到23</param>
    /// <param name="minute">分钟，0到59</param>
    /// <param name="second">秒钟，0到59</param>
    public Solar( int year , int month , int day , int hour = 0 , int minute = 0 , int second = 0 ) {
        if( 1582 == year && 10 == month ) {
            if( day > 4 && day < 15 ) {
                throw new ArgumentException( $"wrong solar year {year} month {month} day {day}" );
            }
        }

        if( month < 1 || month > 12 ) {
            throw new ArgumentException( $"wrong month {month}" );
        }

        if( day < 1 || day > 31 ) {
            throw new ArgumentException( $"wrong day {day}" );
        }

        if( hour < 0 || hour > 23 ) {
            throw new ArgumentException( $"wrong hour {hour}" );
        }

        if( minute < 0 || minute > 59 ) {
            throw new ArgumentException( $"wrong minute {minute}" );
        }

        if( second < 0 || second > 59 ) {
            throw new ArgumentException( $"wrong second {second}" );
        }

        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    /// <summary>
    /// 通过日期初始化
    /// </summary>
    /// <param name="date">日期字符串，2023-12-02、20231202、20231202101010、2023-12-02 12:12:12</param>
    public Solar( string date ) : this( date.ToDateTime() ) {
    }

    /// <summary>
    /// 通过日期初始化
    /// </summary>
    /// <param name="date">日期</param>
    public Solar( DateTime date ) : this( date.Year , date.Month , date.Day , date.Hour , date.Minute , date.Second ) {
    }

    /// <summary>
    /// 通过儒略日初始化
    /// </summary>
    /// <param name="julianDay">儒略日</param>
    public Solar( double julianDay ) {
        var d = ( int ) ( julianDay + 0.5 );
        var f = julianDay + 0.5 - d;

        if( d >= 2299161 ) {
            var c = ( int ) ( ( d - 1867216.25 ) / 36524.25 );
            d += 1 + c - ( int ) ( c * 1D / 4 );
        }
        d += 1524;
        var year = ( int ) ( ( d - 122.1 ) / 365.25 );
        d -= ( int ) ( 365.25 * year );
        var month = ( int ) ( d * 1D / 30.601 );
        d -= ( int ) ( 30.601 * month );
        var day = d;
        if( month > 13 ) {
            month -= 13;
            year -= 4715;
        } else {
            month -= 1;
            year -= 4716;
        }
        f *= 24;
        var hour = ( int ) f;

        f -= hour;
        f *= 60;
        var minute = ( int ) f;

        f -= minute;
        f *= 60;
        var second = ( int ) SystemMath.Round( f );

        if( second > 59 ) {
            second -= 60;
            minute++;
        }
        if( minute > 59 ) {
            minute -= 60;
            hour++;
        }
        if( hour > 23 ) {
            hour -= 24;
            day += 1;
        }
        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    #endregion

    #region 基础属性

    /// <summary>
    /// 年
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// 月
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// 日
    /// </summary>
    public int Day { get; }

    /// <summary>
    /// 时
    /// </summary>
    public int Hour { get; }

    /// <summary>
    /// 分
    /// </summary>
    public int Minute { get; }

    /// <summary>
    /// 秒
    /// </summary>
    public int Second { get; }

    #endregion

    #region 扩展属性

    /// <summary>
    /// 是否闰年
    /// </summary>
    public bool LeapYear => SolarUtil.IsLeapYear( Year );

    /// <summary>
    /// 星期，0代表周日，1代表周一
    /// </summary>
    public int Week => ( ( int ) ( JulianDay + 0.5 ) + 7000001 ) % 7;

    /// <summary>
    /// 星期的中文：日一二三四五六
    /// </summary>
    public string WeekInChinese => SolarUtil.WEEK[ Week ];

    /// <summary>
    /// 星座
    /// </summary>
    public string XingZuo {
        get {
            var index = 11;
            var y = Month * 100 + Day;
            if( y >= 321 && y <= 419 ) {
                index = 0;
            } else if( y >= 420 && y <= 520 ) {
                index = 1;
            } else if( y >= 521 && y <= 621 ) {
                index = 2;
            } else if( y >= 622 && y <= 722 ) {
                index = 3;
            } else if( y >= 723 && y <= 822 ) {
                index = 4;
            } else if( y >= 823 && y <= 922 ) {
                index = 5;
            } else if( y >= 923 && y <= 1023 ) {
                index = 6;
            } else if( y >= 1024 && y <= 1122 ) {
                index = 7;
            } else if( y >= 1123 && y <= 1221 ) {
                index = 8;
            } else if( y >= 1222 || y <= 119 ) {
                index = 9;
            } else if( y <= 218 ) {
                index = 10;
            }
            return SolarUtil.XING_ZUO[ index ];
        }
    }

    /// <summary>
    /// 儒略日
    /// </summary>
    public double JulianDay {
        get {
            var y = Year;
            var m = Month;
            var d = Day + ( ( Second * 1D / 60 + Minute ) / 60 + Hour ) / 24;
            var n = 0;
            var g = y * 372 + m * 31 + ( int ) d >= 588829;
            if( m <= 2 ) {
                m += 12;
                y--;
            }
            if( g ) {
                n = ( int ) ( y * 1D / 100 );
                n = 2 - n + ( int ) ( n * 1D / 4 );
            }
            return ( int ) ( 365.25 * ( y + 4716 ) ) + ( int ) ( 30.6001 * ( m + 1 ) ) + d + n - 1524.5;
        }
    }

    #endregion

    #region 节日

    /// <summary>
    /// 节日，有可能一天会有多个节日
    /// </summary>
    public List<string> Festivals {
        get {
            var l = new List<string>();
            //获取几月几日对应的节日
            try {
                l.Add( SolarUtil.FESTIVAL[ Month + "-" + Day ] );
            } catch {
                // ignored
            }

            //计算几月第几个星期几对应的节日
            var weeks = ( int ) SystemMath.Ceiling( Day / 7D );
            //星期几，0代表星期天
            try {
                l.Add( SolarUtil.WEEK_FESTIVAL[ Month + "-" + weeks + "-" + Week ] );
            } catch {
                // ignored
            }

            if( Day + 7 <= SolarUtil.GetDaysOfMonth( Year , Month ) ) return l;
            try {
                l.Add( SolarUtil.WEEK_FESTIVAL[ Month + "-0-" + Week ] );
            } catch {
                // ignored
            }

            return l;
        }
    }

    /// <summary>
    /// 非正式节日
    /// </summary>
    public List<string> OtherFestivals {
        get {
            var l = new List<string>();
            try {
                var fs = SolarUtil.OTHER_FESTIVAL[ Month + "-" + Day ];
                l.AddRange( fs );
            } catch {
                // ignored
            }
            return l;
        }
    }

    #endregion
}
