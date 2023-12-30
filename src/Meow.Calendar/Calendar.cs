using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Meow.Calendar;

/// <summary>
/// 日历
/// </summary>
public class Calendar {

    /// <summary>
    /// 通过日期初始化
    /// </summary>
    /// <param name="date">日期字符串，2023-12-02、20231202、20231202101010、2023-12-02 12:12:12</param>
    public Calendar( string date ) : this( date.ToDateTime() ) {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    public Calendar( DateTime? date ) : this( date.SafeValue() ) { }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    public Calendar( DateTime date ) {
        date.CheckNull( nameof( date ) );
        Solar = Solar.FromDate( date );
        Lunar = Lunar.FromDate( date );
        EightChar = EightChar.FromDate( date );
        Dao = Dao.FromDate( date );
        Fo = Fo.FromDate( date );
        Holiday = Holiday.Get( date );
        AuxiliaryName = GetAuxiliaryDescription( date );
    }

    /// <summary>
    /// 通过指定日期获取日历
    /// </summary>
    /// <param name="date">日期字符串，2023-12-02、20231202、20231202101010、2023-12-02 12:12:12</param>
    /// <returns>日历</returns>
    public static Calendar FromDate( string date ) {
        return new Calendar( date );
    }

    /// <summary>
    /// 通过指定日期获取日历
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>日历</returns>
    public static Calendar FromDate( DateTime date ) {
        return new Calendar( date );
    }

    /// <summary>
    /// 阳历
    /// </summary>
    public Solar Solar { get; }

    /// <summary>
    /// 农历
    /// </summary>
    public Lunar Lunar { get; }

    /// <summary>
    /// 八字
    /// </summary>
    public EightChar EightChar { get; }

    /// <summary>
    /// 道历
    /// </summary>
    public Dao Dao { get; }

    /// <summary>
    /// 佛历
    /// </summary>
    public Fo Fo { get; }

    /// <summary>
    /// 节假日
    /// </summary>
    public Holiday Holiday { get; }

    /// <summary>
    /// 辅助名称
    /// </summary>
    public string AuxiliaryName { get; }

    /// <summary>
    /// 获取辅助描述
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>辅助描述</returns>
    private string GetAuxiliaryDescription( DateTime date ) {
        //农历
        string text = Lunar.Day == 1 ? $"{Lunar.MonthInChinese}月" : Lunar.DayInChinese;

        //节日
        string festival = Solar.Festivals.IsEmpty() ? "" : Solar.Festivals[ 0 ];
        text = festival.IsEmpty() ? text : festival;

        //节气
        string jieqi = Lunar.JieQi;
        text = jieqi.IsEmpty() ? text : jieqi;

        if( Holiday == null )
            return text;

        //假期
        if( date.Year == Holiday.TargetYear && date.Month == Holiday.TargetMonth && date.Day == Holiday.TargetDay )
            text = Holiday.NameDescription;
        return text;
    }

}
