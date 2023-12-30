// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Meow.Calendar;

/// <summary>
/// 农历年
/// </summary>
public class LunarYear {

    /// <summary>
    /// 天干下标
    /// </summary>
    internal readonly int _ganIndex;

    /// <summary>
    /// 地支下标
    /// </summary>
    internal readonly int _zhiIndex;

    /// <summary>
    /// 缓存年
    /// </summary>
    private static LunarYear cacheYear;

    private static readonly Mutex mutex = new Mutex();

    /// <summary>
    /// 通过农历年初始化
    /// </summary>
    /// <param name="lunarYear">农历年</param>
    public LunarYear( int lunarYear ) {
        Year = lunarYear;
        int offset = lunarYear - 4;
        int yearGanIndex = offset % 10;
        int yearZhiIndex = offset % 12;
        if( yearGanIndex < 0 ) {
            yearGanIndex += 10;
        }
        if( yearZhiIndex < 0 ) {
            yearZhiIndex += 12;
        }
        _ganIndex = yearGanIndex;
        _zhiIndex = yearZhiIndex;
        Compute();
    }

    /// <summary>
    /// 年
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// 月
    /// </summary>
    public List<LunarMonth> Months { get; } = new List<LunarMonth>();

    /// <summary>
    /// 节气儒略日
    /// </summary>
    public List<double> JieQiJulianDays { get; } = new List<double>();

    /// <summary>
    /// 通过农历年初始化
    /// </summary>
    /// <param name="lunarYear">农历年</param>
    /// <returns>农历年</returns>
    public static LunarYear FromYear( int lunarYear ) {
        mutex.WaitOne();
        try {
            LunarYear y;
            if( null == cacheYear || cacheYear.Year != lunarYear ) {
                y = new LunarYear( lunarYear );
                cacheYear = y;
            } else {
                y = cacheYear;
            }
            return y;
        } finally {
            mutex.ReleaseMutex();
        }
    }

    /// <summary>
    /// 计算
    /// </summary>
    private void Compute() {
        // 节气
        double[] jq = new double[ 27 ];
        // 合朔，即每月初一
        double[] hs = new double[ 16 ];
        // 每月天数
        int[] dayCounts = new int[ 15 ];
        int[] months = new int[ 15 ];

        int currentYear = Year;

        double jd = SystemMath.Floor( ( currentYear - 2000 ) * 365.2422 + 180 );
        // 355是2000.12冬至，得到较靠近jd的冬至估计值
        double w = SystemMath.Floor( ( jd - 355 + 183 ) / 365.2422 ) * 365.2422 + 355;
        if( ShouXingUtil.CalcQi( w ) > jd ) {
            w -= 365.2422;
        }

        // 25个节气时刻(北京时间)，从冬至开始到下一个冬至以后
        for( int i = 0 ; i < 26 ; i++ ) {
            jq[ i ] = ShouXingUtil.CalcQi( w + 15.2184 * i );
        }

        // 从上年的大雪到下年的立春
        for( int i = 0, j = LunarUtil.JIE_QI_IN_USE.Length ; i < j ; i++ ) {
            if( i == 0 ) {
                jd = ShouXingUtil.QiAccurate2( jq[ 0 ] - 15.2184 );
            } else if( i <= 26 ) {
                jd = ShouXingUtil.QiAccurate2( jq[ i - 1 ] );
            } else {
                jd = ShouXingUtil.QiAccurate2( jq[ 25 ] + 15.2184 * ( i - 26 ) );
            }

            JieQiJulianDays.Add( jd + SolarUtil.J2000 );
        }

        // 冬至前的初一，今年"首朔"的日月黄经差w
        w = ShouXingUtil.CalcShuo( jq[ 0 ] );
        if( w > jq[ 0 ] ) {
            w -= 29.53;
        }

        // 递推每月初一
        for( int i = 0 ; i < 16 ; i++ ) {
            hs[ i ] = ShouXingUtil.CalcShuo( w + 29.5306 * i );
        }

        // 每月
        for( int i = 0 ; i < 15 ; i++ ) {
            dayCounts[ i ] = ( int ) ( hs[ i + 1 ] - hs[ i ] );
            months[ i ] = i;
        }

        int prevYear = currentYear - 1;
        int leapIndex = 16;
        if( LunarUtil.LEAP_11.Contains( currentYear ) ) {
            leapIndex = 13;
        } else if( LunarUtil.LEAP_12.Contains( currentYear ) ) {
            leapIndex = 14;
        } else if( hs[ 13 ] <= jq[ 24 ] ) {
            int i = 1;
            while( hs[ i + 1 ] > jq[ 2 * i ] && i < 13 ) {
                i++;
            }

            leapIndex = i;
        }

        for( int i = leapIndex ; i < 15 ; i++ ) {
            months[ i ] -= 1;
        }

        int fm = -1;
        int index = -1;
        int y = prevYear;
        for( int i = 0 ; i < 15 ; i++ ) {
            double dm = hs[ i ] + SolarUtil.J2000;
            int v2 = months[ i ];
            int mc = LunarUtil.YMC[ v2 % 12 ];
            if( 1724360 <= dm && dm < 1729794 ) {
                mc = LunarUtil.YMC[ ( v2 + 1 ) % 12 ];
            } else if( 1807724 <= dm && dm < 1808699 ) {
                mc = LunarUtil.YMC[ ( v2 + 1 ) % 12 ];
            } else if( SystemMath.Abs( dm - 1729794 ) < 0.00000000001 || SystemMath.Abs( dm - 1808699 ) < 0.00000000001 ) {
                mc = 12;
            }

            if( fm == -1 ) {
                fm = mc;
                index = mc;
            }

            if( mc < fm ) {
                y += 1;
                index = 1;
            }

            fm = mc;
            if( i == leapIndex ) {
                mc = -mc;
            } else if( SystemMath.Abs( dm - 1729794 ) < 0.00000000001 || SystemMath.Abs( dm - 1808699 ) < 0.00000000001 ) {
                mc = -11;
            }

            Months.Add( new LunarMonth( y , mc , dayCounts[ i ] , hs[ i ] + SolarUtil.J2000 , index ) );
            index++;
        }
    }

    /// <summary>
    /// 天干
    /// </summary>
    public string Gan => LunarUtil.GAN[ _ganIndex + 1 ];

    /// <summary>
    /// 地支
    /// </summary>
    public string Zhi => LunarUtil.ZHI[ _zhiIndex + 1 ];

    /// <summary>
    /// 获取干支
    /// </summary>
    public string GanZhi => $"{Gan}{Zhi}";

    /// <summary>
    /// 获取月份
    /// </summary>
    /// <param name="lunarMonth">月</param>
    /// <returns>农历月</returns>
    public LunarMonth GetMonth( int lunarMonth ) {
        return Months.FirstOrDefault( m => m.Year == Year && m.Month == lunarMonth );
    }

    /// <summary>
    /// 闰月
    /// </summary>
    public int LeapMonth => ( from m in Months where m.Year == Year && m.Leap select SystemMath.Abs( m.Month ) ).FirstOrDefault();

    /// <summary>
    /// 获取总天数
    /// </summary>
    public int DayCount => Months.Where( m => m.Year == Year ).Sum( m => m.DayCount );

    /// <summary>
    /// 获取当年的农历月们
    /// </summary>
    public List<LunarMonth> MonthsInYear => Months.Where( m => m.Year == Year ).ToList();

    /// <inheritdoc />
    public override string ToString() {
        return $"{Year}";
    }

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString => $"{Year}年";

    /// <summary>
    /// 按天干计算灶马头
    /// </summary>
    /// <param name="index">天干序号</param>
    /// <param name="name">名称</param>
    /// <returns>灶马头</returns>
    protected string GetZaoByGan( int index , string name ) {
        int offset = index - Solar.FromJulianDay( GetMonth( 1 ).FirstJulianDay ).ToLunar()._dayGanIndex;
        if( offset < 0 ) {
            offset += 10;
        }
        return new Regex( "几" , RegexOptions.Singleline ).Replace( name , LunarUtil.NUMBER[ offset + 1 ] , 1 );
    }

    /// <summary>
    /// 按地支计算灶马头
    /// </summary>
    /// <param name="index">地支序号</param>
    /// <param name="name">名称</param>
    /// <returns>灶马头</returns>
    protected string GetZaoByZhi( int index , string name ) {
        int offset = index - Solar.FromJulianDay( GetMonth( 1 ).FirstJulianDay ).ToLunar()._dayZhiIndex;
        if( offset < 0 ) {
            offset += 12;
        }
        return new Regex( "几" , RegexOptions.Singleline ).Replace( name , LunarUtil.NUMBER[ offset + 1 ] , 1 );
    }

    /// <summary>
    /// 几鼠偷粮
    /// </summary>
    /// <returns>几鼠偷粮</returns>
    public string TouLiang => GetZaoByZhi( 0 , "几鼠偷粮" );

    /// <summary>
    /// 草子几分
    /// </summary>
    public string CaoZi => GetZaoByZhi( 0 , "草子几分" );

    /// <summary>
    /// 几牛耕田（正月第一个丑日是初几，就是几牛耕田）
    /// </summary>
    public string GengTian => GetZaoByZhi( 1 , "几牛耕田" );

    /// <summary>
    /// 花收几分
    /// </summary>
    public string HuaShou => GetZaoByZhi( 3 , "花收几分" );

    /// <summary>
    /// 几龙治水（正月第一个辰日是初几，就是几龙治水）
    /// </summary>
    public string ZhiShui => GetZaoByZhi( 4 , "几龙治水" );

    /// <summary>
    /// 几马驮谷
    /// </summary>
    public string TuoGu => GetZaoByZhi( 6 , "几马驮谷" );

    /// <summary>
    /// 几鸡抢米
    /// </summary>
    public string QiangMi => GetZaoByZhi( 9 , "几鸡抢米" );

    /// <summary>
    /// 几姑看蚕
    /// </summary>
    public string KanCan => GetZaoByZhi( 9 , "几姑看蚕" );

    /// <summary>
    /// 几屠共猪
    /// </summary>
    public string GongZhu => GetZaoByZhi( 11 , "几屠共猪" );

    /// <summary>
    /// 甲田几分
    /// </summary>
    public string JiaTian => GetZaoByGan( 0 , "甲田几分" );

    /// <summary>
    /// 几人分饼（正月第一个丙日是初几，就是几人分饼）
    /// </summary>
    public string FenBing => GetZaoByGan( 2 , "几人分饼" );

    /// <summary>
    /// 几日得金（正月第一个辛日是初几，就是几日得金）
    /// </summary>
    public string DeJin => GetZaoByGan( 7 , "几日得金" );

    /// <summary>
    /// 几人几丙
    /// </summary>
    public string RenBing => GetZaoByGan( 2 , GetZaoByZhi( 2 , "几人几丙" ) );

    /// <summary>
    /// 几人几锄
    /// </summary>
    public string RenChu => GetZaoByGan( 3 , GetZaoByZhi( 2 , "几人几锄" ) );

    /// <summary>
    /// 三元
    /// </summary>
    public string Yuan => LunarUtil.YUAN[ ( Year + 2696 ) / 60 % 3 ] + "元";

    /// <summary>
    /// 九运
    /// </summary>
    public string Yun => LunarUtil.YUN[ ( Year + 2696 ) / 20 % 9 ] + "运";

    /// <summary>
    /// 九星
    /// </summary>
    public NineStar NineStar {
        get {
            int index = LunarUtil.GetJiaZiIndex( GanZhi ) + 1;
            int yuan = ( Year + 2696 ) / 60 % 3;
            int offset = ( 62 + yuan * 3 - index ) % 9;
            if( 0 == offset ) {
                offset = 9;
            }
            return NineStar.FromIndex( offset - 1 );
        }
    }

    /// <summary>
    /// 喜神方位，如艮
    /// </summary>
    public string PositionXi => LunarUtil.POSITION_XI[ _ganIndex + 1 ];

    /// <summary>
    /// 喜神方位描述，如东北
    /// </summary>
    public string PositionXiDesc => LunarUtil.POSITION_DESC[ PositionXi ];

    /// <summary>
    /// 阳贵神方位，如艮
    /// </summary>
    public string PositionYangGui => LunarUtil.POSITION_YANG_GUI[ _ganIndex + 1 ];

    /// <summary>
    /// 阳贵神方位描述，如东北
    /// </summary>
    public string PositionYangGuiDesc => LunarUtil.POSITION_DESC[ PositionYangGui ];

    /// <summary>
    /// 阴贵神方位，如艮
    /// </summary>
    public string PositionYinGui => LunarUtil.POSITION_YIN_GUI[ _ganIndex + 1 ];

    /// <summary>
    /// 阴贵神方位描述，如东北
    /// </summary>
    public string PositionYinGuiDesc => LunarUtil.POSITION_DESC[ PositionYinGui ];

    /// <summary>
    /// 福神方位，如艮（流派2）
    /// </summary>
    public string PositionFu => GetPositionFu();

    /// <summary>
    /// 福神方位描述，如东北（流派2）
    /// </summary>
    public string PositionFuDesc => GetPositionFuDesc();

    /// <summary>
    /// 获取福神方位
    /// </summary>
    /// <param name="sect">流派，1或2</param>
    /// <returns>方位，如艮</returns>
    public string GetPositionFu( int sect = 2 ) {
        return ( 1 == sect ? LunarUtil.POSITION_FU : LunarUtil.POSITION_FU_2 )[ _ganIndex + 1 ];
    }

    /// <summary>
    /// 获取福神方位描述
    /// </summary>
    /// <param name="sect">流派，1或2</param>
    /// <returns>方位描述，如东北</returns>
    public string GetPositionFuDesc( int sect = 2 ) {
        return LunarUtil.POSITION_DESC[ GetPositionFu( sect ) ];
    }

    /// <summary>
    /// 财神方位，如艮
    /// </summary>
    public string PositionCai => LunarUtil.POSITION_CAI[ _ganIndex + 1 ];

    /// <summary>
    /// 财神方位描述，如东北
    /// </summary>
    public string PositionCaiDesc => LunarUtil.POSITION_DESC[ PositionCai ];

    /// <summary>
    /// 太岁方位，如艮
    /// </summary>
    public string PositionTaiSui => LunarUtil.POSITION_TAI_SUI_YEAR[ _zhiIndex ];

    /// <summary>
    /// 太岁方位描述，如东北
    /// </summary>
    public string PositionTaiSuiDesc => LunarUtil.POSITION_DESC[ PositionTaiSui ];

    /// <summary>
    /// 往后推几年的阴历年，如果要往前推，则年数用负数
    /// </summary>
    /// <param name="n">年数</param>
    /// <returns>阴历年</returns>
    public LunarYear Next( int n ) {
        return FromYear( Year + n );
    }
}
