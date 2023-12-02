// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Meow.Calendar.Util;

/// <summary>
/// 八字 - 工具
/// </summary>
internal static class EightCharUtil {

    /// <summary>
    /// 月支，按正月起寅排列
    /// </summary>
    public static readonly string[] MONTH_ZHI = { "" , "寅" , "卯" , "辰" , "巳" , "午" , "未" , "申" , "酉" , "戌" , "亥" , "子" , "丑" };

    /// <summary>
    /// 长生十二神
    /// </summary>
    public static readonly string[] CHANG_SHENG = { "长生" , "沐浴" , "冠带" , "临官" , "帝旺" , "衰" , "病" , "死" , "墓" , "绝" , "胎" , "养" };

    /// <summary>
    /// 
    /// </summary>
    public static readonly Dictionary<string , int> CHANG_SHENG_OFFSET = new Dictionary<string , int>
    {
        //阳
        {"甲", 1},
        {"丙", 10},
        {"戊", 10},
        {"庚", 7},
        {"壬", 4},
        //阴
        {"乙", 6},
        {"丁", 9},
        {"己", 9},
        {"辛", 0},
        {"癸", 3}
    };

}
