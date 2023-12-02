namespace Meow.Calendar;

/// <summary>
/// 日历
/// </summary>
public class Calendar {

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
}
