// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Meow.Calendar.Almanac;

/// <summary>
/// 九星
/// </summary>
public class NineStar {

    /// <summary>
    /// 序号
    /// </summary>
    private readonly int _index;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="index">序号</param>
    public NineStar( int index ) {
        _index = index;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="index">序号</param>
    /// <returns>九星</returns>
    public static NineStar FromIndex( int index ) {
        return new NineStar( index );
    }

    /// <summary>
    /// 九数
    /// </summary>
    public string Number => NineStarUtil.NUMBER[ _index ];

    /// <summary>
    /// 七色
    /// </summary>
    public string Color => NineStarUtil.COLOR[ _index ];

    /// <summary>
    /// 五行
    /// </summary>
    public string WuXing => NineStarUtil.WU_XING[ _index ];

    /// <summary>
    /// 方位
    /// </summary>
    public string Position => NineStarUtil.POSITION[ _index ];

    /// <summary>
    /// 方位描述
    /// </summary>
    public string PositionDesc => LunarUtil.POSITION_DESC[ Position ];

    /// <summary>
    /// 玄空九星名称
    /// </summary>
    public string NameInXuanKong => NineStarUtil.NAME_XUAN_KONG[ _index ];

    /// <summary>
    /// 北斗九星名称
    /// </summary>
    public string NameInBeiDou => NineStarUtil.NAME_BEI_DOU[ _index ];

    /// <summary>
    /// 奇门九星名称
    /// </summary>
    public string NameInQiMen => NineStarUtil.NAME_QI_MEN[ _index ];

    /// <summary>
    /// 太乙九神名称
    /// </summary>
    public string NameInTaiYi => NineStarUtil.NAME_TAI_YI[ _index ];

    /// <summary>
    /// 奇门九星吉凶
    /// </summary>
    public string LuckInQiMen => NineStarUtil.LUCK_QI_MEN[ _index ];

    /// <summary>
    /// 玄空九星吉凶
    /// </summary>
    public string LuckInXuanKong => NineStarUtil.LUCK_XUAN_KONG[ _index ];

    /// <summary>
    /// 奇门九星阴阳
    /// </summary>
    public string YinYangInQiMen => NineStarUtil.YIN_YANG_QI_MEN[ _index ];

    /// <summary>
    /// 太乙九神类型
    /// </summary>
    public string TypeInTaiYi => NineStarUtil.TYPE_TAI_YI[ _index ];

    /// <summary>
    /// 八门（奇门遁甲）
    /// </summary>
    public string BaMenInQiMen => NineStarUtil.BA_MEN_QI_MEN[ _index ];

    /// <summary>
    /// 太乙九神歌诀
    /// </summary>
    public string SongInTaiYi => NineStarUtil.SONG_TAI_YI[ _index ];

    /// <inheritdoc />
    public override string ToString() {
        return $"{Number}{Color}{WuXing}{NameInBeiDou}";
    }

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString {
        get {
            var s = new StringBuilder();
            s.Append( Number );
            s.Append( Color );
            s.Append( WuXing );
            s.Append( ' ' );
            s.Append( Position );
            s.Append( '(' );
            s.Append( PositionDesc );
            s.Append( ") " );
            s.Append( NameInBeiDou );
            s.Append( " 玄空[" );
            s.Append( NameInXuanKong );
            s.Append( ' ' );
            s.Append( LuckInXuanKong );
            s.Append( "] 奇门[" );
            s.Append( NameInQiMen );
            s.Append( ' ' );
            s.Append( LuckInQiMen );
            if( BaMenInQiMen.Length > 0 ) {
                s.Append( ' ' );
                s.Append( BaMenInQiMen );
                s.Append( '门' );
            }

            s.Append( ' ' );
            s.Append( YinYangInQiMen );
            s.Append( "] 太乙[" );
            s.Append( NameInTaiYi );
            s.Append( ' ' );
            s.Append( TypeInTaiYi );
            s.Append( ']' );
            return s.ToString();
        }
    }
}
