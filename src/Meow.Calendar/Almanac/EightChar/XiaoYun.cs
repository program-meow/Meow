// ReSharper disable IdentifierTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Meow.Calendar.Almanac.EightChar;

/// <summary>
/// 小运
/// </summary>
public class XiaoYun {

    /// <summary>
    /// 序数，0-9
    /// </summary>
    private readonly int _index;

    /// <summary>
    /// 大运
    /// </summary>
    private readonly DaYun _daYun;

    /// <summary>
    /// 阴历
    /// </summary>
    private readonly Lunar _lunar;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="daYun">大运</param>
    /// <param name="index">序数，0-9</param>
    /// <param name="forward">是否顺推</param>
    public XiaoYun( DaYun daYun , int index , bool forward ) {
        _daYun = daYun;
        _lunar = daYun._lunar;
        _index = index;
        Year = daYun.StartYear + index;
        Age = daYun.StartAge + index;
        Forward = forward;
    }

    /// <summary>
    /// 年
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// 年龄
    /// </summary>
    public int Age { get; }

    /// <summary>
    /// 是否顺推
    /// </summary>
    public bool Forward { get; }

    /// <summary>
    /// 干支
    /// </summary>
    public string GanZhi {
        get {
            int offset = LunarUtil.GetJiaZiIndex( _lunar.TimeInGanZhi );
            int add = _index + 1;
            if( _daYun._index > 0 ) {
                add += _daYun.StartAge - 1;
            }
            offset += Forward ? add : -add;
            int size = LunarUtil.JIA_ZI.Length;
            while( offset < 0 ) {
                offset += size;
            }
            offset %= size;
            return LunarUtil.JIA_ZI[ offset ];
        }
    }

    /// <summary>
    /// 旬
    /// </summary>
    public string Xun => LunarUtil.GetXun( GanZhi );

    /// <summary>
    /// 旬空(空亡)
    /// </summary>
    public string XunKong => LunarUtil.GetXunKong( GanZhi );
}
