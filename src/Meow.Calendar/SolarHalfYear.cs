// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Meow.Calendar;

/// <summary>
/// 阳历半年
/// </summary>
public class SolarHalfYear {

    /// <summary>
    /// 年
    /// </summary>
    private readonly int _year;

    /// <summary>
    /// 月
    /// </summary>
    private readonly int _month;

    /// <summary>
    /// 半年的月数
    /// </summary>
    private const int MONTH_COUNT = 6;

    /// <summary>
    /// 半年序号，从1开始
    /// </summary>
    private int _index => ( int ) SystemMath.Ceiling( _month * 1D / MONTH_COUNT );

    /// <summary>
    /// 默认当前日期
    /// </summary>
    public SolarHalfYear() : this( DateTime.Now ) {
    }

    /// <summary>
    /// 通过日期初始化
    /// </summary>
    /// <param name="date">日期</param>
    public SolarHalfYear( DateTime date ) : this( date.Year , date.Month ) {
    }

    /// <summary>
    /// 通过年月初始化
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    public SolarHalfYear( int year , int month ) {
        _year = year;
        _month = month;
    }

    /// <summary>
    /// 通过指定日期获取阳历半年
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>阳历半年</returns>
    public static SolarHalfYear FromDate( DateTime date ) {
        return new SolarHalfYear( date );
    }

    /// <summary>
    /// 通过指定年月获取阳历半年
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    /// <returns>阳历半年</returns>
    public static SolarHalfYear FromYm( int year , int month ) {
        return new SolarHalfYear( year , month );
    }

    /// <summary>
    /// 半年推移
    /// </summary>
    /// <param name="halfYears">推移的半年数，负数为倒推</param>
    /// <returns>推移后的半年</returns>
    public SolarHalfYear Next( int halfYears ) {
        var m = SolarMonth.FromYm( _year , _month ).Next( MONTH_COUNT * halfYears );
        return new SolarHalfYear( m.Year , m.Month );
    }

    /// <summary>
    /// 获取本半年的月份
    /// </summary>
    /// <returns>本半年的月份列表</returns>
    public List<SolarMonth> Months {
        get {
            var l = new List<SolarMonth>();
            var index = _index - 1;
            for( var i = 0 ; i < MONTH_COUNT ; i++ ) {
                l.Add( new SolarMonth( _year , MONTH_COUNT * index + i + 1 ) );
            }
            return l;
        }
    }

    /// <inheritdoc />
    public override string ToString() {
        return _year + "." + _index;
    }

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString => _year + "年" + ( _index == 1 ? "上" : "下" ) + "半年";
}
