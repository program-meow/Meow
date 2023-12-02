namespace Meow.Calendar;

/// <summary>
/// 节假日 - 数据
/// </summary>
public partial class Holiday {
    //节假日数据，日期YYYYMMDD+名称下标+是否调休+对应节日YYYYMMDD
    //需要修正或追加的节假日数据，每18位表示1天依次排列，格式：当天年月日YYYYMMDD(8位)+节假日名称下标(1位)+调休标识(1位)+节假日当天YYYYMMDD(8位)。
    //例：202005023120200501代表2020-05-02为劳动节放假，对应节假日为2020-05-01
    public string A = "2001 12 29 0 0 2002 01 01";
}
