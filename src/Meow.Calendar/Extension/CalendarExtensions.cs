namespace Meow.Calendar.Extension;

/// <summary>
/// 日历扩展
/// </summary>
public static class CalendarExtensions {

    #region 转换为日历

    /// <summary>
    /// 转换为日历
    /// </summary>
    /// <param name="date">日期</param>
    public static Calendar ToCalendar( this DateTime? date ) {
        date.CheckNull( nameof( date ) );
        return date.SafeValue().ToCalendar();
    }

    /// <summary>
    /// 转换为日历
    /// </summary>
    /// <param name="date">日期</param>
    public static Calendar ToCalendar( this DateTime date ) {
        date.CheckNull( nameof( date ) );
        return Calendar.FromDate( date );
    }

    #endregion

    #region 转换为阳历

    /// <summary>
    /// 转换为阳历
    /// </summary>
    /// <param name="date">日期</param>
    public static Solar ToSolar( this DateTime? date ) {
        date.CheckNull( nameof( date ) );
        return date.SafeValue().ToSolar();
    }

    /// <summary>
    /// 转换为阳历
    /// </summary>
    /// <param name="date">日期</param>
    public static Solar ToSolar( this DateTime date ) {
        date.CheckNull( nameof( date ) );
        return Solar.FromDate( date );
    }

    /// <summary>
    /// 转换为阳历
    /// </summary>
    /// <param name="lunar">阴历</param>
    public static Solar ToSolar( this Lunar lunar ) {
        lunar.CheckNull( nameof( lunar ) );
        LunarYear y = LunarYear.FromYear( lunar.Year );
        LunarMonth m = y.GetMonth( lunar.Month );
        Solar noon = Solar.FromJulianDay( m.FirstJulianDay + lunar.Day - 1 );
        return Solar.FromYmdHms( noon.Year , noon.Month , noon.Day , lunar.Hour , lunar.Minute , lunar.Second );
    }

    #endregion

    #region 转换为农历

    /// <summary>
    /// 转换为农历
    /// </summary>
    /// <param name="date">日期</param>
    public static Lunar ToLunar( this DateTime? date ) {
        date.CheckNull( nameof( date ) );
        return date.SafeValue().ToLunar();
    }

    /// <summary>
    /// 转换为农历
    /// </summary>
    /// <param name="date">日期</param>
    public static Lunar ToLunar( this DateTime date ) {
        date.CheckNull( nameof( date ) );
        return Lunar.FromDate( date );
    }

    /// <summary>
    /// 转换为农历
    /// </summary>
    /// <param name="solar">阳历</param>
    public static Lunar ToLunar( this Solar solar ) {
        solar.CheckNull( nameof( solar ) );
        return new Lunar( solar );
    }

    #endregion

    #region 转换为八字

    /// <summary>
    /// 转换为八字
    /// </summary>
    /// <param name="date">日期</param>
    public static EightChar ToEightChar( this DateTime? date ) {
        date.CheckNull( nameof( date ) );
        return date.SafeValue().ToEightChar();
    }

    /// <summary>
    /// 转换为八字
    /// </summary>
    /// <param name="date">日期</param>
    public static EightChar ToEightChar( this DateTime date ) {
        date.CheckNull( nameof( date ) );
        return EightChar.FromDate( date );
    }

    #endregion

    #region 转换为道历

    /// <summary>
    /// 转换为道历
    /// </summary>
    /// <param name="date">日期</param>
    public static Dao ToDao( this DateTime? date ) {
        date.CheckNull( nameof( date ) );
        return date.SafeValue().ToDao();
    }

    /// <summary>
    /// 转换为道历
    /// </summary>
    /// <param name="date">日期</param>
    public static Dao ToDao( this DateTime date ) {
        date.CheckNull( nameof( date ) );
        return Dao.FromDate( date );
    }

    #endregion

    #region 转换为佛历

    /// <summary>
    /// 转换为佛历
    /// </summary>
    /// <param name="date">日期</param>
    public static Fo ToFo( this DateTime? date ) {
        date.CheckNull( nameof( date ) );
        return date.SafeValue().ToFo();
    }

    /// <summary>
    /// 转换为佛历
    /// </summary>
    /// <param name="date">日期</param>
    public static Fo ToFo( this DateTime date ) {
        date.CheckNull( nameof( date ) );
        return Fo.FromDate( date );
    }

    #endregion

    #region 转换为节假日

    /// <summary>
    /// 转换为节假日
    /// </summary>
    /// <param name="date">日期</param>
    public static Holiday ToHoliday( this DateTime? date ) {
        date.CheckNull( nameof( date ) );
        return date.SafeValue().ToHoliday();
    }

    /// <summary>
    /// 转换为节假日
    /// </summary>
    /// <param name="date">日期</param>
    public static Holiday ToHoliday( this DateTime date ) {
        date.CheckNull( nameof( date ) );
        return Holiday.Get( date );
    }

    #endregion
}