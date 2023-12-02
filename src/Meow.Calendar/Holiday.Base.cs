namespace Meow.Calendar;

/// <summary>
/// 节假日
/// </summary>
public partial class Holiday {
    /// <summary>
    /// 日期
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 年
    /// </summary>
    public int Year => Date.Year;

    /// <summary>
    /// 月
    /// </summary>
    public int Month => Date.Month;

    /// <summary>
    /// 日
    /// </summary>
    public int Day => Date.Day;

    /// <summary>
    /// 名称
    /// </summary>
    public List<string> Name { get; set; }

    /// <summary>
    /// 名称描述
    /// </summary>
    public string NameDescription => Name?.Join() ?? "";

    /// <summary>
    /// 是否上班（若调休，则需要调整上班）
    /// </summary>
    public bool Work { get; set; }

    /// <summary>
    /// 是否上班描述
    /// </summary>
    public string WorkDescription => Work ? "班" : "休";

    /// <summary>
    /// 关联节日 - 日期
    /// </summary>
    public DateTime TargetDate { get; set; }

    /// <summary>
    /// 关联节日 - 年
    /// </summary>
    public int TargetYear => TargetDate.Year;

    /// <summary>
    /// 关联节日 - 月
    /// </summary>
    public int TargetMonth => TargetDate.Month;

    /// <summary>
    /// 关联节日 - 日
    /// </summary>
    public int TargetDay => TargetDate.Day;
}
