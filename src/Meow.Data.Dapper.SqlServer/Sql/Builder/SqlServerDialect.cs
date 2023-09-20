namespace Meow.Data.Dapper.Sql.Builder;

/// <summary>
/// Sql Server方言
/// </summary>
public class SqlServerDialect : DialectBase {
    /// <summary>
    /// 封闭构造方法
    /// </summary>
    private SqlServerDialect() {
    }

    /// <summary>
    /// Sql Server方言实例
    /// </summary>
    public static readonly IDialect Instance = new SqlServerDialect();

    /// <inheritdoc />
    public override string GetOpeningIdentifier() {
        return "[";
    }

    /// <inheritdoc />
    public override string GetClosingIdentifier() {
        return "]";
    }

    /// <inheritdoc />
    public override string GetPrefix() {
        return "@";
    }
}