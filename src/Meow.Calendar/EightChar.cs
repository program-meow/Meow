namespace Meow.Calendar;

/// <summary>
/// 八字
/// </summary>
public partial class EightChar {

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>道历</returns>
    public static EightChar FromDate( DateTime date ) {
        return new EightChar( date );
    }

    /// <summary>
    /// 从阴历创建八字
    /// </summary>
    /// <param name="lunar">阴历</param>
    /// <returns>八字</returns>
    public static EightChar FromLunar( Lunar lunar ) {
        return new EightChar( lunar );
    }

    #region 格式化输出

    /// <inheritdoc />
    public override string ToString() {
        return $"{Year} {Month} {Day} {Time}";
    }

    #endregion

    /// <summary>
    /// 获取运
    /// </summary>
    /// <param name="gender">性别：1男，0女</param>
    /// <param name="sect">流派，1按天数和时辰数计算，3天1年，1天4个月，1时辰10天；2按分钟数计算</param>
    /// <returns>运</returns>
    public Yun GetYun( int gender , int sect = 1 ) {
        return new Yun( this , gender , sect );
    }
}
