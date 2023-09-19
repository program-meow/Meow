using Meow.Data.Sql.Builder.Param;

namespace Meow.Data.Sql.Builder.Condition;

/// <summary>
/// Not In查询条件
/// </summary>
public class NotInCondition : InCondition {
    /// <summary>
    /// 初始化Not In查询条件
    /// </summary>
    /// <param name="parameterManager">Sql参数管理器</param>
    /// <param name="column">列名</param>
    /// <param name="value">值</param>
    /// <param name="isParameterization">是否参数化</param>
    public NotInCondition( IParameterManager parameterManager , string column , object value , bool isParameterization )
        : base( parameterManager , column , value , isParameterization ) {
    }

    /// <summary>
    /// 获取操作符关键字
    /// </summary>
    protected override string GetOperator() {
        return "Not In";
    }
}