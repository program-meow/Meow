namespace Meow.Calendar.Festival;

/// <summary>
/// 道历 - 节日
/// </summary>
public class DaoFestival {

    /// <summary>
    /// 名称
    /// </summary>
    private string Name { get; }

    /// <summary>
    /// 备注
    /// </summary>
    private string Remark { get; }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="remark">备注</param>
    public DaoFestival( string name , string remark = null ) {
        Name = name;
        Remark = remark ?? "";
    }

    /// <inheritdoc />
    public override string ToString() {
        return Name;
    }

    /// <summary>
    /// 完整字符串输出
    /// </summary>
    public string FullString {
        get {
            StringBuilder s = new StringBuilder();
            s.Append( Name );
            if( !string.IsNullOrEmpty( Remark ) ) {
                s.Append( '[' );
                s.Append( Remark );
                s.Append( ']' );
            }
            return s.ToString();
        }
    }
}
