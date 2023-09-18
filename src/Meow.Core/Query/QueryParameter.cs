using Meow.Ui;

namespace Meow.Query;

/// <summary>
/// 查询参数
/// </summary>
[Model( "queryParam" )]
public class QueryParameter : Pager {
    /// <summary>
    /// 搜索关键字
    /// </summary>
    [Display( Name = "meow.keyword" )]
    public string Keyword { get; set; }
}