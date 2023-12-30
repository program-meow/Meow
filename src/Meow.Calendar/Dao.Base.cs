namespace Meow.Calendar;

/// <summary>
/// 道历
/// </summary>
public partial class Dao {

    /// <summary>
    /// 阴历
    /// </summary>
    private readonly Lunar _lunar;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    public Dao( DateTime date ) : this( new Lunar( date ) ) {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="lunar">阴历</param>
    public Dao( Lunar lunar ) {
        _lunar = lunar;
    }

    /// <summary>
    /// 年
    /// </summary>
    public int Year => _lunar.Year - DaoUtil.BIRTH_YEAR;

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
            char[] y = ( Year + "" ).ToCharArray();
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
    /// 节日
    /// </summary>
    public List<DaoFestival> Festivals {
        get {
            List<DaoFestival> l = new List<DaoFestival>();
            try {
                l.AddRange( DaoUtil.FESTIVAL[ Month + "-" + Day ] );
            } catch {
                // ignored
            }

            string jq = _lunar.JieQi;
            switch( jq ) {
                case "冬至":
                    l.Add( new DaoFestival( "元始天尊圣诞" ) );
                    break;
                case "夏至":
                    l.Add( new DaoFestival( "灵宝天尊圣诞" ) );
                    break;
            }

            // 八节日
            try {
                l.Add( new DaoFestival( DaoUtil.BA_JIE[ jq ] ) );
            } catch {
                // ignored
            }

            // 八会日
            try {
                l.Add( new DaoFestival( DaoUtil.BA_HUI[ _lunar.DayInGanZhi ] ) );
            } catch {
                // ignored
            }

            return l;
        }
    }
}
