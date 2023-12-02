// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Meow.Calendar.Util;

/// <summary>
/// 道历 - 工具
/// </summary>
internal static class DaoUtil {

    /// <summary>
    /// 生年
    /// </summary>
    public const int BIRTH_YEAR = -2697;

    /// <summary>
    /// 三会日
    /// </summary>
    public static readonly string[] SAN_HUI = { "1-7" , "7-7" , "10-15" };

    /// <summary>
    /// 三元日
    /// </summary>
    public static readonly string[] SAN_YUAN = { "1-15" , "7-15" , "10-15" };

    /// <summary>
    /// 五腊日
    /// </summary>
    public static readonly string[] WU_LA = { "1-1" , "5-5" , "7-7" , "10-1" , "12-8" };

    /// <summary>
    /// 暗戊
    /// </summary>
    public static readonly string[] AN_WU = { "未" , "戌" , "辰" , "寅" , "午" , "子" , "酉" , "申" , "巳" , "亥" , "卯" , "丑" };

    /// <summary>
    /// 八会日
    /// </summary>
    public static readonly Dictionary<string , string> BA_HUI = new Dictionary<string , string>
    {
            { "丙午", "天会" },
            { "壬午", "地会" },
            { "壬子", "人会" },
            { "庚午", "日会" },
            { "庚申", "月会" },
            { "辛酉", "星辰会" },
            { "甲辰", "五行会" },
            { "甲戌", "四时会" }
        };

    /// <summary>
    /// 八节日
    /// </summary>
    public static readonly Dictionary<string , string> BA_JIE = new Dictionary<string , string>
    {
            {"立春", "东北方度仙上圣天尊同梵炁始青天君下降"},
            {"春分", "东方玉宝星上天尊同青帝九炁天君下降"},
            {"立夏", "东南方好生度命天尊同梵炁始丹天君下降"},
            {"夏至", "南方玄真万福天尊同赤帝三炁天君下降"},
            {"立秋", "西南方太灵虚皇天尊同梵炁始素天君下降"},
            {"秋分", "西方太妙至极天尊同白帝七炁天君下降"},
            {"立冬", "西北方无量太华天尊同梵炁始玄天君下降"},
            {"冬至", "北方玄上玉宸天尊同黑帝五炁天君下降"}
        };

    /// <summary>
    /// 节日
    /// </summary>
    public static readonly Dictionary<string , List<DaoFestival>> FESTIVAL = new Dictionary<string , List<DaoFestival>>
    {
            {"1-1", new List<DaoFestival>(new[] { new DaoFestival("天腊之辰", "天腊，此日五帝会于东方九炁青天") })},
            {"1-3", new List<DaoFestival>(new[] { new DaoFestival("郝真人圣诞"), new DaoFestival("孙真人圣诞") })},
            {"1-5", new List<DaoFestival>(new[] { new DaoFestival("孙祖清静元君诞") })},
            {"1-7", new List<DaoFestival>(new[] { new DaoFestival("举迁赏会", "此日上元赐福，天官同地水二官考校罪福") })},
            {"1-9", new List<DaoFestival>(new[] { new DaoFestival("玉皇上帝圣诞") })},
            {"1-13", new List<DaoFestival>(new[] { new DaoFestival("关圣帝君飞升") })},
            {"1-15", new List<DaoFestival>(new[] { new DaoFestival("上元天官圣诞"), new DaoFestival("老祖天师圣诞") })},
            {"1-19", new List<DaoFestival>(new[] { new DaoFestival("长春邱真人(邱处机)圣诞") })},
            {"1-28", new List<DaoFestival>(new[] { new DaoFestival("许真君(许逊天师)圣诞") })},
            {"2-1", new List<DaoFestival>(new[] { new DaoFestival("勾陈天皇大帝圣诞"), new DaoFestival("长春刘真人(刘渊然)圣诞") })},
            {"2-2", new List<DaoFestival>(new[] { new DaoFestival("土地正神诞"), new DaoFestival("姜太公圣诞") })},
            {"2-3", new List<DaoFestival>(new[] { new DaoFestival("文昌梓潼帝君圣诞") })},
            {"2-6", new List<DaoFestival>(new[] { new DaoFestival("东华帝君圣诞") })},
            {"2-13", new List<DaoFestival>(new[] { new DaoFestival("度人无量葛真君圣诞") })},
            {"2-15", new List<DaoFestival>(new[] { new DaoFestival("太清道德天尊(太上老君)圣诞") })},
            {"2-19", new List<DaoFestival>(new[] { new DaoFestival("慈航真人圣诞") })},
            {"3-1", new List<DaoFestival>(new[] { new DaoFestival("谭祖(谭处端)长真真人圣诞") })},
            {"3-3", new List<DaoFestival>(new[] { new DaoFestival("玄天上帝圣诞") })},
            {"3-6", new List<DaoFestival>(new[] { new DaoFestival("眼光娘娘圣诞") })},
            {"3-15", new List<DaoFestival>(new[] { new DaoFestival("天师张大真人圣诞"), new DaoFestival("财神赵公元帅圣诞") })},
            {"3-16", new List<DaoFestival>(new[] { new DaoFestival("三茅真君得道之辰"), new DaoFestival("中岳大帝圣诞") })},
            {"3-18", new List<DaoFestival>(new[] { new DaoFestival("王祖(王处一)玉阳真人圣诞"), new DaoFestival("后土娘娘圣诞") })},
            {"3-19", new List<DaoFestival>(new[] { new DaoFestival("太阳星君圣诞") })},
            {"3-20", new List<DaoFestival>(new[] { new DaoFestival("子孙娘娘圣诞") })},
            {"3-23", new List<DaoFestival>(new[] { new DaoFestival("天后妈祖圣诞") })},
            {"3-26", new List<DaoFestival>(new[] { new DaoFestival("鬼谷先师诞") })},
            {"3-28", new List<DaoFestival>(new[] { new DaoFestival("东岳大帝圣诞") })},
            {"4-1", new List<DaoFestival>(new[] { new DaoFestival("长生谭真君成道之辰") })},
            {"4-10", new List<DaoFestival>(new[] { new DaoFestival("何仙姑圣诞") })},
            {"4-14", new List<DaoFestival>(new[] { new DaoFestival("吕祖纯阳祖师圣诞") })},
            {"4-15", new List<DaoFestival>(new[] { new DaoFestival("钟离祖师圣诞") })},
            {"4-18", new List<DaoFestival>(new[] { new DaoFestival("北极紫微大帝圣诞"), new DaoFestival("泰山圣母碧霞元君诞"), new DaoFestival("华佗神医先师诞") })},
            {"4-20", new List<DaoFestival>(new[] { new DaoFestival("眼光圣母娘娘诞") })},
            {"4-28", new List<DaoFestival>(new[] { new DaoFestival("神农先帝诞") })},
            {"5-1", new List<DaoFestival>(new[] { new DaoFestival("南极长生大帝圣诞") })},
            {"5-5", new List<DaoFestival>(new[] { new DaoFestival("地腊之辰", "地腊，此日五帝会于南方三炁丹天"), new DaoFestival("南方雷祖圣诞"), new DaoFestival("地祗温元帅圣诞"), new DaoFestival("雷霆邓天君圣诞") })},
            {"5-11", new List<DaoFestival>(new[] { new DaoFestival("城隍爷圣诞") })},
            {"5-13", new List<DaoFestival>(new[] { new DaoFestival("关圣帝君降神"), new DaoFestival("关平太子圣诞") })},
            {"5-18", new List<DaoFestival>(new[] { new DaoFestival("张天师圣诞") })},
            {"5-20", new List<DaoFestival>(new[] { new DaoFestival("马祖丹阳真人圣诞") })},
            {"5-29", new List<DaoFestival>(new[] { new DaoFestival("紫青白祖师圣诞") })},
            {"6-1", new List<DaoFestival>(new[] { new DaoFestival("南斗星君下降") })},
            {"6-2", new List<DaoFestival>(new[] { new DaoFestival("南斗星君下降") })},
            {"6-3", new List<DaoFestival>(new[] { new DaoFestival("南斗星君下降") })},
            {"6-4", new List<DaoFestival>(new[] { new DaoFestival("南斗星君下降") })},
            {"6-5", new List<DaoFestival>(new[] { new DaoFestival("南斗星君下降") })},
            {"6-6", new List<DaoFestival>(new[] { new DaoFestival("南斗星君下降") })},
            {"6-10", new List<DaoFestival>(new[] { new DaoFestival("刘海蟾祖师圣诞") })},
            {"6-15", new List<DaoFestival>(new[] { new DaoFestival("灵官王天君圣诞") })},
            {"6-19", new List<DaoFestival>(new[] { new DaoFestival("慈航(观音)成道日") })},
            {"6-23", new List<DaoFestival>(new[] { new DaoFestival("火神圣诞") })},
            {"6-24", new List<DaoFestival>(new[] { new DaoFestival("南极大帝中方雷祖圣诞"), new DaoFestival("关圣帝君圣诞") })},
            {"6-26", new List<DaoFestival>(new[] { new DaoFestival("二郎真君圣诞") })},
            {"7-7", new List<DaoFestival>(new[] { new DaoFestival("道德腊之辰", "道德腊，此日五帝会于西方七炁素天"), new DaoFestival("庆生中会", "此日中元赦罪，地官同天水二官考校罪福") })},
            {"7-12", new List<DaoFestival>(new[] { new DaoFestival("西方雷祖圣诞") })},
            {"7-15", new List<DaoFestival>(new[] { new DaoFestival("中元地官大帝圣诞") })},
            {"7-18", new List<DaoFestival>(new[] { new DaoFestival("王母娘娘圣诞") })},
            {"7-20", new List<DaoFestival>(new[] { new DaoFestival("刘祖(刘处玄)长生真人圣诞") })},
            {"7-22", new List<DaoFestival>(new[] { new DaoFestival("财帛星君文财神增福相公李诡祖圣诞") })},
            {"7-26", new List<DaoFestival>(new[] { new DaoFestival("张三丰祖师圣诞") })},
            {"8-1", new List<DaoFestival>(new[] { new DaoFestival("许真君飞升日") })},
            {"8-3", new List<DaoFestival>(new[] { new DaoFestival("九天司命灶君诞") })},
            {"8-5", new List<DaoFestival>(new[] { new DaoFestival("北方雷祖圣诞") })},
            {"8-10", new List<DaoFestival>(new[] { new DaoFestival("北岳大帝诞辰") })},
            {"8-15", new List<DaoFestival>(new[] { new DaoFestival("太阴星君诞") })},
            {"9-1", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-2", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-3", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-4", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-5", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-6", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-7", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-8", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰") })},
            {"9-9", new List<DaoFestival>(new[] { new DaoFestival("北斗九皇降世之辰"), new DaoFestival("斗姥元君圣诞"), new DaoFestival("重阳帝君圣诞"), new DaoFestival("玄天上帝飞升"), new DaoFestival("酆都大帝圣诞") })},
            {"9-22", new List<DaoFestival>(new[] { new DaoFestival("增福财神诞") })},
            {"9-23", new List<DaoFestival>(new[] { new DaoFestival("萨翁真君圣诞") })},
            {"9-28", new List<DaoFestival>(new[] { new DaoFestival("五显灵官马元帅圣诞") })},
            {"10-1", new List<DaoFestival>(new[] { new DaoFestival("民岁腊之辰", "民岁腊，此日五帝会于北方五炁黑天"), new DaoFestival("东皇大帝圣诞") })},
            {"10-3", new List<DaoFestival>(new[] { new DaoFestival("三茅应化真君圣诞") })},
            {"10-6", new List<DaoFestival>(new[] { new DaoFestival("天曹诸司五岳五帝圣诞") })},
            {"10-15", new List<DaoFestival>(new[] { new DaoFestival("下元水官大帝圣诞"), new DaoFestival("建生大会", "此日下元解厄，水官同天地二官考校罪福") })},
            {"10-18", new List<DaoFestival>(new[] { new DaoFestival("地母娘娘圣诞") })},
            {"10-19", new List<DaoFestival>(new[] { new DaoFestival("长春邱真君飞升") })},
            {"10-20", new List<DaoFestival>(new[] { new DaoFestival("虚靖天师(即三十代天师弘悟张真人)诞") })},
            {"11-6", new List<DaoFestival>(new[] { new DaoFestival("西岳大帝圣诞") })},
            {"11-9", new List<DaoFestival>(new[] { new DaoFestival("湘子韩祖圣诞") })},
            {"11-11", new List<DaoFestival>(new[] { new DaoFestival("太乙救苦天尊圣诞") })},
            {"11-26", new List<DaoFestival>(new[] { new DaoFestival("北方五道圣诞") })},
            {"12-8", new List<DaoFestival>(new[] { new DaoFestival("王侯腊之辰", "王侯腊，此日五帝会于上方玄都玉京") })},
            {"12-16", new List<DaoFestival>(new[] { new DaoFestival("南岳大帝圣诞"), new DaoFestival("福德正神诞") })},
            {"12-20", new List<DaoFestival>(new[] { new DaoFestival("鲁班先师圣诞") })},
            {"12-21", new List<DaoFestival>(new[] { new DaoFestival("天猷上帝圣诞") })},
            {"12-22", new List<DaoFestival>(new[] { new DaoFestival("重阳祖师圣诞") })},
            {"12-23", new List<DaoFestival>(new[] { new DaoFestival("祭灶王", "最适宜谢旧年太岁，开启拜新年太岁") })},
            {"12-25", new List<DaoFestival>(new[] { new DaoFestival("玉帝巡天"), new DaoFestival("天神下降") })},
            {"12-29", new List<DaoFestival>(new[] { new DaoFestival("清静孙真君(孙不二)成道") })}
        };
}
