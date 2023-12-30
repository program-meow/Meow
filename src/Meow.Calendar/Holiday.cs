namespace Meow.Calendar;

/// <summary>
/// 节假日
/// </summary>
public partial class Holiday {

    /// <summary>
    /// 数据
    /// </summary>
    private static readonly List<Holiday> _data = HolidayUtil.Data;

    /// <summary>
    /// 获取当前日期的节假日信息，如果不存在，返回null
    /// </summary>
    /// <returns>节假日</returns>
    public static Holiday Get() {
        DateTime date = Meow.Helper.Time.Now.ToLocalTime();
        return Get( date );
    }

    /// <summary>
    /// 获取指定年月日的节假日信息，如果不存在，返回null
    /// </summary>
    /// <param name="ymd">年月日，2023-12-02、20231202</param>
    /// <returns>节假日</returns>
    public static Holiday Get( string ymd ) {
        return Get( ymd.ToDateTime() );
    }

    /// <summary>
    /// 获取指定日期的节假日信息，如果不存在，返回null
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>节假日</returns>
    public static Holiday Get( DateTime date ) {
        return _data.FirstOrDefault( t => t.Year == date.Year && t.Month == date.Month && t.Day == date.Day );
    }

    /// <summary>
    /// 获取指定年月日的节假日信息，如果不存在，返回null
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月，1-12</param>
    /// <param name="day">日，1-31</param>
    /// <returns>节假日</returns>
    public static Holiday Get( int year , int month , int day ) {
        return _data.FirstOrDefault( t => t.Year == year && t.Month == month && t.Day == day );
    }

    /// <summary>
    /// 获取指定年月的节假日列表
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月，1-12</param>
    /// <returns>节假日列表</returns>
    public static List<Holiday> Get( int year , int month ) {
        return _data.Where( t => t.Year == year && t.Month == month ).ToList();
    }

    /// <summary>
    /// 获取指定年的节假日列表
    /// </summary>
    /// <param name="year">年</param>
    /// <returns>节假日列表</returns>
    public static List<Holiday> Get( int year ) {
        return _data.Where( t => t.Year == year ).ToList();
    }

    /// <summary>
    /// 获取指定范围时间的节假日列表
    /// </summary>
    /// <param name="startDate">起始时间</param>
    /// <param name="endDate">结束时间</param>
    /// <returns>节假日列表</returns>
    public static List<Holiday> Get( DateTime startDate , DateTime endDate ) {
        startDate = new DateTime( startDate.Year , startDate.Month , startDate.Day );
        return _data.Where( t => startDate <= t.Date && t.Date <= endDate ).ToList();
    }
}
