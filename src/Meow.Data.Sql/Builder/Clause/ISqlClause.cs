namespace Meow.Data.Sql.Builder.Clause;

/// <summary>
/// Sql子句
/// </summary>
public interface ISqlClause : ISqlContent
{
    /// <summary>
    /// 验证
    /// </summary>
    bool Validate();
}